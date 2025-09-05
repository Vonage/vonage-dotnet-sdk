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
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightAsynchronousAsync(NumberInsightsTestData.CreateAsyncRequest());
        response.ShouldMatchExpectedAsyncResponse();
    }

    [Fact]
    public async Task Advanced_Asynchronous_FailedRequest()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri);
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient()
                .GetNumberInsightAsynchronousAsync(NumberInsightsTestData.CreateAsyncRequestWithAllProperties()));
        Assert.Equal(4, ex.Response.Status);
    }

    [Fact]
    public async Task Advanced_Asynchronous_WithExplicitCredentials()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Advanced_Asynchronous));
        var response =
            await this.CreateClient().GetNumberInsightAsynchronousAsync(
                NumberInsightsTestData.CreateAsyncRequestWithAllProperties(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedAsyncResponse();
    }

    [Fact]
    public async Task Advanced_FailedRequest()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient().GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest()));
        Assert.Equal(4, ex.Response.Status);
        Assert.Equal("invalid credentials", ((AdvancedInsightsResponse) ex.Response).StatusMessage);
    }

    [Fact]
    public async Task Advanced_NullableValues()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequest());
        response.ShouldMatchExpectedAdvancedResponseWithNullableValues();
    }

    [Fact]
    public async Task Advanced_NullableValues_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?ip=123.0.0.255&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Advanced_NullableValues));
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(
                NumberInsightsTestData.CreateAdvancedRequestWithAllProperties(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedAdvancedResponseWithNullableValues();
    }

    [Fact]
    public async Task Advanced_WithActiveRealTimeData()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(true);
    }

    [Fact]
    public async Task Advanced_WithExplicitCredentials()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Advanced));
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(
                NumberInsightsTestData.CreateAdvancedRequestWithAllProperties(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedAdvancedResponse();
    }

    [Fact]
    public async Task Advanced_WithInactiveRealTimeData()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightAdvancedAsync(NumberInsightsTestData.CreateAdvancedRequestWithRealTimeData());
        response.ShouldMatchExpectedRealTimeDataResponse(false);
    }

    [Fact]
    public async Task Advanced_WithoutRoamingStatus()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?number=447700900000&";
        this.SetupHttpMock(expectedUri);
        var client = this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication());
        var response =
            await client.NumberInsightClient.GetNumberInsightAdvancedAsync(NumberInsightsTestData
                .CreateAdvancedRequestForRoaming());
        response.ShouldMatchExpectedAdvancedResponseWithoutRoaming();
    }

    [Fact]
    public async Task Basic()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var response =
            await this.CreateClient().GetNumberInsightBasicAsync(NumberInsightsTestData.CreateBasicRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task Basic_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Basic));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightBasicAsync(NumberInsightsTestData.CreateBasicRequestWithCountry(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task Standard()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponse();
    }

    [Fact]
    public async Task Standard_NullCarrier()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        this.CreateClient();
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithNullCarrier();
    }

    [Fact]
    public async Task Standard_NullCarrier_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Standard_NullCarrier));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(
                NumberInsightsTestData.CreateStandardRequestWithCnam(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedStandardResponseWithNullCarrier();
    }

    [Fact]
    public async Task Standard_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Standard));
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(
                NumberInsightsTestData.CreateStandardRequestWithCnam(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedStandardResponse();
    }

    [Fact]
    public async Task Standard_WithoutRoaming()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        this.SetupHttpMock(expectedUri);
        this.CreateClient();
        var response = await this.CreateClient()
            .GetNumberInsightStandardAsync(NumberInsightsTestData.CreateStandardRequest());
        response.ShouldMatchExpectedStandardResponseWithoutRoaming();
    }

    [Fact]
    public async Task Standard_WithoutRoaming_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        this.SetupHttpMock(expectedUri, nameof(this.Standard_WithoutRoaming));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(
                NumberInsightsTestData.CreateStandardRequestWithCnam(),
                this.BuildCredentialsForBasicAuthentication());
        response.ShouldMatchExpectedStandardResponseWithoutRoaming();
    }

    private INumberInsightClient CreateClient() =>
        this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication()).NumberInsightClient;

    private void SetupHttpMock(string expectedUri, [CallerMemberName] string responseFileName = null) =>
        this.Setup(expectedUri, this.helper.GetResponseJson(responseFileName));
}