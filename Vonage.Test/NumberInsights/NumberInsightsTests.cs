#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.NumberInsights;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.NumberInsights;

[Trait("Category", "Legacy")]
public class NumberInsightsTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(NumberInsightsTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    [Fact]
    public async Task Advanced()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/async/json")
                .WithParam("callback", "https://example.com/callback")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightAsynchronousAsync(NumberInsightsTestData.CreateAdvancedAsyncRequest());
        response.ShouldMatchExpectedAsyncResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous_FailedRequest()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/async/json")
                .WithParam("callback", "https://example.com/callback")
                .WithParam("ip", "123.0.0.255")
                .WithParam("cnam", "true")
                .WithParam("number", "15555551212")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var exception = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient()
                .GetNumberInsightAsynchronousAsync(NumberInsightsTestData
                    .CreateAdvancedAsyncRequestWithAllProperties()));
        exception.Response.Status.Should().Be(4);
    }

    [Fact]
    public async Task Advanced_FailedRequest()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var exception = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient().GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest()));
        exception.Response.Status.Should().Be(4);
        (exception.Response as AdvancedInsightsResponse)!.StatusMessage.Should().Be("invalid credentials");
    }

    [Fact]
    public async Task Advanced_NullableValues()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponseWithNullableValues();
    }

    [Fact]
    public async Task Advanced_WithActiveRealTimeData()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("ip", "123.0.0.255")
                .WithParam("real_time_data", "true")
                .WithParam("cnam", "true")
                .WithParam("number", "15555551212")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(true);
    }

    [Fact]
    public async Task Advanced_WithInactiveRealTimeData()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("ip", "123.0.0.255")
                .WithParam("real_time_data", "true")
                .WithParam("cnam", "true")
                .WithParam("number", "15555551212")
                .WithParam("country", "GB")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(false);
    }

    [Fact]
    public async Task Advanced_WithoutRoamingStatus()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/advanced/json")
                .WithParam("number", "447700900000")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(NumberInsightsTestData
                .CreateAdvancedRequestForRoaming());
        response.ShouldMatchExpectedAdvancedResponseWithoutRoaming();
    }

    [Fact]
    public async Task Basic()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/basic/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response =
            await this.CreateClient().GetNumberInsightBasicAsync(NumberInsightsTestData.CreateBasicRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task Standard()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/standard/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponse();
    }

    [Fact]
    public async Task Standard_NullCarrier()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/standard/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithNullCarrier();
    }

    [Fact]
    public async Task Standard_WithoutRoaming()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/ni/standard/json")
                .WithParam("number", "15555551212")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithoutRoaming();
    }

    private INumberInsightClient CreateClient() => this.context.VonageClient.NumberInsightClient;
}