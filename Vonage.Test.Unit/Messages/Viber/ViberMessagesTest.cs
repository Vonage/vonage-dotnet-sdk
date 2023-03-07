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
        public async Task SendViberFileAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new ViberFileRequest
            {
                To = "441234567890",
                From = "015417543010",
                ClientRef = "abcdefg",
                Data = new ViberRequestData
                {
                    Category = ViberMessageCategory.Transaction,
                    Type = "string",
                    TTL = 600,
                },
                File = new ViberFileRequest.FileInformation
                {
                    Url = "https://example.com/files/",
                    Name = "example.pdf",
                },
            };
            var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(credentials);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("d1159a25-f64a-4d0e-8cf1-9896b760f3e4"), response.MessageUuid);
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
                Image = new CaptionedAttachment
                {
                    Url = "https://test.com/image.png",
                    Caption = "Check out this new promotion",
                },
                ClientRef = "abcdefg",
                Data = new ViberRequestData
                {
                    Category = ViberMessageCategory.Transaction,
                    TTL = 600,
                    Type = "string",
                    Action = new ViberAction("https://example.com/page1.html", "Find out more"),
                },
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
                Data = new ViberRequestData
                {
                    Category = ViberMessageCategory.Transaction,
                    TTL = 600,
                    Type = "string",
                    Action = new ViberAction("https://example.com/page1.html", "Find out more"),
                },
            };
            var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(credentials);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendViberVideoAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new ViberVideoRequest
            {
                To = "441234567890",
                From = "015417543010",
                ClientRef = "abcdefg",
                Data = new ViberRequestData
                {
                    Category = ViberMessageCategory.Transaction,
                    Duration = "123",
                    Type = "string",
                    FileSize = "1",
                    TTL = 600,
                },
                Video = new ViberVideoRequest.VideoInformation
                {
                    Url = "https://example.com/image.jpg",
                    Caption = "Check out this new video",
                    ThumbUrl = "https://example.com/file1.jpg",
                },
            };
            var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(credentials);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("d1159a25-f64a-4d0e-8cf1-9896b760f3e4"), response.MessageUuid);
        }
    }
}