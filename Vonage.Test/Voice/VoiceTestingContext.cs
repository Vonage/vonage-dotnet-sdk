#region
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Vonage.Request;
using Vonage.Test.Common.TestHelpers;
using WireMock.Server;
using TimeProvider = Vonage.Common.TimeProvider;
#endregion

namespace Vonage.Test.Voice;

internal class VoiceTestingContext : IDisposable
{
    private VoiceTestingContext(Credentials credentials, string authorizationHeaderValue,
        Dictionary<string, string> settings)
    {
        this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
        this.Server = WireMockServer.Start();
        settings.Add(VonageUrls.NexmoApiKey, this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.EMEA", this.Server.Url!.TrimEnd('/') + "/EMEA");
        settings.Add($"{VonageUrls.NexmoApiKey}.APAC", this.Server.Url!.TrimEnd('/') + "/APAC");
        settings.Add($"{VonageUrls.NexmoApiKey}.AMER", this.Server.Url!.TrimEnd('/') + "/AMER");
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

    public static VoiceTestingContext WithBearerCredentials() =>
        new VoiceTestingContext(CreateBearerCredentials(), "Bearer *", new Dictionary<string, string>());

    private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
        Guid.NewGuid().ToString(),
        TokenHelper.GetKey());

    protected void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        this.Server.Stop();
        this.Server.Dispose();
    }
}