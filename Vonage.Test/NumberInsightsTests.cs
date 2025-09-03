#region
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.NumberInsights;
using Xunit;
#endregion

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class NumberInsightsTests : TestBase
{
    [Fact]
    public async Task Advanced()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        this.CreateClient();
        var response = await this.CreateClient().GetNumberInsightAdvancedAsync(request);
        AssertAdvancedInsightResponseCommon(response);
    }

    [Fact]
    public async Task Advanced_Asynchronous()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Number = "15555551212",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightAsynchronousAsync(request);
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task Advanced_Asynchronous_FailedRequest()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri);
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient().GetNumberInsightAsynchronousAsync(request));
        Assert.Equal(4, ex.Response.Status);
    }

    [Fact]
    public async Task Advanced_Asynchronous_WithExplicitCredentials()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, nameof(this.Advanced_Asynchronous));
        var response =
            await this.CreateClient().GetNumberInsightAsynchronousAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task Advanced_FailedRequest()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest {Number = "15555551212"};
        this.SetupHttpMock(expectedUri);
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            this.CreateClient().GetNumberInsightAdvancedAsync(request));
        Assert.Equal(4, ex.Response.Status);
        Assert.Equal("invalid credentials", ((AdvancedInsightsResponse) ex.Response).StatusMessage);
    }

    [Fact]
    public async Task Advanced_NullableValues()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightAdvancedAsync(request);
        AssertAdvancedWithNullableValues(response);
    }

    [Fact]
    public async Task Advanced_NullableValues_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?ip=123.0.0.255&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
            {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        this.SetupHttpMock(expectedUri, nameof(this.Advanced_NullableValues));
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedWithNullableValues(response);
    }

    [Fact]
    public async Task Advanced_WithActiveRealTimeData()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
            RealTimeData = true,
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightAdvancedAsync(request);
        AssertRealTimeDataBase(response);
        Assert.True(response.RealTimeData.ActiveStatus);
    }

    [Fact]
    public async Task Advanced_WithExplicitCredentials()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
            {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        this.SetupHttpMock(expectedUri, nameof(this.Advanced));
        var response =
            await this.CreateClient().GetNumberInsightAdvancedAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedInsightResponseCommon(response);
    }

    [Fact]
    public async Task Advanced_WithInactiveRealTimeData()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
            RealTimeData = true,
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightAdvancedAsync(request);
        AssertRealTimeDataBase(response);
        Assert.False(response.RealTimeData.ActiveStatus);
    }

    [Fact]
    public async Task Advanced_WithoutRoamingStatus()
    {
        var expectedResponse = this.GetResponseJson();
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?number=447700900000&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "447700900000",
        };
        this.Setup(expectedUri, expectedResponse);
        var client = this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication());
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        AssertAdvancedNotRoamingResponse(response);
    }

    [Fact]
    public async Task Basic()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&";
        var request = new BasicNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightBasicAsync(request);
        AssertBasicInsightResponse(response);
    }

    [Fact]
    public async Task Basic_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&country=GB&";
        var request = new BasicNumberInsightRequest
        {
            Country = "GB",
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, nameof(this.Basic));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightBasicAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertBasicInsightResponse(response);
    }

    [Fact]
    public async Task Standard()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        var response = await this.CreateClient().GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        AssertRoamingActive(response.Roaming, "Acme Inc", "12345", "US", RoamingStatus.Roaming);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    [Fact]
    public async Task Standard_NullCarrier()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        this.CreateClient();
        var response = await this.CreateClient().GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        AssertCarrier(response.CurrentCarrier, null, null, null, null);
    }

    [Fact]
    public async Task Standard_NullCarrier_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, nameof(this.Standard_NullCarrier));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        AssertCarrier(response.CurrentCarrier, null, null, null, null);
    }

    [Fact]
    public async Task Standard_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, nameof(this.Standard));
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        AssertRoamingActive(response.Roaming, "Acme Inc", "12345", "US", RoamingStatus.Roaming);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    [Fact]
    public async Task Standard_WithoutRoaming()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri);
        this.CreateClient();
        var response = await this.CreateClient().GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    [Fact]
    public async Task Standard_WithoutRoaming_WithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, nameof(this.Standard_WithoutRoaming));
        this.CreateClient();
        var response =
            await this.CreateClient().GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    private static void AssertAdvancedAsynchronousResponse(AdvancedInsightsAsynchronousResponse response)
    {
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.Number);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("0.01500000", response.RequestPrice);
        Assert.Equal(0, response.Status);
    }

    private static void AssertAdvancedInsightResponseCommon(AdvancedInsightsResponse response)
    {
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        AssertLookupOutcomeSuccess(response);
        AssertStandardCallerInfo(response);
        AssertCallerId(response.CallerIdentity, "John", "Smith", "John Smith", CallerType.consumer);
        AssertRoamingActive(response.Roaming, "Acme Inc", "12345", "US", RoamingStatus.Roaming);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        AssertCarrier(response.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(response);
        AssertStandardPhoneFormat(response);
        AssertStandardPricing(response);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    private static void AssertAdvancedNotRoamingResponse(AdvancedInsightsResponse response)
    {
        AssertStatusSuccess(response);
        AssertLookupOutcomeSuccess(response);
        AssertStandardPhoneFormat(response);
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertCarrier(response.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        Assert.Equal(RoamingStatus.NotRoaming, response.Roaming.Status);
        Assert.Null(response.Roaming.RoamingNetworkName);
        Assert.Null(response.Roaming.RoamingCountryCode);
        Assert.Null(response.Roaming.RoamingNetworkCode);
    }

    private static void AssertAdvancedWithNullableValues(AdvancedInsightsResponse response)
    {
        Assert.Null(response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        AssertLookupOutcomeSuccess(response);
        AssertStandardCallerInfo(response);
        AssertCallerId(response.CallerIdentity, "John", "Smith", "John Smith", CallerType.consumer);
        Assert.Null(response.Roaming);
        Assert.Null(response.Ported);
        AssertCarrier(response.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(response);
        AssertStandardPhoneFormat(response);
        AssertStandardPricing(response);
        AssertCarrier(response.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    private static void AssertBasicInsightResponse(BasicInsightResponse response)
    {
        AssertStatusSuccess(response);
        AssertStandardPhoneFormat(response);
    }

    private static void AssertCallerId(CallerId identity, string firstName, string lastName, string callerName,
        CallerType callerType)
    {
        Assert.Equal(firstName, identity.FirstName);
        Assert.Equal(lastName, identity.LastName);
        Assert.Equal(callerName, identity.CallerName);
        Assert.Equal(callerType, identity.CallerType);
    }

    private static void AssertCarrier(Carrier carrier, string networkCode, string name, string country,
        string networkType)
    {
        Assert.Equal(networkCode, carrier.NetworkCode);
        Assert.Equal(name, carrier.Name);
        Assert.Equal(country, carrier.Country);
        Assert.Equal(networkType, carrier.NetworkType);
    }

    private static void AssertLookupOutcomeSuccess(dynamic response)
    {
        Assert.Equal(0, response.LookupOutcome);
        Assert.Equal("Success", response.LookupOutcomeMessage);
    }

    private static void AssertRealTimeDataBase(AdvancedInsightsResponse response)
    {
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.NotNull(response.RealTimeData);
        Assert.Equal("on", response.RealTimeData.HandsetStatus);
    }

    private static void AssertRoamingActive(Roaming roaming, string networkName, string networkCode, string countryCode,
        RoamingStatus status)
    {
        Assert.Equal(networkName, roaming.RoamingNetworkName);
        Assert.Equal(networkCode, roaming.RoamingNetworkCode);
        Assert.Equal(countryCode, roaming.RoamingCountryCode);
        Assert.Equal(status, roaming.Status);
    }

    private static void AssertStandardCallerInfo(dynamic response)
    {
        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
    }

    private static void AssertStandardInsightResponseCommon(StandardInsightResponse response)
    {
        AssertStandardCallerInfo(response);
        AssertCallerId(response.CallerIdentity, "John", "Smith", "John Smith", CallerType.consumer);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        AssertCarrier(response.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(response);
        AssertStandardPhoneFormat(response);
        AssertStandardPricing(response);
    }

    private static void AssertStandardPhoneFormat(dynamic response)
    {
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.InternationalFormatNumber);
        Assert.Equal("07700 900000", response.NationalFormatNumber);
        Assert.Equal("GB", response.CountryCode);
        Assert.Equal("GBR", response.CountryCodeIso3);
        Assert.Equal("United Kingdom", response.CountryName);
        Assert.Equal("44", response.CountryPrefix);
    }

    private static void AssertStandardPricing(dynamic response)
    {
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("0.01500000", response.RefundPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
    }

    private static void AssertStatusSuccess(dynamic response)
    {
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
    }

    private INumberInsightClient CreateClient() =>
        this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication()).NumberInsightClient;

    private void SetupHttpMock(string expectedUri, [CallerMemberName] string responseFileName = null) =>
        this.Setup(expectedUri,
            responseFileName != null ? this.GetResponseJson(responseFileName) : this.GetResponseJson());
}