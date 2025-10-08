#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Messages;
using Vonage.Messages.Rcs;
using Vonage.Messages.Rcs.Suggestions;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs;

[Trait("Category", "Legacy")]
public class RcsMessagesTest : TestBase
{
    private const string ResponseKey = "SendMessage";
    private readonly VonageClient client;
    private readonly string expectedResponse;
    private readonly string expectedUri;

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(RcsMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public RcsMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.client = this.BuildVonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
        this.expectedResponse = this.helper.GetResponseJson(ResponseKey);
    }

    [Fact]
    public async Task SendFullRcsCardAsyncReturnsOk()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Rcs = new MessageRcs
            {
                Category = "category",
                CardOrientation = RcsCardOrientation.Vertical,
                ImageAlignment = RcsImageAlignment.Right,
            },
            TrustedNumber = true,
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .ForceMediaRefresh()
                .WithMediaDescription("Image description for accessibility purposes.")
                .WithMediaHeight(CardAttachment.Height.Short)
                .WithThumbnailUrl(new Uri("https://example.com/thumbnail.jpg")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendFullRcsCustomAsyncReturnsOk()
    {
        var request = new RcsCustomRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Custom = new {Key1 = "value1", Key2 = "value2"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendFullRcsFileAsyncReturnsOk()
    {
        var request = new RcsFileRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            File = new CaptionedAttachment {Url = "https://example.com/file.pdf"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendFullRcsImageAsyncReturnsOk()
    {
        var request = new RcsImageRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Image = new CaptionedAttachment {Url = "https://example.com/image.jpg"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendFullRcsTextAsyncReturnsOk()
    {
        var request = new RcsTextRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Text = "Hello world",
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendFullRcsVideoAsyncReturnsOk()
    {
        var request = new RcsVideoRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Video = new CaptionedAttachment {Url = "https://example.com/video.mp4"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOk()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                new Uri("https://example.com/image.jpg")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithCreateCalendarEventSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new CreateCalendarEventSuggestion("Option 1", "action_1",
                    DateTime.Parse("2023-01-01T10:00:00Z"), DateTime.Parse("2023-01-01T10:00:00Z"), "New Year Party")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithDialSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new DialSuggestion("Option 1", "action_1", "14155550100")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithOpenUrlSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new OpenUrlSuggestion("Option 1", "action_1", new Uri("https://example.com"))),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithOpenWebviewUrlSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new OpenWebviewUrlSuggestion("Option 1", "action_1", new Uri("https://example.com"))),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithReplySuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ReplySuggestion("Yes", "question_1_yes")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithShareLocationSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ShareLocationSuggestion("Option 1", "action_1")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithViewLocationSuggestion()
    {
        var request = new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ViewLocationSuggestion("Option 1", "action_1", "37.7749", "-122.4194", "vonage")),
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsCustomAsyncReturnsOk()
    {
        var request = new RcsCustomRequest
        {
            To = "447700900000",
            From = "Vonage",
            Custom = new {Key1 = "value1", Key2 = "value2"},
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsFileAsyncReturnsOk()
    {
        var request = new RcsFileRequest
        {
            To = "447700900000",
            From = "Vonage",
            File = new CaptionedAttachment {Url = "https://example.com/file.pdf"},
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsImageAsyncReturnsOk()
    {
        var request = new RcsImageRequest
        {
            To = "447700900000",
            From = "Vonage",
            Image = new CaptionedAttachment {Url = "https://example.com/image.jpg"},
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsTextAsyncReturnsOk()
    {
        var request = new RcsTextRequest
        {
            To = "447700900000",
            From = "Vonage",
            Text = "Hello world",
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendRcsVideoAsyncReturnsOk()
    {
        var request = new RcsVideoRequest
        {
            To = "447700900000",
            From = "Vonage",
            Video = new CaptionedAttachment {Url = "https://example.com/video.mp4"},
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task UpdateAsyncReturnsOk()
    {
        this.Setup($"{this.expectedUri}/ID-123", Maybe<string>.None, this.helper.GetRequestJson());
        await this.client.MessagesClient.UpdateAsync(RcsUpdateMessageRequest.Build("ID-123"));
    }

    private async Task AssertResponse(IMessage request, string expectedRequest)
    {
        this.Setup(this.expectedUri, this.expectedResponse, expectedRequest);
        var response = await this.client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}