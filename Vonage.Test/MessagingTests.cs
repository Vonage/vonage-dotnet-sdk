﻿#region
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Messaging;
using Vonage.Request;
using Vonage.Serialization;
using Xunit;
#endregion

namespace Vonage.Test
{
    [Trait("Category", "Legacy")]
    public class MessagingTests : TestBase
    {
        [Fact]
        public async Task NullMessagesResponse()
        {
            var expectedResponse = @"";
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent =
                $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
            var exception = await Assert.ThrowsAsync<VonageSmsResponseException>(() =>
                client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                    {From = "AcmeInc", To = "447700900000", Text = "Hello World!"}));
            Assert.NotNull(exception);
            Assert.Equal("Encountered an Empty SMS response", exception.Message);
        }

        [Fact]
        public async Task SendSmsAsyncBadResponse()
        {
            var expectedResponse = this.GetResponseJson();
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent =
                $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
            var exception = await Assert.ThrowsAsync<VonageSmsResponseException>(async () =>
                await client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                    {From = "AcmeInc", To = "447700900000", Text = "Hello World!"}));
            Assert.NotNull(exception);
            Assert.Equal(
                $"SMS Request Failed with status: {exception.Response.Messages[0].Status} and error message: {exception.Response.Messages[0].ErrorText}",
                exception.Message);
            Assert.Equal(SmsStatusCode.InvalidCredentials, exception.Response.Messages[0].StatusCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task SendSmsAsyncWithAllPropertiesSet(bool passCreds)
        {
            var expectedResponse = this.GetResponseJson();
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent = $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}" +
                                         $"&ttl=900000&status-report-req=true&callback={WebUtility.UrlEncode("https://example.com/sms-dlr")}&message-class=0" +
                                         "&type=text&body=638265253311&udh=06050415811581&protocol-id=127" +
                                         $"&client-ref=my-personal-reference&account-ref=customer1234&entity-id=testEntity&content-id=testcontent&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var request = new SendSmsRequest
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
                Ttl = 900000,
                Type = SmsType.Text,
                Udh = "06050415811581",
                ContentId = "testcontent",
                EntityId = "testEntity",
            };
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(creds);
            var response = passCreds
                ? await client.SmsClient.SendAnSmsAsync(request, creds)
                : await client.SmsClient.SendAnSmsAsync(request);
            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
            response.Messages[0].ClientRef.Should().Be("my-personal-reference");
        }

        [Fact]
        public async Task SendSmsTypicalUsage()
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
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent =
                $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
            var response = await client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                {From = "AcmeInc", To = "447700900000", Text = "Hello World!"});
            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
        }

        [Fact]
        public async Task SendSmsTypicalUsageSimplifiedAsync()
        {
            var expectedResponse = this.GetResponseJson();
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent =
                $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}&type=text&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
            var response = await client.SmsClient.SendAnSmsAsync("AcmeInc", "447700900000", "Hello World!");
            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
        }

        [Fact]
        public async Task SendSmsUnicode()
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
            var expectedUri = $"{this.RestUrl}/sms/json";
            var expectedRequestContent =
                $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("こんにちは世界")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            this.Setup(expectedUri, expectedResponse, expectedRequestContent);
            var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
            var response = await client.SmsClient.SendAnSmsAsync(new SendSmsRequest
                {From = "AcmeInc", To = "447700900000", Text = "こんにちは世界"});
            Assert.Equal("1", response.MessageCount);
            Assert.Equal("447700900000", response.Messages[0].To);
            Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
            Assert.Equal("0", response.Messages[0].Status);
            Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
            Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
            Assert.Equal("12345", response.Messages[0].Network);
            Assert.Equal("customer1234", response.Messages[0].AccountRef);
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
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
            var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP);
            Assert.Equal("447700900000", dlr.Msisdn);
            Assert.Equal("AcmeInc", dlr.To);
            Assert.Equal("12345", dlr.NetworkCode);
            Assert.Equal("0A0000001234567B", dlr.MessageId);
            Assert.Equal("0.03330000", dlr.Price);
            Assert.Equal(DlrStatus.delivered, dlr.Status);
            Assert.Equal("2001011400", dlr.Scts);
            Assert.Equal("0", dlr.ErrorCode);
            Assert.Equal("abcd1234", dlr.ApiKey);
            Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
            Assert.True(1582650446 == dlr.Timestamp);
            Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
            Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
            Assert.Equal("steve", dlr.ClientRef);
        }

        [Fact]
        public void TestDlrStructCamelCaseIgnore()
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
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
            var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP,
                VonageSerialization.SerializerSettings);
            Assert.Equal("447700900000", dlr.Msisdn);
            Assert.Equal("AcmeInc", dlr.To);
            Assert.Equal("12345", dlr.NetworkCode);
            Assert.Equal("0A0000001234567B", dlr.MessageId);
            Assert.Equal("0.03330000", dlr.Price);
            Assert.Equal(DlrStatus.delivered, dlr.Status);
            Assert.Equal("2001011400", dlr.Scts);
            Assert.Equal("0", dlr.ErrorCode);
            Assert.Equal("abcd1234", dlr.ApiKey);
            Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
            Assert.True(1582650446 == dlr.Timestamp);
            Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
            Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
            Assert.Equal("steve", dlr.ClientRef);
        }

        [Fact]
        public void TestDlrStructNoStatus()
        {
            var jsonFromNDP = @"{
                  ""msisdn"": ""447700900000"",
                  ""to"": ""AcmeInc"",
                  ""network-code"": ""12345"",
                  ""messageId"": ""0A0000001234567B"",
                  ""price"": ""0.03330000"",                  
                  ""scts"": ""2001011400"",
                  ""err-code"": ""0"",
                  ""api-key"": ""abcd1234"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": 1582650446,
                  ""nonce"": ""ec11dd3e-1e7f-4db5-9467-82b02cd223b9"",
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
            var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP);
            Assert.Equal("447700900000", dlr.Msisdn);
            Assert.Equal("AcmeInc", dlr.To);
            Assert.Equal("12345", dlr.NetworkCode);
            Assert.Equal("0A0000001234567B", dlr.MessageId);
            Assert.Equal("0.03330000", dlr.Price);
            Assert.Equal(DlrStatus.unknown, dlr.Status);
            Assert.Equal("2001011400", dlr.Scts);
            Assert.Equal("0", dlr.ErrorCode);
            Assert.Equal("abcd1234", dlr.ApiKey);
            Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
            Assert.True(1582650446 == dlr.Timestamp);
            Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
            Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
            Assert.Equal("steve", dlr.ClientRef);
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
            var inboundSms = JsonConvert.DeserializeObject<InboundSms>(jsonFromNdp);
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
            Assert.True(inboundSms.Concat);
            Assert.Equal("1", inboundSms.ConcatRef);
            Assert.Equal("3", inboundSms.ConcatTotal);
            Assert.Equal("2", inboundSms.ConcatPart);
            Assert.Equal("abc123", inboundSms.Data);
            Assert.Equal("abc123", inboundSms.Udh);
        }
    }
}