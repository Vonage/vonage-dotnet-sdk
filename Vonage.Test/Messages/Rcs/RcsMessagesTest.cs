#region
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.Rcs;
using Vonage.Messages.Rcs.Suggestions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Rcs;

[Trait("Category", "Legacy")]
public class RcsMessagesTest : IDisposable
{
    private const string ResponseKey = "SendMessage";
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(RcsMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendFullRcsCardAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCardRequest
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
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsCarouselAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCarouselRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Rcs = new MessageRcs
            {
                Category = "category",
                CardWidth = RcsCardWidth.Small,
            },
            Carousel = new CarouselAttachment(new CardAttachment("Card Title",
                    "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ReplySuggestion("Yes", "question_1_yes"))),
            TrustedNumber = true,
            Suggestions = [new ReplySuggestion("Yes", "question_1_yes")],
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsCustomAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCustomRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Custom = new {Key1 = "value1", Key2 = "value2"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsFileAsyncReturnsOk() =>
        await this.AssertResponse(new RcsFileRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            File = new CaptionedAttachment {Url = "https://example.com/file.pdf"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsImageAsyncReturnsOk() =>
        await this.AssertResponse(new RcsImageRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Image = new CaptionedAttachment {Url = "https://example.com/image.jpg"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsTextAsyncReturnsOk() =>
        await this.AssertResponse(new RcsTextRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Text = "Hello world",
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendFullRcsVideoAsyncReturnsOk() =>
        await this.AssertResponse(new RcsVideoRequest
        {
            To = "447700900000",
            ClientRef = "abc123",
            WebhookUrl = new Uri("https://example.com/status"),
            TimeToLive = 600,
            From = "Vonage",
            Video = new CaptionedAttachment {Url = "https://example.com/video.mp4"},
            Rcs = new MessageRcs {Category = "category"},
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                new Uri("https://example.com/image.jpg")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithCreateCalendarEventSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new CreateCalendarEventSuggestion("Option 1", "action_1",
                    DateTime.Parse("2023-01-01T10:00:00Z"), DateTime.Parse("2023-01-01T10:00:00Z"), "New Year Party",
                    "Description")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithDialSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new DialSuggestion("Option 1", "action_1", "14155550100")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithOpenUrlSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new OpenUrlSuggestion("Option 1", "action_1", new Uri("https://example.com"),
                    "Description")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithOpenWebviewUrlSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new OpenWebviewUrlSuggestion("Option 1", "action_1", new Uri("https://example.com"),
                    "Description")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithReplySuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ReplySuggestion("Yes", "question_1_yes")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithShareLocationSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ShareLocationSuggestion("Option 1", "action_1")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCardAsyncReturnsOkWithViewLocationSuggestion() =>
        await this.AssertResponse(new RcsCardRequest
        {
            To = "447700900000",
            From = "Vonage",
            Card = new CardAttachment("Card Title", "This is some text to display on the card.",
                    new Uri("https://example.com/image.jpg"))
                .AppendSuggestion(new ViewLocationSuggestion("Option 1", "action_1", "37.7749", "-122.4194", "vonage")),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCarouselAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCarouselRequest
        {
            To = "447700900000",
            From = "Vonage",
            Carousel = new CarouselAttachment(new CardAttachment("Card Title",
                "This is some text to display on the card.",
                new Uri("https://example.com/image.jpg"))),
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsCustomAsyncReturnsOk() =>
        await this.AssertResponse(new RcsCustomRequest
        {
            To = "447700900000",
            From = "Vonage",
            Custom = new {Key1 = "value1", Key2 = "value2"},
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsFileAsyncReturnsOk() =>
        await this.AssertResponse(new RcsFileRequest
        {
            To = "447700900000",
            From = "Vonage",
            File = new CaptionedAttachment {Url = "https://example.com/file.pdf"},
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsImageAsyncReturnsOk() =>
        await this.AssertResponse(new RcsImageRequest
        {
            To = "447700900000",
            From = "Vonage",
            Image = new CaptionedAttachment {Url = "https://example.com/image.jpg"},
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsTextAsyncReturnsOk() =>
        await this.AssertResponse(new RcsTextRequest
        {
            To = "447700900000",
            From = "Vonage",
            Text = "Hello world",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsTextAsyncReturnsOk_WithSuggestions() =>
        await this.AssertResponse(new RcsTextRequest
        {
            To = "447700900000",
            From = "Vonage",
            Text = "Hello world",
            Suggestions =
            [
                new CreateCalendarEventSuggestion("Option 1", "action_1",
                    DateTime.Parse("2023-01-01T10:00:00Z"), DateTime.Parse("2023-01-01T10:00:00Z"), "New Year Party",
                    "Description"),
                new ReplySuggestion("Yes", "question_1_yes"),
            ],
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendRcsVideoAsyncReturnsOk() =>
        await this.AssertResponse(new RcsVideoRequest
        {
            To = "447700900000",
            From = "Vonage",
            Video = new CaptionedAttachment {Url = "https://example.com/video.mp4"},
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task UpdateAsyncReturnsOk()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.context.VonageClient.MessagesClient.UpdateAsync(RcsUpdateMessageRequest.Build("ID-123"));
    }

    private async Task AssertResponse(IMessage request, string expectedRequest)
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