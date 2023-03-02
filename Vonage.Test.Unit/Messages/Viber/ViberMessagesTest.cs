using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Messages;
using Vonage.Messages.Viber;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit.Messages.Viber
{
    public class ViberMessagesTest : TestBase
    {
        private readonly SerializationTestHelper helper;
        private readonly string expectedUri;

        public ViberMessagesTest()
        {
            this.expectedUri = $"{this.ApiUrl}/v1/messages";
            this.helper = new SerializationTestHelper(typeof(ViberMessagesTest).Namespace,
                JsonSerializer.BuildWithCamelCase());
        }

        [Fact]
        public async Task SendViberImageAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new ViberImageRequest
            {
                To = "441234567890",
                From = "015417543010",
                Image = new Attachment
                {
                    Url = "https://test.com/image.png",
                },
                ClientRef = "abcdefg",
            };
            var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(credentials);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendViberTextAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new ViberTextRequest
            {
                To = "441234567890",
                From = "015417543010",
                Text = "Hello mum",
                ClientRef = "abcdefg",
            };
            var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(credentials);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }
    }
}