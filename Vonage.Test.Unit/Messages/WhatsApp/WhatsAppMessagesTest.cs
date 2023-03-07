using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Messages;
using Vonage.Messages.WhatsApp;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit.Messages.WhatsApp
{
    public class WhatsAppMessagesTest : TestBase
    {
        private readonly SerializationTestHelper helper;
        private readonly string expectedUri;

        public WhatsAppMessagesTest()
        {
            this.expectedUri = $"{this.ApiUrl}/v1/messages";
            this.helper = new SerializationTestHelper(typeof(WhatsAppMessagesTest).Namespace,
                JsonSerializer.BuildWithCamelCase());
        }

        [Fact]
        public async Task SendWhatsAppAudioAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppAudioRequest
            {
                To = "441234567890",
                From = "015417543010",
                Audio = new Attachment
                {
                    Url = "https://test.com/voice.mp3",
                },
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppCustomAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppCustomRequest
            {
                To = "441234567890",
                From = "015417543010",
                ClientRef = "abcdefg",
                Custom = new
                {
                    type = "template",
                    template = new
                    {
                        something = "whatsapp:hsm:technology:nexmo",
                        name = "parcel_location",
                    },
                },
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppFileAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppFileRequest
            {
                To = "441234567890",
                From = "015417543010",
                File = new CaptionedAttachment
                {
                    Url = "https://test.com/me.txt",
                    Caption = "Me",
                },
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppImageAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppImageRequest
            {
                To = "441234567890",
                From = "015417543010",
                Image = new CaptionedAttachment
                {
                    Url = "https://test.com/image.png",
                    Caption = "Testing image caption",
                },
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppStickerAsyncReturnsOkWithIdSticker()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppStickerRequest<IdSticker>
            {
                To = "447700900000",
                From = "447700900001",
                ClientRef = "string",
                Sticker = new IdSticker(new Guid("aabb7a31-1d1f-4755-a574-2971d831cd5b")),
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("d1159a25-f64a-4d0e-8cf1-9896b760f3e4"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppStickerAsyncReturnsOkWithUrlSticker()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppStickerRequest<UrlSticker>
            {
                To = "447700900000",
                From = "447700900001",
                ClientRef = "string",
                Sticker = new UrlSticker("https://example.com/image.webp"),
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("d1159a25-f64a-4d0e-8cf1-9896b760f3e4"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppTemplateAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppTemplateRequest
            {
                To = "441234567890",
                From = "015417543010",
                Template = new MessageTemplate
                {
                    Name = "Amazing template",
                    Parameters = new List<object>
                    {
                        new {@default = "Vonage Verification"},
                        new {@default = "64873"},
                        new {@default = "10"},
                    },
                },
                ClientRef = "abcdefg",
                WhatsApp = new MessageWhatsApp
                {
                    Policy = "deterministic",
                    Locale = "en-GB",
                },
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppTextAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppTextRequest
            {
                To = "441234567890",
                From = "015417543010",
                Text = "Hello mum",
                ClientRef = "abcdefg",
            };
            var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
            this.Setup(this.expectedUri, expectedResponse, expectedRequest);
            var client = new VonageClient(creds);
            var response = await client.MessagesClient.SendAsync(request);
            Assert.NotNull(response);
            Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
        }

        [Fact]
        public async Task SendWhatsAppVideoAsyncReturnsOk()
        {
            var expectedResponse = this.helper.GetResponseJson();
            var expectedRequest = this.helper.GetRequestJson();
            var request = new WhatsAppVideoRequest
            {
                To = "441234567890",
                From = "015417543010",
                Video = new CaptionedAttachment
                {
                    Url = "https://test.com/me.mp4",
                    Caption = "Me at the zoo",
                },
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