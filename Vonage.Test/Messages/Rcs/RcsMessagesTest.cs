#region
using System;
using System.Collections.Generic;
using System.Linq;
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
[Trait("Product", "Messages.Rcs")]
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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
            TrustedRecipient = true,
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

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenRcsRequestIsValid() =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = "Hello"}
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenFromIsNullOrEmpty(string from) =>
        new RcsTextRequest {To = "447700900000", From = from, Text = "Hello"}
            .GetErrors().Should().Contain("From must not be null or empty.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenToIsNullOrEmpty(string to) =>
        new RcsTextRequest {To = to, From = "Vonage", Text = "Hello"}
            .GetErrors().Should().Contain("To must not be null or empty.");

    [Theory]
    [InlineData("1")]
    [InlineData("123456")]
    public void GetErrors_ReturnsError_WhenToIsTooShort(string to) =>
        new RcsTextRequest {To = to, From = "Vonage", Text = "Hello"}
            .GetErrors().Should().Contain("To length must be between 7 and 15 characters.");

    [Fact]
    public void GetErrors_ReturnsError_WhenToIsTooLong() =>
        new RcsTextRequest {To = "1234567890123456", From = "Vonage", Text = "Hello"}
            .GetErrors().Should().Contain("To length must be between 7 and 15 characters.");

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789012345")]
    public void GetErrors_ReturnsEmpty_WhenToIsAtLengthBoundary(string to) =>
        new RcsTextRequest {To = to, From = "Vonage", Text = "Hello"}
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(0)]
    [InlineData(20)]
    [InlineData(259200)]
    public void GetErrors_ReturnsEmpty_WhenTtlIsValid(int ttl) =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = "Hello", TimeToLive = ttl}
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(1)]
    [InlineData(19)]
    [InlineData(259201)]
    [InlineData(int.MaxValue)]
    public void GetErrors_ReturnsError_WhenTtlIsOutOfRange(int ttl) =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = "Hello", TimeToLive = ttl}
            .GetErrors().Should().Contain("TimeToLive must be between 20 and 259200.");

    [Theory]
    [InlineData("v0.1")]
    [InlineData("v1")]
    public void GetErrors_ReturnsEmpty_WhenWebhookVersionIsValid(string version) =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = "Hello", WebhookVersion = version}
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData("v2")]
    [InlineData("invalid")]
    [InlineData("V1")]
    public void GetErrors_ReturnsError_WhenWebhookVersionIsInvalid(string version) =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = "Hello", WebhookVersion = version}
            .GetErrors().Should().Contain("WebhookVersion must be 'v0.1' or 'v1'.");

    [Fact]
    public void GetErrors_ReturnsMultipleErrors_WhenMultipleCommonFieldsAreInvalid()
    {
        var errors = new RcsTextRequest {TimeToLive = 5}.GetErrors().ToList();
        errors.Should().Contain("From must not be null or empty.");
        errors.Should().Contain("To must not be null or empty.");
        errors.Should().Contain("Text must not be null or empty.");
        errors.Should().Contain("TimeToLive must be between 20 and 259200.");
    }

    // ── RcsTextRequest ─────────────────────────────────────────────────────────

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsText_GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new RcsTextRequest {To = "447700900000", From = "Vonage", Text = text}
            .GetErrors().Should().Contain("Text must not be null or empty.");

    [Fact]
    public void RcsText_GetErrors_ReturnsError_WhenSuggestionsExceedLimit() =>
        new RcsTextRequest
        {
            To = "447700900000", From = "Vonage", Text = "Hello",
            Suggestions = Enumerable.Repeat(new ReplySuggestion("Yes", "yes"), 12).ToArray(),
        }.GetErrors().Should().Contain("Suggestions must contain at most 11 items.");

    [Fact]
    public void RcsText_GetErrors_ReturnsError_WhenSuggestionIsInvalid() =>
        new RcsTextRequest
        {
            To = "447700900000", From = "Vonage", Text = "Hello",
            Suggestions = [new ReplySuggestion(new string('a', 26), "yes")],
        }.GetErrors().Should().Contain("Text must not exceed 25 characters.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardSuggestionIsInvalid() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg"))
                with {Suggestions = [new ReplySuggestion(new string('a', 26), "yes")]},
        }.GetErrors().Should().Contain("Text must not exceed 25 characters.");

    [Fact]
    public void RcsText_GetErrors_ReturnsEmpty_WhenSuggestionsAreAtLimit() =>
        new RcsTextRequest
        {
            To = "447700900000", From = "Vonage", Text = "Hello",
            Suggestions = Enumerable.Repeat(new ReplySuggestion("Yes", "yes"), 11).ToArray(),
        }.GetErrors().Should().BeEmpty();

    // ── RcsImageRequest ────────────────────────────────────────────────────────

    [Fact]
    public void RcsImage_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsImageRequest
                {To = "447700900000", From = "Vonage", Image = new CaptionedAttachment {Url = "https://example.com/image.jpg"}}
            .GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsImage_GetErrors_ReturnsError_WhenImageIsNull() =>
        new RcsImageRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("Image must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsImage_GetErrors_ReturnsError_WhenImageUrlIsNullOrEmpty(string url) =>
        new RcsImageRequest {To = "447700900000", From = "Vonage", Image = new CaptionedAttachment {Url = url}}
            .GetErrors().Should().Contain("Image Url must not be null or empty.");

    // ── RcsVideoRequest ────────────────────────────────────────────────────────

    [Fact]
    public void RcsVideo_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsVideoRequest
                {To = "447700900000", From = "Vonage", Video = new CaptionedAttachment {Url = "https://example.com/video.mp4"}}
            .GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsVideo_GetErrors_ReturnsError_WhenVideoIsNull() =>
        new RcsVideoRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("Video must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsVideo_GetErrors_ReturnsError_WhenVideoUrlIsNullOrEmpty(string url) =>
        new RcsVideoRequest {To = "447700900000", From = "Vonage", Video = new CaptionedAttachment {Url = url}}
            .GetErrors().Should().Contain("Video Url must not be null or empty.");

    // ── RcsFileRequest ─────────────────────────────────────────────────────────

    [Fact]
    public void RcsFile_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsFileRequest
                {To = "447700900000", From = "Vonage", File = new CaptionedAttachment {Url = "https://example.com/file.pdf"}}
            .GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsFile_GetErrors_ReturnsError_WhenFileIsNull() =>
        new RcsFileRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("File must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsFile_GetErrors_ReturnsError_WhenFileUrlIsNullOrEmpty(string url) =>
        new RcsFileRequest {To = "447700900000", From = "Vonage", File = new CaptionedAttachment {Url = url}}
            .GetErrors().Should().Contain("File Url must not be null or empty.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg")),
        }.GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardIsNull() =>
        new RcsCardRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("Card must not be null.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsCard_GetErrors_ReturnsError_WhenCardTitleIsNullOrEmpty(string title) =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment(title, "Text", new Uri("https://example.com/image.jpg")),
        }.GetErrors().Should().Contain("Card Title must not be null or empty.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardTitleExceedsMaxLength() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment(new string('a', 201), "Text", new Uri("https://example.com/image.jpg")),
        }.GetErrors().Should().Contain("Card Title must not exceed 200 characters.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RcsCard_GetErrors_ReturnsError_WhenCardTextIsNullOrEmpty(string text) =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", text, new Uri("https://example.com/image.jpg")),
        }.GetErrors().Should().Contain("Card Text must not be null or empty.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardTextExceedsMaxLength() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", new string('a', 2001), new Uri("https://example.com/image.jpg")),
        }.GetErrors().Should().Contain("Card Text must not exceed 2000 characters.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardMediaUrlIsNull() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", null),
        }.GetErrors().Should().Contain("Card MediaUrl must not be null.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenCardSuggestionsExceedLimit() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg"))
                with {Suggestions = Enumerable.Repeat(new ReplySuggestion("Yes", "yes"), 5).ToArray()},
        }.GetErrors().Should().Contain("Card Suggestions must contain at most 4 items.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsError_WhenHorizontalOrientationWithoutImageAlignment() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg")),
            Rcs = new MessageRcs {CardOrientation = RcsCardOrientation.Horizontal},
        }.GetErrors().Should().Contain("ImageAlignment is required when CardOrientation is Horizontal.");

    [Fact]
    public void RcsCard_GetErrors_ReturnsEmpty_WhenHorizontalOrientationWithImageAlignment() =>
        new RcsCardRequest
        {
            To = "447700900000", From = "Vonage",
            Card = new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg")),
            Rcs = new MessageRcs {CardOrientation = RcsCardOrientation.Horizontal, ImageAlignment = RcsImageAlignment.Right},
        }.GetErrors().Should().BeEmpty();

    // ── RcsCarouselRequest ─────────────────────────────────────────────────────

    [Fact]
    public void RcsCarousel_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsCarouselRequest
        {
            To = "447700900000", From = "Vonage",
            Carousel = new CarouselAttachment(new CardAttachment("Title", "Text",
                new Uri("https://example.com/image.jpg"))),
        }.GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsCarousel_GetErrors_ReturnsError_WhenCarouselIsNull() =>
        new RcsCarouselRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("Carousel must not be null.");

    [Fact]
    public void RcsCarousel_GetErrors_ReturnsError_WhenCardsExceedMaxItems()
    {
        var cards = Enumerable
            .Repeat(new CardAttachment("Title", "Text", new Uri("https://example.com/image.jpg")), 11)
            .ToArray();
        new RcsCarouselRequest {To = "447700900000", From = "Vonage", Carousel = new CarouselAttachment(cards)}
            .GetErrors().Should().Contain("Carousel must contain at most 10 cards.");
    }

    [Fact]
    public void RcsCarousel_GetErrors_ReturnsError_WhenCardInCarouselIsInvalid() =>
        new RcsCarouselRequest
        {
            To = "447700900000", From = "Vonage",
            Carousel = new CarouselAttachment(new CardAttachment(null, "Text",
                new Uri("https://example.com/image.jpg"))),
        }.GetErrors().Should().Contain("Card Title must not be null or empty.");

    [Fact]
    public void RcsCarousel_GetErrors_ReturnsError_WhenSuggestionsExceedLimit() =>
        new RcsCarouselRequest
        {
            To = "447700900000", From = "Vonage",
            Carousel = new CarouselAttachment(new CardAttachment("Title", "Text",
                new Uri("https://example.com/image.jpg"))),
            Suggestions = Enumerable.Repeat(new ReplySuggestion("Yes", "yes"), 12).ToArray(),
        }.GetErrors().Should().Contain("Suggestions must contain at most 11 items.");

    // ── RcsCustomRequest ───────────────────────────────────────────────────────

    [Fact]
    public void RcsCustom_GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new RcsCustomRequest {To = "447700900000", From = "Vonage", Custom = new { }}
            .GetErrors().Should().BeEmpty();

    [Fact]
    public void RcsCustom_GetErrors_ReturnsError_WhenCustomIsNull() =>
        new RcsCustomRequest {To = "447700900000", From = "Vonage"}
            .GetErrors().Should().Contain("Custom must not be null.");

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