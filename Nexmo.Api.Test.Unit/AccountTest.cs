using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class AccountTest : TestBase
    {
        [Fact]
        public void GetAccountBalance()
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/get-balance?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""value"": 3.14159, ""autoReload"": false }";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var balance = client.AccountClient.GetAccountBalance();

            //ASSERT
            Assert.Equal(3.14159m, balance.Value);
            Assert.False(balance.AutoReload);
        }
        
        [Fact]
        public void SetSettings()
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/settings";
            var expectedRequestContents = $"moCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&drCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/delivery-receipt")}&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""mo-callback-url"": ""https://example.com/webhooks/inbound-sms"",""dr-callback-url"": ""https://example.com/webhooks/delivery-receipt"",""max-outbound-request"": 15,""max-inbound-request"": 30,""max-calls-per-second"": 4}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent, requestContent: expectedRequestContents);

            //ACT
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var result = client.AccountClient.ChangeAccountSettings(new Accounts.AccountSettingsRequest { MoCallBackUrl = "https://example.com/webhooks/inbound-sms", DrCallBackUrl = "https://example.com/webhooks/delivery-receipt" });

            //ASSERT
            Assert.Equal("https://example.com/webhooks/delivery-receipt", result.DrCallbackurl);
            Assert.Equal("https://example.com/webhooks/inbound-sms", result.MoCallbackUrl);
            Assert.Equal(4, result.MaxCallsPerSecond);
            Assert.Equal(30, result.MaxInboundRequest);
            Assert.Equal(15, result.MaxOutboundRequest);
        }

        [Fact]
        public void TopUp()
        {
            //ARRANGE            
            var expectedUri = $"{RestUrl}/account/top-up?trx=00X123456Y7890123Z&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""response"":""abc123""}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //Act
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var response = client.AccountClient.TopUpAccountBalance(new Accounts.TopUpRequest { Trx = "00X123456Y7890123Z" });

            Assert.Equal("abc123",response.Response);
        }

        [Fact]
        public void GetNumbers()
        {
            //ARRANGE
            var expectedUri = $"{RestUrl}/account/numbers?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = @"{""count"":1,""numbers"":[{""country"":""US"",""msisdn"":""17775551212"",""type"":""mobile-lvn"",""features"":[""VOICE"",""SMS""]}]}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //Act
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var numbers = client.NumbersClient.GetOwnedNumbers(new Numbers.NumberSearchRequest());

            //ASSERT
            Assert.Equal(1, numbers.Count);
            Assert.Equal("17775551212", numbers.Numbers[0].Msisdn);
            Assert.Equal("US", numbers.Numbers[0].Country);
            Assert.Equal("mobile-lvn", numbers.Numbers[0].Type);
            Assert.Equal("VOICE", numbers.Numbers[0].Features.First());
        }
    }
}
