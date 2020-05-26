using Newtonsoft.Json;
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
                Type = Messaging.SmsType.text,
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

        [Fact]
        public void TestDlrStruct()
        {
            var jsonFromNDP = @"{
                  ""msisdn"": ""447700900000"",
                  ""to"": ""AcmeInc"",
                  ""network-code"": ""12345"",
                  ""messageId"": ""0A0000001234567B"",
                  ""price"": ""0.03330000"",
                  ""status"": ""delivered"",
                  ""scts"": ""2001011400"",
                  ""err-code"": ""0"",
                  ""api-key"": ""abcd1234"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": 1582650446,
                  ""nonce"": ""ec11dd3e-1e7f-4db5-9467-82b02cd223b9"",
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C""
                }";
            var dlr = JsonConvert.DeserializeObject<Messaging.DeliveryReceipt>(jsonFromNDP);
            Assert.Equal("447700900000", dlr.Msisdn);
            Assert.Equal("AcmeInc", dlr.To);
            Assert.Equal("12345", dlr.NetworkCode);
            Assert.Equal("0A0000001234567B", dlr.MessageId);
            Assert.Equal("0.03330000", dlr.Price);
            Assert.Equal(Messaging.DlrStatus.delivered, dlr.Status);
            Assert.Equal("2001011400", dlr.Scts);
            Assert.Equal("0", dlr.ErrorCode);
            Assert.Equal("abcd1234", dlr.ApiKey);
            Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
            Assert.True(1582650446 == dlr.Timestamp);
            Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
            Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
        }

        [Fact]
        public void TestInboundSmsStruct()
        {
            var jsonFromNdp = @"{
                  ""api-key"": ""abcd1234"",
                  ""msisdn"": ""447700900001"",
                  ""to"": ""447700900000"",
                  ""messageId"": ""0A0000000123ABCD1"",
                  ""text"": ""Hello world"",
                  ""type"": ""text"",
                  ""keyword"": ""HELLO"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": ""1578787200"",
                  ""nonce"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                  ""concat"": ""true"",
                  ""concat-ref"": ""1"",
                  ""concat-total"": ""3"",
                  ""concat-part"": ""2"",
                  ""data"": ""abc123"",
                  ""udh"": ""abc123""
                }";
            var inboundSms = JsonConvert.DeserializeObject<Messaging.InboundSms>(jsonFromNdp);
            Assert.Equal("abcd1234", inboundSms.ApiKey);
            Assert.Equal("447700900001", inboundSms.Msisdn);
            Assert.Equal("447700900000", inboundSms.To);
            Assert.Equal("0A0000000123ABCD1", inboundSms.MessageId);
            Assert.Equal("Hello world", inboundSms.Text);
            Assert.Equal("text", inboundSms.Type);
            Assert.Equal("HELLO", inboundSms.Keyword);
            Assert.Equal("2020-01-01T12:00:00.000+00:00", inboundSms.MessageTimestamp);
            Assert.Equal("1578787200", inboundSms.Timestamp);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", inboundSms.Nonce);
            Assert.Equal("true", inboundSms.Concat);
            Assert.Equal("1", inboundSms.ConcatRef);
            Assert.Equal("3", inboundSms.ConcatTotal);
            Assert.Equal("2", inboundSms.ConcatPart);
            Assert.Equal("abc123", inboundSms.Data);
            Assert.Equal("abc123", inboundSms.Udh);
        }

        [Fact]
        public void TestValidateSignature()
        {
            var inboundSmsShell = new Messaging.InboundSms
            {
                ApiKey = "abcd1234",
                Msisdn = "447700900001",
                To = "447700900000",
                MessageId = "0A0000000123ABCD1",
                Text = "Hello world",
                Keyword = "HELLO",
                MessageTimestamp = "2020-01-01T12:00:00.000+00:00",
                Timestamp = "1578787200",
                Nonce = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                Concat = "true",
                ConcatRef = "3",
            };
            var TestSigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";
            var json = JsonConvert.SerializeObject(inboundSmsShell, Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            inboundSmsShell.Sig = Cryptography.SmsSignatureGenerator.GenerateSignature(Messaging.InboundSms.ConstructSignatureStringFromDictionary(dict),TestSigningSecret,Cryptography.SmsSignatureGenerator.Method.md5);
            Assert.True(inboundSmsShell.ValidateSignature(TestSigningSecret, Cryptography.SmsSignatureGenerator.Method.md5));
        }
    }
}

