#region
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vonage.NumberInsights;
using Vonage.Request;
using Xunit;
#endregion

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class NumberInsightsTests : TestBase
{
    [Fact]
    public async Task AdvancedNIRequestSyncWithNotRoamingStatus()
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?number=971639946111&";
        var request = new AdvancedNumberInsightRequest
        {
            Number = "971639946111",
        };
        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        AdvancedInsightsResponse response;
        response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);

        //ASSERT
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

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestAdvancedAsync(bool passCreds, bool kitchenSink)
    {
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/advanced/async/json";
        AdvancedNumberInsightAsynchronousRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                $"?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
            request = new AdvancedNumberInsightAsynchronousRequest
            {
                Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
                Callback = "https://example.com/callback",
            };
        }
        else
        {
            expectedUri +=
                $"?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
            request = new AdvancedNumberInsightAsynchronousRequest
            {
                Number = "15555551212",
                Callback = "https://example.com/callback",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        AdvancedInsightsAsynchronousResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);
        }

        //ASSERT
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.Number);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("0.01500000", response.RequestPrice);
        Assert.Equal(0, response.Status);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestAdvancedAsyncAsync(bool passCreds, bool kitchenSink)
    {
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/advanced/async/json";
        AdvancedNumberInsightAsynchronousRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                $"?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
            request = new AdvancedNumberInsightAsynchronousRequest
            {
                Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
                Callback = "https://example.com/callback",
            };
        }
        else
        {
            expectedUri +=
                $"?callback={WebUtility.UrlEncode("https://example.com/callback")}&number=15555551212&";
            request = new AdvancedNumberInsightAsynchronousRequest
            {
                Number = "15555551212",
                Callback = "https://example.com/callback",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        AdvancedInsightsAsynchronousResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);
        }

        //ASSERT
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
        Assert.Equal("447700900000", response.Number);
        Assert.Equal("1.23456789", response.RemainingBalance);
        Assert.Equal("0.01500000", response.RequestPrice);
        Assert.Equal(0, response.Status);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestAdvancedNIRequestSyncAsync(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json";
        AdvancedNumberInsightRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                $"?ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
            request = new AdvancedNumberInsightRequest
                {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new AdvancedNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        AdvancedInsightsResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        }

        //ASSERT
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task TestAdvancedNIRequestSyncRealTimeData(bool active)
    {
        //ARRANGE
        var activeStatus = active ? "active" : "inactive";
        var responseData = new Dictionary<string, string>
        {
            {"active_status", activeStatus},
        };
        var expectedResponse = this.GetResponseJson(responseData);
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json";
        expectedUri +=
            $"?ip={WebUtility.UrlEncode("123.0.0.255")}&real_time_data=true&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
            RealTimeData = true,
        };
        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);

        //ASSERT
        Assert.Equal(NumberReachability.Reachable, response.Reachable);
        Assert.Equal(NumberValidity.valid, response.ValidNumber);
        Assert.NotNull(response.RealTimeData);
        Assert.Equal(active, response.RealTimeData.ActiveStatus);
        Assert.Equal("on", response.RealTimeData.HandsetStatus);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public async Task TestAdvancedNIRequestSyncWithNullableValues(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/advanced/json";
        AdvancedNumberInsightRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                "?ip=123.0.0.255&cnam=true&number=15555551212&country=GB&";
            request = new AdvancedNumberInsightRequest
                {Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255"};
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new AdvancedNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        AdvancedInsightsResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
        }

        //ASSERT
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

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestBasicNIRequestAsync(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedUri = $"{this.ApiUrl}/ni/basic/json";
        BasicNumberInsightRequest request;
        var expectedResponseContent = this.GetResponseJson();
        if (kitchenSink)
        {
            expectedUri += "?number=15555551212&country=GB&";
            request = new BasicNumberInsightRequest
            {
                Country = "GB",
                Number = "15555551212",
            };
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new BasicNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponseContent);

        //ACT
        BasicInsightResponse response;
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightBasicAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightBasicAsync(request);
        }

        //ASSERT
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

    [Fact]
    public async Task TestFailedAdvancedRequest()
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/json?number=15555551212&";
        var request = new AdvancedNumberInsightRequest {Number = "15555551212"};
        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        try
        {
            await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);

            //ASSERT
            Assert.True(false, "Auto fail because request returned without throwing exception");
        }
        catch (VonageNumberInsightResponseException ex)
        {
            //ASSERT
            Assert.Equal(4, ex.Response.Status);
            Assert.Equal("invalid credentials", ((AdvancedInsightsResponse) ex.Response).StatusMessage);
        }
    }

    [Fact]
    public async Task TestFailedAsyncRequest()
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri =
            $"{this.ApiUrl}/ni/advanced/async/json?callback={WebUtility.UrlEncode("https://example.com/callback")}&ip={WebUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&";
        var request = new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        try
        {
            await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);

            //ASSERT
            Assert.True(false, "Auto fail because request returned without throwing exception");
        }
        catch (VonageNumberInsightResponseException ex)
        {
            //ASSERT
            Assert.Equal(4, ex.Response.Status);
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestStandardNIRequestAsync(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/standard/json";
        StandardNumberInsightRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                "?cnam=true&number=15555551212&country=GB&";
            request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new StandardNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        StandardInsightResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        }

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

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestStandardNIRequestWithNullCarrier(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedResponse = @"{
              ""status"": 0,
              ""status_message"": ""Success"",
              ""request_id"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""international_format_number"": ""447700900000"",
              ""national_format_number"": ""07700 900000"",
              ""country_code"": ""GB"",
              ""country_code_iso3"": ""GBR"",
              ""country_name"": ""United Kingdom"",
              ""country_prefix"": ""44"",
              ""request_price"": ""0.04000000"",
              ""refund_price"": ""0.01500000"",
              ""remaining_balance"": ""1.23456789"",
              ""current_carrier"": {
                ""network_code"": null,
                ""name"": null,
                ""country"": null,
                ""network_type"": null
              },
              ""original_carrier"": {
                ""network_code"": ""12345"",
                ""name"": ""Acme Inc"",
                ""country"": ""GB"",
                ""network_type"": ""mobile""
              },
              ""ported"": ""not_ported"",
              ""roaming"": ""unknown"",
              ""caller_identity"": {
                            ""caller_type"": ""consumer"",
                ""caller_name"": ""John Smith"",
                ""first_name"": ""John"",
                ""last_name"": ""Smith""
              },
              ""caller_name"": ""John Smith"",
              ""last_name"": ""Smith"",
              ""first_name"": ""John"",
              ""caller_type"": ""consumer""
            }";
        var expectedUri = $"{this.ApiUrl}/ni/standard/json";
        StandardNumberInsightRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                "?cnam=true&number=15555551212&country=GB&";
            request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new StandardNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        StandardInsightResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        }

        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
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
        Assert.Null(response.CurrentCarrier.NetworkCode);
        Assert.Null(response.CurrentCarrier.Name);
        Assert.Null(response.CurrentCarrier.Country);
        Assert.Null(response.CurrentCarrier.NetworkType);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task TestStandardNIRequestWithoutRoaming(bool passCreds, bool kitchenSink)
    {
        //ARRANGE
        var expectedResponse = this.GetResponseJson();
        var expectedUri = $"{this.ApiUrl}/ni/standard/json";
        StandardNumberInsightRequest request;
        if (kitchenSink)
        {
            expectedUri +=
                "?cnam=true&number=15555551212&country=GB&";
            request = new StandardNumberInsightRequest {Cnam = true, Country = "GB", Number = "15555551212"};
        }
        else
        {
            expectedUri += "?number=15555551212&";
            request = new StandardNumberInsightRequest
            {
                Number = "15555551212",
            };
        }

        this.Setup(expectedUri, expectedResponse);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        StandardInsightResponse response;
        if (passCreds)
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request, creds);
        }
        else
        {
            response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
        }

        Assert.Equal("John", response.FirstName);
        Assert.Equal(CallerType.consumer, response.CallerType);
        Assert.Equal("Smith", response.LastName);
        Assert.Equal("John Smith", response.CallerName);
        Assert.Equal("Smith", response.CallerIdentity.LastName);
        Assert.Equal("John", response.CallerIdentity.FirstName);
        Assert.Equal("John Smith", response.CallerIdentity.CallerName);
        Assert.Equal(CallerType.consumer, response.CallerIdentity.CallerType);
        Assert.Equal(RoamingStatus.Unknown, response.Roaming.Status);
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
}