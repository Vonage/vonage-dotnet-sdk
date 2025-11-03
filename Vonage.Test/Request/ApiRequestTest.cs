#region
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Vonage.Accounts;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
using Vonage.Request;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;
using TimeProvider = Vonage.Common.TimeProvider;
#endregion

namespace Vonage.Test.Request;

public class ApiRequestTest : TestBase
{
    private readonly Configuration defaultConfiguration = new Configuration();
    private Credentials BasicCredentials => Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
    private Credentials BearerCredentials => Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);

    [Fact]
    public async Task Configuration_ShouldAllowCustomBaseUri()
    {
        var server = WireMockServer.Start();
        server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/rest/account/get-balance")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBodyAsJson(new Balance())
                .WithStatusCode(HttpStatusCode.OK));
        this.configuration = Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.Rest", $"{server.Url}/rest"},
            })
            .Build());
        var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
        await client.AccountClient.GetAccountBalanceAsync();
    }

    [Fact]
    public async Task DoGetRequestWithJwtAsync_ShouldThrowOnHttpError()
    {
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"error\": \"Unauthorized\"}"),
            null, HttpStatusCode.Unauthorized);
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        await Assert.ThrowsAsync<VonageHttpRequestException>(() => apiRequest.DoGetRequestWithJwtAsync(uri));
    }

    [Fact]
    public async Task DoGetRequestWithJwtAsync_ShouldUseBearerAuth()
    {
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"));
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var result = await apiRequest.DoGetRequestWithJwtAsync(uri);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DoGetRequestWithQueryParametersAsync_ShouldThrowWhenMissingApiKey()
    {
        var credentials = Credentials.FromApiKeyAndSecret("", this.ApiSecret);
        var apiRequest = ApiRequest.Build(credentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        await Assert.ThrowsAsync<VonageAuthenticationException>(() =>
            apiRequest.DoGetRequestWithQueryParametersAsync<dynamic>(uri, AuthType.Basic));
    }

    [Fact]
    public async Task DoGetRequestWithQueryParametersAsync_ShouldThrowWhenMissingApiSecret()
    {
        var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, "");
        var apiRequest = ApiRequest.Build(credentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        await Assert.ThrowsAsync<VonageAuthenticationException>(() =>
            apiRequest.DoGetRequestWithQueryParametersAsync<dynamic>(uri, AuthType.Basic));
    }

    [Fact]
    public async Task DoGetRequestWithQueryParametersAsync_ShouldUseBearerAuth()
    {
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"));
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var result = await apiRequest.DoGetRequestWithQueryParametersAsync<dynamic>(uri, AuthType.Bearer);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DoPostRequestUrlContentFromObjectAsync_ShouldHandleWithoutCredentials()
    {
        var expectedContent = "test=value&";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedContent);
        var apiRequest = ApiRequest.Build(this.BasicCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var result = await apiRequest.DoPostRequestUrlContentFromObjectAsync<dynamic>(uri, payload, false);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DoPostRequestUrlContentFromObjectAsync_ShouldSerializeObjectToUrlEncoded()
    {
        var expectedContent = "test=value&";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedContent);
        var apiRequest = ApiRequest.Build(this.BasicCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var result = await apiRequest.DoPostRequestUrlContentFromObjectAsync<dynamic>(uri, payload, false);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DoRequestWithJsonContentAsync_ShouldHandleCustomSerialization()
    {
        var expectedPayload = "custom-serialized-data";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedPayload);
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var result = await apiRequest.DoRequestWithJsonContentAsync<dynamic>(
            HttpMethod.Post, uri, payload, AuthType.Bearer,
            _ => "custom-serialized-data",
            json => new {success = true});
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DoRequestWithJsonContentAsync_ShouldSerializePayload()
    {
        var expectedPayload = "{\"test\":\"value\"}";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedPayload);
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var result =
            await apiRequest.DoRequestWithJsonContentAsync<dynamic>(HttpMethod.Post, uri, payload, AuthType.Bearer);
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(AuthType.Basic)]
    [InlineData(AuthType.Bearer)]
    public async Task DoRequestWithJsonContentAsync_ShouldSupportAllAuthTypes(AuthType authType)
    {
        var expectedPayload = "{\"test\":\"value\"}";
        var expectedUrl = $"{this.ApiUrl}/test";
        this.Setup(expectedUrl, Maybe<string>.Some("{\"success\": true}"), expectedPayload);
        var credentials = authType == AuthType.Bearer ? this.BearerCredentials : this.BasicCredentials;
        var apiRequest = ApiRequest.Build(credentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var result = await apiRequest.DoRequestWithJsonContentAsync<dynamic>(
            HttpMethod.Post, uri, payload, authType);
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("PUT")]
    [InlineData("PATCH")]
    [InlineData("DELETE")]
    public async Task DoRequestWithJsonContentAsync_ShouldSupportDifferentHttpMethods(string methodName)
    {
        var expectedPayload = "{\"test\":\"value\"}";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedPayload);
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        var method = new HttpMethod(methodName);
        var result = await apiRequest.DoRequestWithJsonContentAsync<dynamic>(method, uri, payload, AuthType.Bearer);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DoRequestWithJsonContentAsync_ShouldThrowOnInvalidAuthType()
    {
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        await Assert.ThrowsAsync<ArgumentException>(() => apiRequest.DoRequestWithJsonContentAsync<dynamic>(
            HttpMethod.Post, uri, payload, (AuthType) 999));
    }

    [Fact]
    public async Task DoRequestWithJsonContentAsync_VoidVersion_ShouldNotReturnValue()
    {
        var expectedPayload = "{\"test\":\"value\"}";
        this.Setup($"{this.ApiUrl}/test", Maybe<string>.Some("{\"success\": true}"), expectedPayload);
        var apiRequest = ApiRequest.Build(this.BearerCredentials, this.configuration, new TimeProvider());
        var uri = new Uri($"{this.ApiUrl}/test");
        var payload = new {test = "value"};
        await apiRequest.DoRequestWithJsonContentAsync(HttpMethod.Post, uri, payload, AuthType.Bearer,
            JsonConvert.SerializeObject);
    }

    [Fact]
    public void GetBaseUri_ShouldAppendUrlPath() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.defaultConfiguration, "test/path").Should()
            .Be(new Uri(this.defaultConfiguration.VonageUrls.Nexmo, "test/path"));

    [Fact]
    public void GetBaseUri_ShouldHandleLeadingSlash() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.defaultConfiguration, "/test/path").Should()
            .Be(new Uri(this.defaultConfiguration.VonageUrls.Nexmo, "test/path"));

    [Fact]
    public void GetBaseUri_ShouldHandleNullUrl_Api() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.defaultConfiguration).Should()
            .Be(this.defaultConfiguration.VonageUrls.Nexmo);

    [Fact]
    public void GetBaseUri_ShouldHandleNullUrl_Rest() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.defaultConfiguration).Should()
            .Be(this.defaultConfiguration.VonageUrls.Rest);

    [Fact]
    public void GetBaseUri_ShouldReturnCorrectApiUri() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.defaultConfiguration).Should()
            .Be(this.defaultConfiguration.VonageUrls.Nexmo);

    [Fact]
    public void GetBaseUri_ShouldReturnCorrectRestUri() =>
        ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.defaultConfiguration).Should()
            .Be(this.defaultConfiguration.VonageUrls.Rest);
}