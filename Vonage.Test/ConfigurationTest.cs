using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Vonage.Cryptography;
using Vonage.Test.Common.Extensions;
using Vonage.Test.TestHelpers;
using Vonage.VerifyV2.Cancel;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Test;

[Trait("Category", "Unit")]
public class ConfigurationTest
{
    [Fact]
    public void BuildCredentials_ShouldCreateEmptyCredentials_GivenConfigurationContainsNoElement()
    {
        var credentials = Configuration.FromConfiguration(new ConfigurationBuilder().Build()).BuildCredentials();
        credentials.ApiKey.Should().BeEmpty();
        credentials.ApiSecret.Should().BeEmpty();
        credentials.ApplicationId.Should().BeEmpty();
        credentials.ApplicationKey.Should().BeEmpty();
        credentials.SecuritySecret.Should().BeEmpty();
        credentials.Method.Should().Be(SmsSignatureGenerator.Method.md5hash);
    }

    [Fact]
    public void FromConfiguration_ShouldCreateEmptyConfiguration_GivenConfigurationContainsNoElement()
    {
        var configuration = Configuration.FromConfiguration(new ConfigurationBuilder().Build());
        configuration.ApiKey.Should().BeEmpty();
        configuration.ApiSecret.Should().BeEmpty();
        configuration.ApplicationId.Should().BeEmpty();
        configuration.ApplicationKey.Should().BeEmpty();
        configuration.SecuritySecret.Should().BeEmpty();
        configuration.SigningMethod.Should().BeEmpty();
        configuration.UserAgent.Should().BeEmpty();
        configuration.VonageUrls.Nexmo.Should().Be(new Uri("https://api.nexmo.com"));
        configuration.VonageUrls.Rest.Should().Be(new Uri("https://rest.nexmo.com"));
        configuration.VonageUrls.Video.Should().Be(new Uri("https://video.api.vonage.com"));
        configuration.VonageUrls.Oidc.Should().Be("https://oidc.idp.vonage.com");
        configuration.VonageUrls.Get(VonageUrls.Region.EU).Should().Be(new Uri("https://api-eu.vonage.com"));
        configuration.VonageUrls.Get(VonageUrls.Region.APAC).Should().Be(new Uri("https://api-ap.vonage.com"));
        configuration.VonageUrls.Get(VonageUrls.Region.US).Should().Be(new Uri("https://api-us.vonage.com"));
        configuration.RequestTimeout.Should().BeNone();
    }

    [Fact]
    public void FromConfiguration_ShouldSetApiKey_GivenConfigurationContainsApiKey() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Api.Key", "RandomValue"},
            })
            .Build()).ApiKey.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetApiKSecret_GivenConfigurationContainsApiSecret() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Api.Secret", "RandomValue"},
            })
            .Build()).ApiSecret.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetApplicationId_GivenConfigurationContainsApplicationId() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Application.Id", "RandomValue"},
            })
            .Build()).ApplicationId.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetApplicationKey_GivenConfigurationContainsApplicationKey() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Application.Key", "RandomValue"},
            })
            .Build()).ApplicationKey.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetNexmoApiUrl_GivenConfigurationContainsNexmoApiUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.Api", "https://api.vonage.com"},
            })
            .Build()).VonageUrls.Nexmo.Should().Be(new Uri("https://api.vonage.com"));

    [Fact]
    public void FromConfiguration_ShouldSetOidcUrl_GivenConfigurationContainsOidcUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.OIDC", "https://api.vonage.com"},
            })
            .Build()).VonageUrls.Oidc.Should().Be(new Uri("https://api.vonage.com"));

    [Fact]
    public void FromConfiguration_ShouldSetRequestTimeout_GivenConfigurationContainsRequestTimeout() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:RequestTimeout", "100"},
            })
            .Build()).RequestTimeout.Should().BeSome(TimeSpan.FromSeconds(100));

    [Fact]
    public void FromConfiguration_ShouldSetRestApiUrl_GivenConfigurationContainsRestApiUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.Rest", "https://api.vonage.com"},
            })
            .Build()).VonageUrls.Rest.Should().Be(new Uri("https://api.vonage.com"));

    [Fact]
    public void FromConfiguration_ShouldSetSecuritySecret_GivenConfigurationContainsSecuritySecret() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Security_secret", "RandomValue"},
            })
            .Build()).SecuritySecret.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetSigningMethod_GivenConfigurationContainsSigningMethod() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Signing_method", "sha512"},
            })
            .Build()).SigningMethod.Should().Be("sha512");

    [Fact]
    public void FromConfiguration_ShouldSetUserAgent_GivenConfigurationContainsUserAgent() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:UserAgent", "RandomValue"},
            })
            .Build()).UserAgent.Should().Be("RandomValue");

    [Fact]
    public void FromConfiguration_ShouldSetVideoApiUrl_GivenConfigurationContainsVideoApiUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.Api.Video", "https://api.vonage.com"},
            })
            .Build()).VonageUrls.Video.Should().Be(new Uri("https://api.vonage.com"));

    [Theory]
    [InlineData(VonageUrls.Region.US, "vonage:Url.Api.AMER")]
    [InlineData(VonageUrls.Region.EU, "vonage:Url.Api.EMEA")]
    [InlineData(VonageUrls.Region.APAC, "vonage:Url.Api.APAC")]
    public void VonageUrl_ShouldReturnCustomApiUsUrl_GivenConfigurationContainsApiUsUrl(VonageUrls.Region region,
        string key) =>
        Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    {key, "https://api.com"},
                }).Build())
            .VonageUrls.Get(region).Should().Be(new Uri("https://api.com"));

    [Fact]
    public void VonageUrl_ShouldReturnCustomNexmoUrl_GivenConfigurationContainsDefaultUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                    {{"vonage:Url.Api", "https://api.com"}}).Build())
            .VonageUrls.Nexmo.Should().Be(new Uri("https://api.com"));

    [Fact]
    public void VonageUrl_ShouldReturnCustomRestUrl_GivenConfigurationContainsRestUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    {"vonage:Url.Rest", "https://api.com"},
                }).Build())
            .VonageUrls.Rest.Should().Be(new Uri("https://api.com"));

    [Fact]
    public void VonageUrl_ShouldReturnCustomVideoUrl_GivenConfigurationContainsVideoUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    {"vonage:Url.Api.Video", "https://api.com"},
                }).Build())
            .VonageUrls.Video.Should().Be(new Uri("https://api.com"));

    [Theory]
    [InlineData(VonageUrls.Region.US, "https://api-us.vonage.com")]
    public void VonageUrl_ShouldReturnDefaultApiUsUrl(VonageUrls.Region region, string defaultValue) =>
        Configuration.FromConfiguration(new ConfigurationBuilder().Build())
            .VonageUrls.Get(region).Should().Be(new Uri(defaultValue));

    [Fact]
    public void VonageUrl_ShouldReturnDefaultRestUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().Build())
            .VonageUrls.Rest.Should().Be(new Uri("https://rest.nexmo.com"));

    [Fact]
    public void VonageUrl_ShouldReturnDefaultVideoUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().Build())
            .VonageUrls.Video.Should().Be(new Uri("https://video.api.vonage.com"));

    [Fact]
    public void VonageUrl_ShouldReturnNexmoUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().Build())
            .VonageUrls.Nexmo.Should().Be(new Uri("https://api.nexmo.com"));

    [Fact]
    public void VonageUrl_ShouldReturnOidcUrl() =>
        Configuration.FromConfiguration(new ConfigurationBuilder().Build())
            .VonageUrls.Oidc.Should().Be(new Uri("https://oidc.idp.vonage.com"));
}

[Collection(nameof(NonThreadSafeCollection))]
public class ProxyTest
{
    [Fact]
    public async Task Proxy_ShouldReceiveRequestToTargetServer()
    {
        var proxyServer = WireMockServer.Start();
        proxyServer.Given(WireMock.RequestBuilders.Request.Create())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.NoContent));
        var testingContext = TestingContext.WithBasicCredentials(new Dictionary<string, string>
        {
            {"vonage:Proxy", proxyServer.Url},
        });
        _ = await testingContext.VonageClient.VerifyV2Client.CancelAsync(
            CancelRequest.Parse(new Guid("af52a489-21b5-454b-b050-a2119f950b8f")));
        proxyServer.LogEntries
            .Any(entry => entry.RequestMessage.AbsoluteUrl ==
                          testingContext.Server.Url + "/v2/verify/af52a489-21b5-454b-b050-a2119f950b8f").Should()
            .BeTrue();
    }
}

[Trait("Category", "HttpConnectionPool")]
[Collection(nameof(NonThreadSafeCollection))]
public class ConnectionLifetimeTest
{
    [Fact]
    public async Task ShouldRenewConnection_GivenIdleTimeoutHasExpired()
    {
        var settings = new Dictionary<string, string>
        {
            {"vonage:PooledConnectionIdleTimeout", "3"},
            {"vonage:PooledConnectionLifetime", "60"},
        };
        var refreshedConnections = await this.RefreshConnectionPool(settings, 5, 5);
        refreshedConnections.Should().Be(5);
    }

    [Theory]
    [InlineData(5, 2, 1)]
    [InlineData(5, 5, 2)]
    [InlineData(5, 7, 3)]
    [InlineData(5, 12, 4)]
    [InlineData(10, 20, 4)]
    public async Task ShouldRenewConnection_GivenLifetimeDurationHasExpired(int connectionLifetime, int loop,
        int expectedResolutionCount)
    {
        var settings = new Dictionary<string, string>
        {
            {"vonage:PooledConnectionLifetime", connectionLifetime.ToString()},
        };
        var refreshedConnections = await this.RefreshConnectionPool(settings, 2, loop);
        refreshedConnections.Should().Be(expectedResolutionCount);
    }

    private async Task<int> RefreshConnectionPool(Dictionary<string, string> settings, int timerBuffer, int loops)
    {
        using var spy = new EventSpy();
        var helper = TestingContext.WithBasicCredentials(settings);
        helper.Server.Given(WireMock.RequestBuilders.Request.Create().UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(timerBuffer));
        for (var iterations = 0; iterations < loops; iterations++)
        {
            await timer.WaitForNextTickAsync();
            await helper.VonageClient.AccountClient.GetAccountBalanceAsync();
        }

        spy.ReceivedRequests.Should().Be(loops);
        return spy.RefreshedConnections;
    }
}

internal class EventSpy : EventListener
{
    public int ReceivedRequests { get; private set; }
    public int RefreshedConnections { get; private set; }

    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name.StartsWith("System.Net"))
        {
            this.EnableEvents(eventSource, EventLevel.Informational);
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        switch (eventData.EventName)
        {
            case "ResolutionStart":
                this.RefreshedConnections++;
                break;
            case "RequestStart":
                this.ReceivedRequests++;
                break;
        }
    }
}