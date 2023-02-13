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
        var apiKey = Environment.GetEnvironmentVariable("Vonage.Key");
        var apiSecret = Environment.GetEnvironmentVariable("Vonage.Key");
        Console.WriteLine($"Vonage.Key: {apiKey}");
        outputHelper.WriteLine($"Vonage.Key: {apiKey}");
        this.client = new VonageClient(Credentials.FromApiKeyAndSecret(apiKey ?? Configuration.Instance.ApiKey,
            apiSecret ?? Configuration.Instance.ApiSecret));
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