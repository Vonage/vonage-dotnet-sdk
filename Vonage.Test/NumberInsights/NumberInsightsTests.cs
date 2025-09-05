#region
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.NumberInsights;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.NumberInsights;

[Trait("Category", "Legacy")]
public class NumberInsightsTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(NumberInsightsTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task Advanced()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/advanced/json?number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous()
    {
        this.SetupHttpMock(
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightAsynchronousAsync(NumberInsightsTestData.CreateAdvancedAsyncRequest());
        response.ShouldMatchExpectedAsyncResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous_FailedRequest()
    {
        this.SetupHttpMock(
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&");
        var exception = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient()
                .GetNumberInsightAsynchronousAsync(NumberInsightsTestData
                    .CreateAdvancedAsyncRequestWithAllProperties()));
        Assert.Equal(4, exception.Response.Status);
    }

    [Fact]
    public async Task Advanced_FailedRequest()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/advanced/json?number=15555551212&");
        var exception = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient().GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest()));
        Assert.Equal(4, exception.Response.Status);
        Assert.Equal("invalid credentials", ((AdvancedInsightsResponse) exception.Response).StatusMessage);
    }

    [Fact]
    public async Task Advanced_NullableValues()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/advanced/json?number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponseWithNullableValues();
    }

    [Fact]
    public async Task Advanced_WithActiveRealTimeData()
    {
        this.SetupHttpMock(
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&");
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(true);
    }

    [Fact]
    public async Task Advanced_WithInactiveRealTimeData()
    {
        this.SetupHttpMock(
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&");
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(false);
    }

    [Fact]
    public async Task Advanced_WithoutRoamingStatus()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/advanced/json?number=447700900000&");
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(NumberInsightsTestData
                .CreateAdvancedRequestForRoaming());
        response.ShouldMatchExpectedAdvancedResponseWithoutRoaming();
    }

    [Fact]
    public async Task Basic()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/basic/json?number=15555551212&");
        var response =
            await this.CreateClient().GetNumberInsightBasicAsync(NumberInsightsTestData.CreateBasicRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task Standard()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/standard/json?number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponse();
    }

    [Fact]
    public async Task Standard_NullCarrier()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/standard/json?number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithNullCarrier();
    }

    [Fact]
    public async Task Standard_WithoutRoaming()
    {
        this.SetupHttpMock($"{this.ApiUrl}/ni/standard/json?number=15555551212&");
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithoutRoaming();
    }

    private INumberInsightClient CreateClient() =>
        this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication()).NumberInsightClient;

    private void SetupHttpMock(string expectedUri, [CallerMemberName] string responseFileName = null) =>
        this.Setup(expectedUri, this.helper.GetResponseJson(responseFileName));
}