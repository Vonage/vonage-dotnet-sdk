using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Vonage.Request;
using Vonage.Test.Common.TestHelpers;
using WireMock.Server;
using TimeProvider = Vonage.Common.TimeProvider;

namespace Vonage.Test.NumberVerification;

internal class OidcTestingContext : IDisposable
{
    private OidcTestingContext(Credentials credentials, string authorizationHeaderValue,
        Dictionary<string, string> settings)
    {
        this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
        this.VonageServer = WireMockServer.Start();
        this.OidcServer = WireMockServer.Start();
        settings.Add(VonageUrls.NexmoApiKey, this.VonageServer.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.EMEA", this.VonageServer.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.APAC", this.VonageServer.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.AMER", this.VonageServer.Url);
        settings.Add(VonageUrls.NexmoRestKey, this.VonageServer.Url);
        settings.Add(VonageUrls.VideoApiKey, this.VonageServer.Url);
        settings.Add(VonageUrls.OidcApiKey, this.OidcServer.Url);
        var configuration =
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(settings).Build());
        this.VonageClient = new VonageClient(credentials, configuration, new TimeProvider());
    }

    public string ExpectedAuthorizationHeaderValue { get; protected init; }
    public WireMockServer VonageServer { get; }
    public WireMockServer OidcServer { get; }
    public VonageClient VonageClient { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public static OidcTestingContext WithBearerCredentials() =>
        new OidcTestingContext(CreateBearerCredentials(), "Bearer *", new Dictionary<string, string>());

    private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
        Guid.NewGuid().ToString(),
        TokenHelper.GetKey());

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        this.VonageServer.Stop();
        this.VonageServer.Dispose();
    }
}