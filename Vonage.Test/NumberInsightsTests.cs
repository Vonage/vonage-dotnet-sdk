#region
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vonage.NumberInsights;
using Xunit;
#endregion

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class NumberInsightsTests : TestBase
{
    [Fact]
    public async Task AdvancedAsynchronous_AlternativeTestFile_WithExplicitCredentials_ShouldReturnValidResponse()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedAsyncAsync");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task AdvancedAsynchronous_AlternativeTestFile_WithMinimalRequest_ShouldReturnValidResponse()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Number = "15555551212",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedAsyncAsync");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task AdvancedAsynchronous_WithExplicitCredentials_ShouldReturnValidResponse()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedAsync");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task AdvancedAsynchronous_WithMinimalRequest_ShouldReturnValidResponse()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Number = "15555551212",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedAsync");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);
        AssertAdvancedAsynchronousResponse(response);
    }

    [Fact]
    public async Task AdvancedAsynchronousFailedRequest()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.SetupHttpMock(expectedUri, "TestFailedAsyncRequest");
        var client = this.CreateClient();
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request));
        Assert.Equal(4, ex.Response.Status);
    }

    [Fact]
    public async Task AdvancedNIRequestSyncWithNotRoamingStatus()
    {
        var expectedResponse = this.GetResponseJson();
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?number=971639946111&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "971639946111",
        };
        this.Setup(expectedUri, expectedResponse);
        var client = this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication());
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal(0, response.LookupOutcome);
        Assert.Equal("Success", response.LookupOutcomeMessage);
        Assert.Equal("784758db-0468-4c61-86dc-2dffdb715bac", response.RequestId);
        Assert.Equal("971123456789", response.InternationalFormatNumber);
        Assert.Equal("053 345 6789", response.NationalFormatNumber);
        Assert.Equal("AE", response.CountryCode);
        Assert.Equal("ARE", response.CountryCodeIso3);
        Assert.Equal("United Arab Emirates", response.CountryName);
        Assert.Equal("971", response.CountryPrefix);
        Assert.Equal("0.03000000", response.RequestPrice);
        Assert.Equal("40.27231333", response.RemainingBalance);
        Assert.Equal("42403", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("AE", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
        Assert.Equal("42403", response.OriginalCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.OriginalCarrier.Name);
        Assert.Equal("AE", response.OriginalCarrier.Country);
        Assert.Equal("mobile", response.OriginalCarrier.NetworkType);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        Assert.Equal(RoamingStatus.NotRoaming, response.Roaming.Status);
        Assert.Null(response.Roaming.RoamingNetworkName);
        Assert.Null(response.Roaming.RoamingCountryCode);
        Assert.Null(response.Roaming.RoamingNetworkCode);
    }

    [Fact]
    public async Task AdvancedNumberInsightFailedRequest()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest {Number = "15555551212"};
        this.SetupHttpMock(expectedUri, "TestFailedAdvancedRequest");
        var client = this.CreateClient();
        var ex = await Assert.ThrowsAsync<VonageNumberInsightResponseException>(() =>
            client.NumberInsightClient.GetNumberInsightAdvancedAsync(request));
        Assert.Equal(4, ex.Response.Status);
        Assert.Equal("invalid credentials", ((AdvancedInsightsResponse) ex.Response).StatusMessage);
    }

    [Fact]
    public async Task AdvancedNumberInsightSyncMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedNIRequestSync");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        AssertAdvancedInsightResponseCommon(response);
    }

    [Fact]
    public async Task AdvancedNumberInsightSyncWithExplicitCredentials()
    {
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
            {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        this.SetupHttpMock(expectedUri, "TestAdvancedNIRequestSync");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertAdvancedInsightResponseCommon(response);
    }

    [Fact]
    public async Task AdvancedNumberInsightWithNullableValuesAndExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?ip=123.0.0.255&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
            {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        this.SetupHttpMock(expectedUri, "TestAdvancedNIRequestSyncWithNullableValues");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        Assert.Null(response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.Equal("Success", response.LookupOutcomeMessage);
        Assert.Equal(0, response.LookupOutcome);
        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Null(response.Roaming);
        Assert.Null(response.Ported);
        Assert.Equal("12345", response.OriginalCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.OriginalCarrier.Name);
        Assert.Equal("GB", response.OriginalCarrier.Country);
        Assert.Equal("mobile", response.OriginalCarrier.NetworkType);
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.InternationalFormatNumber);
        Assert.Equal("07700 900000", response.NationalFormatNumber);
        Assert.Equal("GB", response.CountryCode);
        Assert.Equal("GBR", response.CountryCodeIso3);
        Assert.Equal("United Kingdom", response.CountryName);
        Assert.Equal("44", response.CountryPrefix);
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("0.01500000", response.RefundPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task AdvancedNumberInsightWithNullableValuesMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestAdvancedNIRequestSyncWithNullableValues");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        Assert.Null(response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.Equal("Success", response.LookupOutcomeMessage);
        Assert.Equal(0, response.LookupOutcome);
        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Null(response.Roaming);
        Assert.Null(response.Ported);
        Assert.Equal("12345", response.OriginalCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.OriginalCarrier.Name);
        Assert.Equal("GB", response.OriginalCarrier.Country);
        Assert.Equal("mobile", response.OriginalCarrier.NetworkType);
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.InternationalFormatNumber);
        Assert.Equal("07700 900000", response.NationalFormatNumber);
        Assert.Equal("GB", response.CountryCode);
        Assert.Equal("GBR", response.CountryCodeIso3);
        Assert.Equal("United Kingdom", response.CountryName);
        Assert.Equal("44", response.CountryPrefix);
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("0.01500000", response.RefundPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task AdvancedNumberInsightWithRealTimeDataActive()
    {
        var responseData = new Dictionary<string, string>
        {
            {"active_status", "active"},
        };
        var expectedResponse =
            this.GetResponseJsonWithParameters(responseData, "TestAdvancedNIRequestSyncRealTimeData");
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
        this.Setup(expectedUri, expectedResponse);
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.NotNull(response.RealTimeData);
        Assert.True(response.RealTimeData.ActiveStatus);
        Assert.Equal("on", response.RealTimeData.HandsetStatus);
    }

    [Fact]
    public async Task AdvancedNumberInsightWithRealTimeDataInactive()
    {
        var responseData = new Dictionary<string, string>
        {
            {"active_status", "inactive"},
        };
        var expectedResponse =
            this.GetResponseJsonWithParameters(responseData, "TestAdvancedNIRequestSyncRealTimeData");
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
        this.Setup(expectedUri, expectedResponse);
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.NotNull(response.RealTimeData);
        Assert.False(response.RealTimeData.ActiveStatus);
        Assert.Equal("on", response.RealTimeData.HandsetStatus);
    }

    [Fact]
    public async Task BasicNumberInsightMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&";
        var request = new BasicNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestBasicNIRequest");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightBasicAsync(request);
        AssertBasicInsightResponse(response);
    }

    [Fact]
    public async Task BasicNumberInsightWithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/basic/json?number=15555551212&country=GB&";
        var request = new BasicNumberInsightRequest
        {
            Country = "GB",
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestBasicNIRequest");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightBasicAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertBasicInsightResponse(response);
    }

    [Fact]
    public async Task StandardNumberInsightMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestStandardNIRequest");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        Assert.Equal("Acme Inc", response.Roaming.RoamingNetworkName);
        Assert.Equal("12345", response.Roaming.RoamingNetworkCode);
        Assert.Equal("US", response.Roaming.RoamingCountryCode);
        Assert.Equal(RoamingStatus.Roaming, response.Roaming.Status);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task StandardNumberInsightWithExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, "TestStandardNIRequest");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        Assert.Equal("Acme Inc", response.Roaming.RoamingNetworkName);
        Assert.Equal("12345", response.Roaming.RoamingNetworkCode);
        Assert.Equal("US", response.Roaming.RoamingCountryCode);
        Assert.Equal(RoamingStatus.Roaming, response.Roaming.Status);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task StandardNumberInsightWithNullCarrierAndExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, "TestStandardNIRequestWithNullCarrier");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        Assert.Null(response.CurrentCarrier.NetworkCode);
        Assert.Null(response.CurrentCarrier.Name);
        Assert.Null(response.CurrentCarrier.Country);
        Assert.Null(response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task StandardNumberInsightWithNullCarrierMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestStandardNIRequestWithNullCarrier");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        Assert.Null(response.CurrentCarrier.NetworkCode);
        Assert.Null(response.CurrentCarrier.Name);
        Assert.Null(response.CurrentCarrier.Country);
        Assert.Null(response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task StandardNumberInsightWithoutRoamingAndExplicitCredentials()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?cnam=true&number=15555551212&country=GB&";
        var request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        this.SetupHttpMock(expectedUri, "TestStandardNIRequestWithoutRoaming");
        var client = this.CreateClient();
        var response =
            await client.NumberInsightClient.GetNumberInsightStandardAsync(request,
                this.BuildCredentialsForBasicAuthentication());
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    [Fact]
    public async Task StandardNumberInsightWithoutRoamingMinimal()
    {
        var expectedUri = $"{this.ApiUrl}/ni/standard/json?number=15555551212&";
        var request = new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };
        this.SetupHttpMock(expectedUri, "TestStandardNIRequestWithoutRoaming");
        var client = this.CreateClient();
        var response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        AssertStandardInsightResponseCommon(response);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
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
        Assert.Equal("Success", response.LookupOutcomeMessage);
        Assert.Equal(0, response.LookupOutcome);
        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Equal("Acme Inc", response.Roaming.RoamingNetworkName);
        Assert.Equal("12345", response.Roaming.RoamingNetworkCode);
        Assert.Equal("US", response.Roaming.RoamingCountryCode);
        Assert.Equal(RoamingStatus.Roaming, response.Roaming.Status);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        Assert.Equal("12345", response.OriginalCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.OriginalCarrier.Name);
        Assert.Equal("GB", response.OriginalCarrier.Country);
        Assert.Equal("mobile", response.OriginalCarrier.NetworkType);
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.InternationalFormatNumber);
        Assert.Equal("07700 900000", response.NationalFormatNumber);
        Assert.Equal("GB", response.CountryCode);
        Assert.Equal("GBR", response.CountryCodeIso3);
        Assert.Equal("United Kingdom", response.CountryName);
        Assert.Equal("44", response.CountryPrefix);
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("0.01500000", response.RefundPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("12345", response.CurrentCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.CurrentCarrier.Name);
        Assert.Equal("GB", response.CurrentCarrier.Country);
        Assert.Equal("mobile", response.CurrentCarrier.NetworkType);
    }

    private static void AssertBasicInsightResponse(BasicInsightResponse response)
    {
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal("ca4f82b6-73aa-43fe-8c52-874fd9ffffff", response.RequestId);
        Assert.Equal("15555551212", response.InternationalFormatNumber);
        Assert.Equal("(555) 555-1212", response.NationalFormatNumber);
        Assert.Equal("US", response.CountryCode);
        Assert.Equal("USA", response.CountryCodeIso3);
        Assert.Equal("United States of America", response.CountryName);
        Assert.Equal("1", response.CountryPrefix);
    }

    private static void AssertStandardInsightResponseCommon(StandardInsightResponse response)
    {
        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Equal(PortedStatus.NotPorted, response.Ported);
        Assert.Equal("12345", response.OriginalCarrier.NetworkCode);
        Assert.Equal("Acme Inc", response.OriginalCarrier.Name);
        Assert.Equal("GB", response.OriginalCarrier.Country);
        Assert.Equal("mobile", response.OriginalCarrier.NetworkType);
        Assert.Equal(0, response.Status);
        Assert.Equal("Success", response.StatusMessage);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.InternationalFormatNumber);
        Assert.Equal("07700 900000", response.NationalFormatNumber);
        Assert.Equal("GB", response.CountryCode);
        Assert.Equal("GBR", response.CountryCodeIso3);
        Assert.Equal("United Kingdom", response.CountryName);
        Assert.Equal("44", response.CountryPrefix);
        Assert.Equal("0.04000000", response.RequestPrice);
        Assert.Equal("0.01500000", response.RefundPrice);
        Assert.Equal("1.23456789", response.RemainingBalance);
    }

    private VonageClient CreateClient() => this.BuildVonageClient(this.BuildCredentialsForBasicAuthentication());

    private string GetResponseJsonWithParameters(Dictionary<string, string> parameters, string filename) =>
        parameters.Aggregate(this.GetResponseJson(filename),
            (current, parameter) => current.Replace($"${parameter.Key}$", parameter.Value));

    private void SetupHttpMock(string expectedUri, string responseFileName = null) =>
        this.Setup(expectedUri,
            responseFileName != null ? this.GetResponseJson(responseFileName) : this.GetResponseJson());
}