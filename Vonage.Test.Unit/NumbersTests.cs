using System.Web;
using Vonage.Numbers;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class NumbersTests : TestBase
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestBuyNumber(bool passCredentials, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            NumberTransactionRequest request;
            if (kitchenSink)
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
                request = new NumberTransactionRequest
                    {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = client.NumbersClient.BuyANumber(request, credentials);
            }
            else
            {
                response = client.NumbersClient.BuyANumber(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void TestBuyNumberAsync(bool passCredentials, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            NumberTransactionRequest request;
            if (kitchenSink)
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
                request = new NumberTransactionRequest
                    {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = await client.NumbersClient.BuyANumberAsync(request, credentials);
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
        public void TestCancelNumber(bool passCredentials, bool kitchenSink)
        {
            var expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            NumberTransactionRequest request;
            if (kitchenSink)
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
                request = new NumberTransactionRequest
                    {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = client.NumbersClient.CancelANumber(request, credentials);
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
        public async void TestCancelNumberAsync(bool passCredentials, bool kitchenSink)
        {
            const string expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            NumberTransactionRequest request;
            if (kitchenSink)
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
                request = new NumberTransactionRequest
                    {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = await client.NumbersClient.CancelANumberAsync(request, credentials);
            }
            else
            {
                response = await client.NumbersClient.CancelANumberAsync(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }

        [Fact]
        public void TestFailedPurchase()
        {
            const string expectedResponse = @"{
              ""error-code"": ""401"",
              ""error-code-label"": ""authentifcation failed""
            }";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var expectedRequestContent = "country=GB&msisdn=447700900000&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
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

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestSearchNumbers(bool passCredentials, bool kitchenSink)
        {
            const string expectedResponse = @"{
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
            var expectedUri = $"{this.RestUrl}/number/search";
            NumberSearchRequest request;
            if (kitchenSink)
            {
                expectedUri +=
                    $"?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
                request = new NumberSearchRequest
                {
                    Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains,
                    Features = "SMS", Size = 10, Index = 1,
                };
            }
            else
            {
                expectedUri += $"?country=GB&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
                request = new NumberSearchRequest {Country = "GB"};
            }

            this.Setup(expectedUri, expectedResponse);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumbersSearchResponse response;
            if (passCredentials)
            {
                response = client.NumbersClient.GetAvailableNumbers(request, credentials);
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
        public async void TestSearchNumbersAsync(bool passCredentials, bool kitchenSink)
        {
            const string expectedResponse = @"{
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
            var expectedUri = $"{this.RestUrl}/number/search";
            NumberSearchRequest request;
            if (kitchenSink)
            {
                expectedUri +=
                    $"?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
                request = new NumberSearchRequest
                {
                    Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains,
                    Features = "SMS", Size = 10, Index = 1,
                };
            }
            else
            {
                expectedUri += $"?country=GB&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
                request = new NumberSearchRequest {Country = "GB"};
            }

            this.Setup(expectedUri, expectedResponse);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumbersSearchResponse response;
            if (passCredentials)
            {
                response = await client.NumbersClient.GetAvailableNumbersAsync(request, credentials);
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
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void TestUpdateNumber(bool passCredentials, bool kitchenSink)
        {
            const string expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            UpdateNumberRequest request;
            if (kitchenSink)
            {
                expectedRequestContent =
                    $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                    $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&";
                request = new UpdateNumberRequest
                {
                    Country = "GB",
                    Msisdn = "447700900000",
                    AppId = "aaaaaaaa-bbbb-cccc-dddd-0123456789abc",
                    MoHttpUrl = "https://example.com/webhooks/inbound-sms",
                    MoSmppSysType = "inbound",
                    VoiceCallbackType = "tel",
                    VoiceCallbackValue = "447700900000",
                    VoiceStatusCallback = "https://example.com/webhooks/status",
                };
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new UpdateNumberRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = client.NumbersClient.UpdateANumber(request, credentials);
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
        public async void TestUpdateNumberAsync(bool passCredentials, bool kitchenSink)
        {
            const string expectedResponse = @"{
              ""error-code"": ""200"",
              ""error-code-label"": ""success""
            }";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            string expectedRequestContent;
            UpdateNumberRequest request;
            if (kitchenSink)
            {
                expectedRequestContent =
                    $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                    $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&";
                request = new UpdateNumberRequest
                {
                    Country = "GB",
                    Msisdn = "447700900000",
                    AppId = "aaaaaaaa-bbbb-cccc-dddd-0123456789abc",
                    MoHttpUrl = "https://example.com/webhooks/inbound-sms",
                    MoSmppSysType = "inbound",
                    VoiceCallbackType = "tel",
                    VoiceCallbackValue = "447700900000",
                    VoiceStatusCallback = "https://example.com/webhooks/status",
                };
            }
            else
            {
                expectedRequestContent = "country=GB&msisdn=447700900000&";
                request = new UpdateNumberRequest {Country = "GB", Msisdn = "447700900000"};
            }

            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var credentials = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = new VonageClient(credentials);
            NumberTransactionResponse response;
            if (passCredentials)
            {
                response = await client.NumbersClient.UpdateANumberAsync(request, credentials);
            }
            else
            {
                response = await client.NumbersClient.UpdateANumberAsync(request);
            }

            Assert.Equal("200", response.ErrorCode);
            Assert.Equal("success", response.ErrorCodeLabel);
        }
    }
}