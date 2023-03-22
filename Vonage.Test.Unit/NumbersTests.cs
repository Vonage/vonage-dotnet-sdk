using System;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using Vonage.Numbers;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class NumbersTests : TestBase
    {
        private readonly Credentials credentials;
        private readonly VonageClient client;

        public NumbersTests()
        {
            this.credentials = this.BuildCredentials();
            this.client = new VonageClient(this.credentials);
        }

        [Fact]
        public void TestBuyNumber()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = this.client.NumbersClient.BuyANumber(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestBuyNumberAsync()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = await this.client.NumbersClient.BuyANumberAsync(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestBuyNumberAsyncWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var request = new NumberTransactionRequest
                {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            var response = await this.client.NumbersClient.BuyANumberAsync(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public void TestBuyNumberWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var request = new NumberTransactionRequest
                {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            var response = this.client.NumbersClient.BuyANumber(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public void TestCancelNumber()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = this.client.NumbersClient.CancelANumber(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestCancelNumberAsync()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = await this.client.NumbersClient.CancelANumberAsync(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestCancelNumberAsyncWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest
                {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = await this.client.NumbersClient.CancelANumberAsync(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public void TestCancelNumberWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&target_api_key=12345&";
            var expectedUri = $"{this.RestUrl}/number/cancel?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new NumberTransactionRequest
                {Country = "GB", Msisdn = "447700900000", TargetApiKey = "12345"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = this.client.NumbersClient.CancelANumber(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public void TestFailedPurchase()
        {
            const string expectedResponse =
                @"{""error-code"": ""401"",""error-code-label"": ""authentication failed""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/buy?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var request = new NumberTransactionRequest {Country = "GB", Msisdn = "447700900000"};
            Action act = () => this.client.NumbersClient.BuyANumber(request);
            act.Should().ThrowExactly<VonageNumberResponseException>()
                .Which.Response.Should().BeEquivalentTo(new NumberTransactionResponse
                    {ErrorCode = "401", ErrorCodeLabel = "authentication failed"});
        }

        [Fact]
        public void TestSearchNumbers()
        {
            const string expectedResponse =
                @"{""count"": 1234,""numbers"": [{""country"": ""GB"",""msisdn"": ""447700900000"",""type"": ""mobile-lvn"",""cost"": ""1.25"",""features"": [""VOICE"",""SMS""]}]}";
            var expectedUri =
                $"{this.RestUrl}/number/search?country=GB&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var request = new NumberSearchRequest {Country = "GB"};
            this.Setup(expectedUri, expectedResponse);
            var response = this.client.NumbersClient.GetAvailableNumbers(request);
            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Fact]
        public async Task TestSearchNumbersAsync()
        {
            const string expectedResponse =
                @"{""count"": 1234,""numbers"": [{""country"": ""GB"",""msisdn"": ""447700900000"",""type"": ""mobile-lvn"",""cost"": ""1.25"",""features"": [""VOICE"",""SMS""]}]}";
            var expectedUri =
                $"{this.RestUrl}/number/search?country=GB&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var request = new NumberSearchRequest {Country = "GB"};
            this.Setup(expectedUri, expectedResponse);
            var response = await this.client.NumbersClient.GetAvailableNumbersAsync(request);
            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Fact]
        public async Task TestSearchNumbersAsyncWithCredentials()
        {
            const string expectedResponse =
                @"{""count"": 1234,""numbers"": [{""country"": ""GB"",""msisdn"": ""447700900000"",""type"": ""mobile-lvn"",""cost"": ""1.25"",""features"": [""VOICE"",""SMS""]}]}";
            var expectedUri =
                $"{this.RestUrl}/number/search?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var request = new NumberSearchRequest
            {
                Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains,
                Features = "SMS", Size = 10, Index = 1,
            };
            this.Setup(expectedUri, expectedResponse);
            var response = await this.client.NumbersClient.GetAvailableNumbersAsync(request, this.credentials);
            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Fact]
        public void TestSearchNumbersWithCredentials()
        {
            const string expectedResponse =
                @"{""count"": 1234,""numbers"": [{""country"": ""GB"",""msisdn"": ""447700900000"",""type"": ""mobile-lvn"",""cost"": ""1.25"",""features"": [""VOICE"",""SMS""]}]}";
            var expectedUri =
                $"{this.RestUrl}/number/search?country=GB&type=mobile-lvn&pattern=12345&search_pattern=1&features=SMS&size=10&index=1&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var request = new NumberSearchRequest
            {
                Country = "GB", Type = "mobile-lvn", Pattern = "12345", SearchPattern = SearchPattern.Contains,
                Features = "SMS", Size = 10, Index = 1,
            };
            this.Setup(expectedUri, expectedResponse);
            var response = this.client.NumbersClient.GetAvailableNumbers(request, this.credentials);
            var number = response.Numbers[0];
            Assert.Equal(1234, response.Count);
            Assert.Equal("GB", number.Country);
            Assert.Equal("447700900000", number.Msisdn);
            Assert.Equal("mobile-lvn", number.Type);
            Assert.Equal("1.25", number.Cost);
            Assert.Equal("VOICE", number.Features[0]);
            Assert.Equal("SMS", number.Features[1]);
        }

        [Fact]
        public void TestUpdateNumber()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new UpdateNumberRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = this.client.NumbersClient.UpdateANumber(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestUpdateNumberAsync()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            const string expectedRequestContent = "country=GB&msisdn=447700900000&";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var request = new UpdateNumberRequest {Country = "GB", Msisdn = "447700900000"};
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = await this.client.NumbersClient.UpdateANumberAsync(request);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public async Task TestUpdateNumberAsyncWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var expectedRequestContent =
                $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&";
            var request = new UpdateNumberRequest
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
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = await this.client.NumbersClient.UpdateANumberAsync(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        [Fact]
        public void TestUpdateNumberWithCredentials()
        {
            const string expectedResponse = @"{""error-code"": ""200"",""error-code-label"": ""success""}";
            var expectedUri = $"{this.RestUrl}/number/update?api_key={this.ApiKey}&api_secret={this.ApiSecret}";
            var expectedRequestContent =
                $"country=GB&msisdn=447700900000&app_id=aaaaaaaa-bbbb-cccc-dddd-0123456789abc&moHttpUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&" +
                $"moSmppSysType=inbound&voiceCallbackType=tel&voiceCallbackValue=447700900000&voiceStatusCallback={HttpUtility.UrlEncode("https://example.com/webhooks/status")}&";
            var request = new UpdateNumberRequest
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
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var response = this.client.NumbersClient.UpdateANumber(request, this.credentials);
            response.ErrorCode.Should().Be("200");
            response.ErrorCodeLabel.Should().Be("success");
        }

        private Credentials BuildCredentials() => Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
    }
}