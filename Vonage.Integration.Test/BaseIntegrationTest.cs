using System.Security.Authentication;
using AutoFixture;
using Vonage.Applications;
using Vonage.Request;
using Xunit.Abstractions;

namespace Vonage.Integration.Test;

public abstract class BaseIntegrationTest : IDisposable
{
    private readonly VonageClient client;

    protected BaseIntegrationTest(ITestOutputHelper outputHelper)
    {
        this.Fixture = new Fixture();
        this.client = new VonageClient(Credentials.FromApiKeyAndSecret(
            Environment.GetEnvironmentVariable("Vonage.Key") ?? throw new InvalidCredentialException("Missing variable 'Vonage.Key' from environment variables."),
            Environment.GetEnvironmentVariable("Vonage.Secret") ??  throw new InvalidCredentialException("Missing variable 'Vonage.Secret' from environment variables.")));
        this.ApplicationClient = new VonageClient(this.CreateApplicationAsync().Result);
    }

    public virtual void Dispose()
    {
        this.DeleteApplicationAsync().Wait();
        GC.SuppressFinalize(this);
    }

    private static CreateApplicationRequest BuildRequest() =>
        new()
        {
            Name = $"Integration Testing - {Guid.NewGuid()}",
            Capabilities = new ApplicationCapabilities
            {
                Meetings = new Applications.Capabilities.Meetings(),
            },
        };

    private async Task<Credentials> CreateApplicationAsync()
    {
        var result = await this.client.ApplicationClient.CreateApplicaitonAsync(BuildRequest());
        return Credentials.FromAppIdAndPrivateKey(result.Id, result.Keys.PrivateKey);
    }

    private async Task DeleteApplicationAsync() =>
        await this.client.ApplicationClient.DeleteApplicationAsync(this.ApplicationClient.Credentials.ApplicationId);

    protected readonly VonageClient ApplicationClient;
    protected readonly Fixture Fixture;
}