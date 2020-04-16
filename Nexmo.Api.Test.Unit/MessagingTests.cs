using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class MessagingTests : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void KitcenSinkSendSms(bool passCreds)
        {
            var expectedResponse = @"{
                  ""message-count"": ""1"",
                  ""messages"": [
                    {
                      ""to"": ""447700900000"",
                      ""message-id"": ""0A0000000123ABCD1"",
                      ""status"": ""0"",
                      ""remaining-balance"": ""3.14159265"",
                      ""message-price"": ""0.03330000"",
                      ""network"": ""12345"",
                      ""account-ref"": ""customer1234""
                    }
                  ]
                }";
            var expectedUri = $"{RestUrl}/sms/json?";
            var expectedRequestContent = $"from=AcmeInc&to=447700900000&text={HttpUtility.UrlEncode("Hello World!")}" +
                $"&ttl=900000&status-report-req=true&callback={HttpUtility.UrlEncode("https://example.com/sms-dlr")}&message-class=0" +
                $"&type=text&vcard=none&vcal=none&body=638265253311&udh=06050415811581&protocol-id=127&title=welcome&url={HttpUtility.UrlEncode("https://example.com")}" +
                $"&validity=300000&client-ref=my-personal-reference&account-ref=customer1234&api_key={ApiKey}&api_secret={ApiSecret}&";
            var request = new Messaging.SendSmsRequest
            {
                AccountRef = "customer1234",
                Body = "638265253311",
                Callback = "https://example.com/sms-dlr",
                ClientRef = "my-personal-reference",
                From = "AcmeInc",
                To = "447700900000",
                MessageClass = 0,
                ProtocolId = 127,
                StatusReportReq = true,
                Text = "Hello World!",
                Title = "welcome",
                Ttl = 900000,
                Type = "text",
                Udh = "06050415811581",
                Validity = "300000",
                Vcal = "none",
                Vcard = "none",
                Url = "https://example.com"
                

            };
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = new NexmoClient(creds);
            Messaging.SendSmsResponse response;
            if (passCreds)
            {
                response = client.SmsClient.SendAnSms(request, creds);
            }
            else
            {
                response = client.SmsClient.SendAnSms(request);
            }

            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
        }

        [Fact]        
        public void SendSmsTypicalUsage()
        {
            var expectedResponse = @"{
                  ""message-count"": ""1"",
                  ""messages"": [
                    {
                      ""to"": ""447700900000"",
                      ""message-id"": ""0A0000000123ABCD1"",
                      ""status"": ""0"",
                      ""remaining-balance"": ""3.14159265"",
                      ""message-price"": ""0.03330000"",
                      ""network"": ""12345"",
                      ""account-ref"": ""customer1234""
                    }
                  ]
                }";
            var expectedUri = $"{RestUrl}/sms/json?";
            var expectedRequestContent = $"from=AcmeInc&to=447700900000&text={HttpUtility.UrlEncode("Hello World!")}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            var response = client.SmsClient.SendAnSms(new Messaging.SendSmsRequest { From = "AcmeInc", To = "447700900000", Text = "Hello World!" });
            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
        }

        [Fact]
        public void SendSmsBadResponse()
        {
            var expectedResponse = @"{
                  ""message-count"": ""1"",
                  ""messages"": [
                    {                      
                      ""status"": ""4"",
                      ""error-text"":""invalid credentials""
                    }
                  ]
                }";
            var expectedUri = $"{RestUrl}/sms/json?";
            var expectedRequestContent = $"from=AcmeInc&to=447700900000&text={HttpUtility.UrlEncode("Hello World!")}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            try
            {
                var response = client.SmsClient.SendAnSms(new Messaging.SendSmsRequest { From = "AcmeInc", To = "447700900000", Text = "Hello World!" });
                Assert.True(false);
            }
            catch(Messaging.NexmoSmsResponseException nex)
            {
                Assert.Equal($"SMS Request Failed with status: {nex.Response.Messages[0].Status} and error message: {nex.Response.Messages[0].ErrorText}", nex.Message);
            }
        }

        [Fact]
        public void NullMessagesResponse()
        {
            var expectedResponse = @"";
            var expectedUri = $"{RestUrl}/sms/json?";
            var expectedRequestContent = $"from=AcmeInc&to=447700900000&text={HttpUtility.UrlEncode("Hello World!")}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = new NexmoClient(Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret));
            try
            {
                var response = client.SmsClient.SendAnSms(new Messaging.SendSmsRequest { From = "AcmeInc", To = "447700900000", Text = "Hello World!" });
                Assert.True(false);
            }
            catch (Messaging.NexmoSmsResponseException nex)
            {
                Assert.Equal($"Encountered an Empty SMS response", nex.Message);
            }
        }
    }
}
