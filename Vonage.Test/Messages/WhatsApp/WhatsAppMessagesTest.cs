#region
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.WhatsApp;
using Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;
using Vonage.Messages.WhatsApp.ProductMessages.SingleItem;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.WhatsApp;

[Trait("Category", "Legacy")]
public class WhatsAppMessagesTest : IDisposable
{
    private const string ResponseKey = "SendMessage";
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(WhatsAppMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendWhatsAppAudioAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppAudioRequest
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
        });

    [Fact]
    public async Task SendWhatsAppAudioAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppAudioRequest
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
        });

    [Fact]
    public async Task SendWhatsAppCustomAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppCustomRequest
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
        });

    [Fact]
    public async Task SendWhatsAppCustomAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppCustomRequest
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
        });

    [Fact]
    public async Task SendWhatsAppFileAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppFileRequest
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
        });

    [Fact]
    public async Task SendWhatsAppFileAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppFileRequest
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
        });

    [Fact]
    public async Task SendWhatsAppImageAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppImageRequest
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
        });

    [Fact]
    public async Task SendWhatsAppImageAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppImageRequest
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
        });

    [Fact]
    public async Task SendWhatsAppMultipleItemsAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppMultipleItemsRequest
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
        });

    [Fact]
    public async Task SendWhatsAppMultipleItemsAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppMultipleItemsRequest
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
        });

    [Fact]
    public async Task SendWhatsAppSingleItemAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppSingleProductRequest
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
        });

    [Fact]
    public async Task SendWhatsAppSingleItemAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppSingleProductRequest
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
        });

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithIdSticker() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppStickerRequest<IdSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new IdSticker(new Guid("aabb7a31-1d1f-4755-a574-2971d831cd5b")),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithIdStickerWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppStickerRequest<IdSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new IdSticker(new Guid("aabb7a31-1d1f-4755-a574-2971d831cd5b")),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithUrlSticker() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppStickerRequest<UrlSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new UrlSticker("https://example.com/image.webp"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppStickerAsyncReturnsOkWithUrlStickerWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppStickerRequest<UrlSticker>
        {
            To = "447700900000",
            From = "447700900001",
            ClientRef = "string",
            Sticker = new UrlSticker("https://example.com/image.webp"),
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppTemplateAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppTemplateRequest
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
        });

    [Fact]
    public async Task SendWhatsAppTemplateAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppTemplateRequest
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
        });

    [Fact]
    public async Task SendWhatsAppTextAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppTextAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
            Context = new WhatsAppContext("a1b2c3d4a1b2c3d4"),
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendWhatsAppVideoAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppVideoRequest
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
        });

    [Fact]
    public async Task SendWhatsAppVideoAsyncReturnsOkWithContext() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new WhatsAppVideoRequest
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
        });

    [Fact]
    public async Task UpdateAsyncReturnsOk()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.context.VonageClient
            .MessagesClient.UpdateAsync(WhatsAppUpdateMessageRequest.Build("ID-123"));
    }

    [Fact]
    public async Task UpdateAsyncWithReplyingIndicatorReturnsOk()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.context.VonageClient
            .MessagesClient
            .UpdateAsync(WhatsAppUpdateMessageRequest.Build("ID-123", new WhatsAppReplyingIndicator(true)));
    }

    private async Task VerifySendMessage(string expectedRequest, IMessage request)
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/messages")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(expectedRequest)
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.helper.GetResponseJson(ResponseKey)));
        var response = await this.context.VonageClient.MessagesClient.SendAsync(request);
        response.Should().BeEquivalentTo(new MessagesResponse(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")));
    }
}