using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Nexmo.Api.NumberInsights;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class NumberInsightsTests : TestBase
    {
        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void TestBasicNIRequest(bool passCreds, bool kitchenSink)
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/ni/basic/json";            
            BasicNumberInsightRequest request;
            var expectedResponseContent = "{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"ca4f82b6-73aa-43fe-8c52-874fd9ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\"," +
                "\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\"}";

            if (kitchenSink)
            {
                expectedUri+=$"?number=15555551212&country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new BasicNumberInsightRequest
                {
                    Country = "GB",
                    Number = "15555551212"
                };
            }
            else
            {
                expectedUri += $"?number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new BasicNumberInsightRequest
                {
                    Number = "15555551212"
                };
            }
            Setup(expectedUri, expectedResponseContent);

            //ACT
            BasicInsightResponse response;
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            if (passCreds)
            {
                response = client.NumberInsightClient.GetNumberInsightBasic(request, creds);
            }
            else
            {
                response = client.NumberInsightClient.GetNumberInsightBasic(request);
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

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestStandardNIRequest(bool passCreds, bool kitchenSink)
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
                ""network_code"": ""12345"",
                ""name"": ""Acme Inc"",
                ""country"": ""GB"",
                ""network_type"": ""mobile""
              },
              ""original_carrier"": {
                ""network_code"": ""12345"",
                ""name"": ""Acme Inc"",
                ""country"": ""GB"",
                ""network_type"": ""mobile""
              },
              ""ported"": ""not_ported"",
              ""roaming"": {
                            ""status"": ""roaming"",
                ""roaming_country_code"": ""US"",
                ""roaming_network_code"": ""12345"",
                ""roaming_network_name"": ""Acme Inc""
              },
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
            var expectedUri = $"{ApiUrl}/ni/standard/json";
            StandardNumberInsightRequest request;
            if (kitchenSink)
            {
                expectedUri+= $"?cnam=true&number=15555551212&country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new StandardNumberInsightRequest { Cnam = true, Country = "GB", Number = "15555551212" };
            }
            else
            {
                expectedUri += $"?number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new StandardNumberInsightRequest
                {
                    Number = "15555551212"
                };
            }

            Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            StandardInsightResponse response;
            if (passCreds)
            {
                response = client.NumberInsightClient.GetNumberInsightStandard(request, creds);
            }
            else
            {
                response = client.NumberInsightClient.GetNumberInsightStandard(request);
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
            Assert.Equal(RoamingStatus.roaming, response.Roaming.Status);
            Assert.Equal(PortedStatus.not_ported, response.Ported);
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
        public void TestAdvancedNIRequestSync(bool passCreds, bool kitchenSink)
        {//ARRANGE
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
                ""network_code"": ""12345"",
                ""name"": ""Acme Inc"",
                ""country"": ""GB"",
                ""network_type"": ""mobile""
              },
              ""original_carrier"": {
                ""network_code"": ""12345"",
                ""name"": ""Acme Inc"",
                ""country"": ""GB"",
                ""network_type"": ""mobile""
              },
              ""ported"": ""not_ported"",
              ""roaming"": {
                            ""status"": ""roaming"",
                ""roaming_country_code"": ""US"",
                ""roaming_network_code"": ""12345"",
                ""roaming_network_name"": ""Acme Inc""
              },
              ""caller_identity"": {
                            ""caller_type"": ""consumer"",
                ""caller_name"": ""John Smith"",
                ""first_name"": ""John"",
                ""last_name"": ""Smith""
              },
              ""caller_name"": ""John Smith"",
              ""last_name"": ""Smith"",
              ""first_name"": ""John"",
              ""caller_type"": ""consumer"",
              ""lookup_outcome"": 0,
              ""lookup_outcome_message"": ""Success"",
              ""valid_number"": ""valid"",
              ""reachable"": ""reachable""
            }";

            var expectedUri = $"{ApiUrl}/ni/advanced/json";
            AdvancedNumberInsightRequest request;
            if (kitchenSink)
            {
                expectedUri += $"?ip={HttpUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new AdvancedNumberInsightRequest { Cnam = true, Country = "GB", Number = "15555551212" , Ip="123.0.0.255"};
            }
            else
            {
                expectedUri += $"?number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new AdvancedNumberInsightRequest
                {
                    Number = "15555551212"
                };
            }

            Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            AdvancedInsightsResponse response;
            if (passCreds)
            {
                response = client.NumberInsightClient.GetNumberInsightAdvanced(request, creds);
            }
            else
            {
                response = client.NumberInsightClient.GetNumberInsightAdvanced(request);
            }

            //ASSERT
            Assert.Equal(NumberReachability.reachable, response.Reachable);
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
            Assert.Equal(RoamingStatus.roaming, response.Roaming.Status);
            Assert.Equal(PortedStatus.not_ported, response.Ported);
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
        public void TestAdvancedAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""request_id"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""number"": ""447700900000"",
              ""remaining_balance"": ""1.23456789"",
              ""request_price"": ""0.01500000"",
              ""status"": 0
            }";
            var expectedUri = $"{ApiUrl}/ni/advanced/async/json";
            AdvancedNumberInsightAsynchronousRequest request;            
            if (kitchenSink)
            {
                expectedUri += $"?callback={HttpUtility.UrlEncode("https://example.com/callback")}&ip={HttpUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new AdvancedNumberInsightAsynchronousRequest { Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255", Callback= "https://example.com/callback" };
            }
            else
            {
                expectedUri += $"?callback={HttpUtility.UrlEncode("https://example.com/callback")}&number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new AdvancedNumberInsightAsynchronousRequest
                {
                    Number = "15555551212",
                    Callback = "https://example.com/callback"
                };
            }
            Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            AdvancedInsightsAsyncResponse response;
            if (passCreds)
            {
                response = client.NumberInsightClient.GetNumberInsightAsync(request, creds);
            }
            else
            {
                response = client.NumberInsightClient.GetNumberInsightAsync(request);
            }

            //ASSERT
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", response.RequestId);
            Assert.Equal("447700900000", response.Number);
            Assert.Equal("1.23456789", response.RemainingBalance);
            Assert.Equal("0.01500000", response.RequestPrice);
            Assert.Equal(0, response.Status);
        }

        [Fact]
        public void TestFailedAsyncRequest()
        {
            //ARRANGE
            var expectedResponse = @"{
              ""status"": 4
            }";
            var expectedUri = $"{ApiUrl}/ni/advanced/async/json?callback={HttpUtility.UrlEncode("https://example.com/callback")}&ip={HttpUtility.UrlEncode("123.0.0.255")}&cnam=true&number=15555551212&country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
            var request = new AdvancedNumberInsightAsynchronousRequest { Cnam = true, Country = "GB", Number = "15555551212", Ip = "123.0.0.255", Callback = "https://example.com/callback" };
            Setup(expectedUri, expectedResponse);
            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            try
            {
                client.NumberInsightClient.GetNumberInsightAsync(request);
                //ASSERT
                Assert.True(false, "Auto fail because request returned without throwing exception");
            }
            catch (NumberInsights.NexmoNumberInsightResponseException ex) 
            {
                //ASSERT
                Assert.Equal(4, ex.Response.Status);
            }
        }

        [Fact]
        public void TestFailedAdvancedRequest()
        {
            //ARRANGE
            var expectedResponse = @"{
              ""status"": 4,
              ""status_message"":""invalid credentials""
            }";
            var expectedUri = $"{ApiUrl}/ni/advanced/json?number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
            var request = new AdvancedNumberInsightRequest { Number = "15555551212"};
            Setup(expectedUri, expectedResponse);
            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new NexmoClient(creds);
            try
            {
                client.NumberInsightClient.GetNumberInsightAdvanced(request);
                //ASSERT
                Assert.True(false, "Auto fail because request returned without throwing exception");
            }
            catch (NumberInsights.NexmoNumberInsightResponseException ex)
            {
                //ASSERT
                Assert.Equal(4, ex.Response.Status);
                Assert.Equal("invalid credentials", ((AdvancedInsightsResponse)ex.Response).StatusMessage);
            }
            
        }
    }
}
