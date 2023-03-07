using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Common.Test;
using Vonage.Messages.Sms;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit.Messages.Sms
{
    public class SmsMessagesTest : TestBase
    {
        private readonly SerializationTestHelper helper;
        private readonly string expectedUri;

        public SmsMessagesTest()
        {
            this.expectedUri = $"{this.ApiUrl}/v1/messages";
            this.helper =
                new SerializationTestHelper(typeof(SmsMessagesTest).Namespace, JsonSerializer.BuildWithCamelCase());
        }

        [Fact]
        public async Task SendSmsAsyncReturnsInvalidCredentials()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new SmsRequest
            {
                To = "441234567890",
                From = "015417543010",
                Text = "This is a test",
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest, HttpStatusCode.Unauthorized);
            var client = new VonageClient(creds);
            var exception =
                await Assert.ThrowsAsync<VonageHttpRequestException>(async () =>
                    await client.MessagesClient.SendAsync(request));
            Assert.NotNull(exception);
            Assert.Equal(expectedResponse, exception.Json);
        }

        [Fact]
        public async Task SendSmsAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new SmsRequest
            {
                To = "441234567890",
                From = "015417543010",
                Text = "This is a test",
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }
    }
}