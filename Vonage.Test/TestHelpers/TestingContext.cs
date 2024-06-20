using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Vonage.Request;
using Vonage.Test.Common.TestHelpers;
using WireMock.Server;
using TimeProvider = Vonage.Common.TimeProvider;

namespace Vonage.Test.TestHelpers;

internal class TestingContext : IDisposable
{
    private TestingContext(Credentials credentials, string authorizationHeaderValue,
        Dictionary<string, string> settings)
    {
        this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
        this.Server = WireMockServer.Start();
        settings.Add(VonageUrls.NexmoApiKey, this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.EMEA", this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.APAC", this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.AMER", this.Server.Url);
        settings.Add(VonageUrls.NexmoRestKey, this.Server.Url);
        settings.Add(VonageUrls.VideoApiKey, this.Server.Url);
        settings.Add(VonageUrls.OidcApiKey, this.Server.Url);
        var configuration =
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(settings).Build());
        this.VonageClient = new VonageClient(credentials, configuration, new TimeProvider());
    }

    public string ExpectedAuthorizationHeaderValue { get; }
    public WireMockServer Server { get; }
    public VonageClient VonageClient { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public static TestingContext WithBasicCredentials(string appSettingsKey) =>
        new TestingContext(CreateBasicCredentials(), "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5",
            new Dictionary<string, string>());

    public static TestingContext WithBearerCredentials(string appSettingsKey) =>
        new TestingContext(CreateBearerCredentials(), "Bearer *", new Dictionary<string, string>());

    public static TestingContext WithBasicCredentials(string appSettingsKey, Dictionary<string, string> settings) =>
        new TestingContext(CreateBasicCredentials(), "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5", settings);

    private static Credentials CreateBasicCredentials() => Credentials.FromApiKeyAndSecret("790fc5e5", "Aa3456789");

    private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
        Guid.NewGuid().ToString(),
        TokenHelper.GetKey());

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        this.Server.Stop();
        this.Server.Dispose();
    }
}