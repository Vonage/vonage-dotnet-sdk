using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vonage.Numbers;
using Xunit;
namespace Vonage.Test.Unit
{
    public class NumbersTests : TestBase
    {
        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void TestSearchNumbers(bool passCreds, bool kitchenSink)
        {
            var expetedResponse = @"{
              ""count"": 1234,
              ""numbers"": [
                {
                  ""country"": ""GB"",
                  ""msisdn"": ""447700900000"",
                  ""type"": ""mobile-lvn"",
                  ""cost"": ""1.25"",
                  ""features"": [
                    ""VOICE"",
                    ""SMS""
                  ]
                }
              ]
            }";
            var expectedUri = $"{RestUrl}/number/search";
            NumberSearchRequest request;
            if (kitchenSink)
            {
                expectedUri += $"?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberSearchRequest { Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains, Features = "SMS", Size = 10, Index = 1 };
            }
            else
            {
                expectedUri += $"?country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberSearchRequest { Country = "GB" };
            }
            Setup(expectedUri, expetedResponse);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            NumbersSearchResponse response;
            if (passCreds)
            {
                response = client.NumbersClient.GetAvailableNumbers(request, creds);
            }
            else
            {
                response = client.NumbersClient.GetAvailableNumbers(request);
            }

            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestSearchNumbersAsync(bool passCreds, bool kitchenSink)
        {
            var expetedResponse = @"{
              ""count"": 1234,
              ""numbers"": [
                {
                  ""country"": ""GB"",
                  ""msisdn"": ""447700900000"",
                  ""type"": ""mobile-lvn"",
                  ""cost"": ""1.25"",
                  ""features"": [
                    ""VOICE"",
                    ""SMS""
                  ]
                }
              ]
            }";
            var expectedUri = $"{RestUrl}/number/search";
            NumberSearchRequest request;
            if (kitchenSink)
            {
                expectedUri += $"?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberSearchRequest { Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains, Features = "SMS", Size = 10, Index = 1 };
            }
            else
            {
                expectedUri += $"?country=GB&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberSearchRequest { Country = "GB" };
            }
            Setup(expectedUri, expetedResponse);
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            NumbersSearchResponse response;
            if (passCreds)
            {
                response = await client.NumbersClient.GetAvailableNumbersAsync(request, creds);
            }
            else
            {
                response = await client.NumbersClient.GetAvailableNumbersAsync(request);
            }

            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void TestBuyNumber(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/buy";
            string expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            NumberTransactionRequest request;

            if (kitchenSink)
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&target_api_key=12345&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000", TargetApiKey="12345" };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
            }
            
            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = client.NumbersClient.BuyANumber(request, creds);
            }
            else
            {
                response = client.NumbersClient.BuyANumber(request);
            }
            
            Assert.Equal("200",response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestBuyNumberAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/buy";
            string expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            NumberTransactionRequest request;

            if (kitchenSink)
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&target_api_key=12345&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345" };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
            }

            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = await client.NumbersClient.BuyANumberAsync(request, creds);
            }
            else
            {
                response = await client.NumbersClient.BuyANumberAsync(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestCancelNumber(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/cancel";
            string expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            NumberTransactionRequest request;

            if (kitchenSink)
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&target_api_key=12345&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345" };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
            }

            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = client.NumbersClient.CancelANumber(request, creds);
            }
            else
            {
                response = client.NumbersClient.CancelANumber(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestCancelNumberAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/cancel";
            string expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            NumberTransactionRequest request;

            if (kitchenSink)
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&target_api_key=12345&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345" };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
            }

            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = await client.NumbersClient.CancelANumberAsync(request, creds);
            }
            else
            {
                response = await client.NumbersClient.CancelANumberAsync(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void TestUpdateNumber(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/update";
            string expectdRequestContent;
            UpdateNumberRequest request;

            if (kitchenSink)
            {                
                expectdRequestContent = $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                    $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&" +
                    $"api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new UpdateNumberRequest 
                { 
                    Country = "GB", 
                    Msisdn = "447700900000", 
                    AppId= "aaaaaaaa-bbbb-cccc-dddd-0123456789abc", 
                    MoHttpUrl= "https://example.com/webhooks/inbound-sms",
                    MoSmppSysType="inbound",
                    VoiceCallbackType="tel",
                    VoiceCallbackValue= "447700900000",
                    VoiceStatusCallback= "https://example.com/webhooks/status"
                };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new UpdateNumberRequest { Country = "GB", Msisdn = "447700900000" };
            }

            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = client.NumbersClient.UpdateANumber(request, creds);
            }
            else
            {
                response = client.NumbersClient.UpdateANumber(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestUpdateNumberAsync(bool passCreds, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{RestUrl}/number/update";
            string expectdRequestContent;
            UpdateNumberRequest request;

            if (kitchenSink)
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                    $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&" +
                    $"api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new UpdateNumberRequest
                {
                    Country = "GB",
                    Msisdn = "447700900000",
                    AppId = "aaaaaaaa-bbbb-cccc-dddd-0123456789abc",
                    MoHttpUrl = "https://example.com/webhooks/inbound-sms",
                    MoSmppSysType = "inbound",
                    VoiceCallbackType = "tel",
                    VoiceCallbackValue = "447700900000",
                    VoiceStatusCallback = "https://example.com/webhooks/status"
                };
            }
            else
            {
                expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
                request = new UpdateNumberRequest { Country = "GB", Msisdn = "447700900000" };
            }

            Setup(expectedUri, expectedResponse, expectdRequestContent);

            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            NumberTransactionResponse response;
            if (passCreds)
            {
                response = await client.NumbersClient.UpdateANumberAsync(request, creds);
            }
            else
            {
                response = await client.NumbersClient.UpdateANumberAsync(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Fact]
        public void TestFailedPurchase()
        {
            var expectedResponse = @"{
              ""error-code"": ""401"",
              ""error-code-label"": ""authentifcation failed""
            }";
            var expectedUri = $"{RestUrl}/number/buy";
            string expectdRequestContent = $"country=GB&msisdn=447700900000&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, expectdRequestContent);
            var request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);
            try
            {
                client.NumbersClient.BuyANumber(request);
                Assert.True(false, "Failin because exception was not thrown");
            }
            catch (VonageNumberResponseException ex)
            {
                Assert.Equal("401", ex.Response.ErrorCode);
                Assert.Equal("authentifcation failed", ex.Response.ErrorCodeLabel);
            }
        }
        
    }
}
