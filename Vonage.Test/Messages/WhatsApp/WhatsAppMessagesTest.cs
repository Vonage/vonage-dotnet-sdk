#region
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Messages;
using Vonage.Messages.WhatsApp;
using Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;
using Vonage.Messages.WhatsApp.ProductMessages.SingleItem;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages.WhatsApp;

[Trait("Category", "Legacy")]
public class WhatsAppMessagesTest : TestBase
{
    private const string ResponseKey = "SendMessage";
    private readonly string expectedResponse;
    private readonly string expectedUri;
    private readonly SerializationTestHelper helper;

    public WhatsAppMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.helper = new SerializationTestHelper(typeof(WhatsAppMessagesTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());
        this.expectedResponse = this.helper.GetResponseJson(ResponseKey);
    }

    [Fact]
    public async Task SendWhatsAppAudioAsyncReturnsOk()
    {
        var request = new WhatsAppAudioRequest
        {
            To = "441234567890",
            From = "015417543010",
            Audio = new Attachment
            {
                Url = "https://test.com/voice.mp3",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppAudioAsyncReturnsOkWithContext()
    {
        var request = new WhatsAppAudioRequest
        {
            To = "441234567890",
            From = "015417543010",
            Audio = new Attachment
            {
                Url = "https://test.com/voice.mp3",
            },
            ClientRef = "abcdefg",
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppCustomAsyncReturnsOk()
    {
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppCustomAsyncReturnsOkWithContext()
    {
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
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppFileAsyncReturnsOk()
    {
        var request = new WhatsAppFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new CaptionedAttachment
            {
                Url = "https://test.com/me.txt",
                Caption = "Me",
                Name = "file.txt",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppFileAsyncReturnsOkWithContext()
    {
        var request = new WhatsAppFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new CaptionedAttachment
            {
                Url = "https://test.com/me.txt",
                Caption = "Me",
                Name = "file.txt",
            },
            ClientRef = "abcdefg",
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppImageAsyncReturnsOk()
    {
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppImageAsyncReturnsOkWithContext()
    {
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
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppMultipleItemsAsyncReturnsOk()
    {
        var request = new WhatsAppMultipleItemsRequest
        {
            To = "441234567890",
            From = "015417543010",
            ClientRef = "abcdefg",
            Custom = MultipleItemsContentBuilder.Initialize()
                .WithHeader("Our top products")
                .WithBody("Check out these great products")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithSection("Cool products")
                .WithProductRetailer("product_1")
                .WithProductRetailer("product_2")
                .WithSection("Awesome products")
                .WithProductRetailer("product_3")
                .Build(),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppMultipleItemsAsyncReturnsOkWithContext()
    {
        var request = new WhatsAppMultipleItemsRequest
        {
            To = "441234567890",
            From = "015417543010",
            ClientRef = "abcdefg",
            Custom = MultipleItemsContentBuilder.Initialize()
                .WithHeader("Our top products")
                .WithBody("Check out these great products")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithSection("Cool products")
                .WithProductRetailer("product_1")
                .WithProductRetailer("product_2")
                .WithSection("Awesome products")
                .WithProductRetailer("product_3")
                .Build(),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppSingleItemAsyncReturnsOk()
    {
        var request = new WhatsAppSingleProductRequest
        {
            To = "441234567890",
            From = "015417543010",
            ClientRef = "abcdefg",
            Custom = SingleItemContentBuilder.Initialize()
                .WithBody("Check out this cool product")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build(),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppSingleItemAsyncReturnsOkWithContext()
    {
        var request = new WhatsAppSingleProductRequest
        {
            To = "441234567890",
            From = "015417543010",
            ClientRef = "abcdefg",
            Custom = SingleItemContentBuilder.Initialize()
                .WithBody("Check out this cool product")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build(),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithIdSticker()
    {
        var request = new WhatsAppStickerRequest<IdSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new IdSticker(new Guid("aabb7a31-1d1f-4755-a574-2971d831cd5b")),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithIdStickerWithContext()
    {
        var request = new WhatsAppStickerRequest<IdSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new IdSticker(new Guid("aabb7a31-1d1f-4755-a574-2971d831cd5b")),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithUrlSticker()
    {
        var request = new WhatsAppStickerRequest<UrlSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new UrlSticker("https://example.com/image.webp"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithUrlStickerWithContext()
    {
        var request = new WhatsAppStickerRequest<UrlSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new UrlSticker("https://example.com/image.webp"),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppTemplateAsyncReturnsOk()
    {
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppTemplateAsyncReturnsOkWithContext()
    {
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
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppTextAsyncReturnsOk()
    {
        var request = new WhatsAppTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppTextAsyncReturnsOkWithContext()
    {
        var request = new WhatsAppTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppVideoAsyncReturnsOk()
    {
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendWhatsAppVideoAsyncReturnsOkWithContext()
    {
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
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task UpdateAsyncReturnsOk()
    {
        this.Setup($"{this.expectedUri}/ID-123", Maybe<string>.None, this.helper.GetRequestJson());
        await this.BuildVonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey))
            .MessagesClient.UpdateAsync(WhatsAppUpdateMessageRequest.Build("ID-123"));
    }

    private async Task VerifySendMessage(string expectedRequest, IMessage request)
    {
        this.Setup(this.expectedUri, this.expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}