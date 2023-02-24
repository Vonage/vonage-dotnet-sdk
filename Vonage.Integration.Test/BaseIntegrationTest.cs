using System.Security.Authentication;
using AutoFixture;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Request;
using Vonage.Server.Video;

namespace Vonage.Integration.Test;

public abstract class BaseIntegrationTest : IDisposable
{
    private readonly VonageClient client;

    protected BaseIntegrationTest()
    {
        this.Fixture = new Fixture();
        this.client = new VonageClient(GetCredentials());
        var credentials = this.CreateApplicationAsync().Result;
        this.ApplicationClient = new VonageClient(credentials);
        this.VideoClient = new VideoClient(credentials);
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
                Video = new Video(),
            },
        };

    private async Task<Credentials> CreateApplicationAsync()
    {
        var result = await this.client.ApplicationClient.CreateApplicaitonAsync(BuildRequest());
        return Credentials.FromAppIdAndPrivateKey(result.Id, result.Keys.PrivateKey);
    }

    private async Task DeleteApplicationAsync() =>
        await this.client.ApplicationClient.DeleteApplicationAsync(this.ApplicationClient.Credentials.ApplicationId);

    private static Credentials GetCredentials() =>
        Credentials.FromApiKeyAndSecret(
            Environment.GetEnvironmentVariable("Vonage.Key") ??
            throw new InvalidCredentialException("Missing variable 'Vonage.Key' from environment variables."),
            Environment.GetEnvironmentVariable("Vonage.Secret") ??
            throw new InvalidCredentialException("Missing variable 'Vonage.Secret' from environment variables."));

    protected readonly VonageClient ApplicationClient;
    protected readonly VideoClient VideoClient;
    protected readonly Fixture Fixture;
}