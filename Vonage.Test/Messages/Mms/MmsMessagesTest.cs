#region
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.Mms;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Mms;

[Trait("Category", "Legacy")]
public class MmsMessagesTest : IDisposable
{
    private const string ResponseKey = "SendMessage";
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(MmsMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendMmsAudioAsyncReturnsOk() =>
        await this.AssertResponse(new MmsAudioRequest
        {
            To = "441234567890",
            From = "015417543010",
            Audio = new CaptionedAttachment
            {
                Url = "https://test.com/me.mp3",
                Caption = "Sounds I make",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsAudioAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsAudioRequest
        {
            To = "441234567890",
            From = "015417543010",
            Audio = new CaptionedAttachment
            {
                Url = "https://test.com/me.mp3",
                Caption = "Sounds I make",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsContentAsyncReturnsOk() =>
        await this.AssertResponse(new MmsContentRequest
        {
            To = "441234567890",
            From = "015417543010",
            Content = new[]
            {
                new Attachment
                {
                    Type = "image",
                    Url = "https://example.com/image.jpg",
                    Caption = "See the attached image",
                },
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsContentAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsContentRequest
        {
            To = "441234567890",
            From = "015417543010",
            Content = new[]
            {
                new Attachment
                {
                    Type = "image",
                    Url = "https://example.com/image.jpg",
                    Caption = "See the attached image",
                },
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsFileAsyncReturnsOk() =>
        await this.AssertResponse(new MmsFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new Attachment
            {
                Url = "https://example.com/file.pdf",
                Caption = "Please see the attached file.",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsFileAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new Attachment
            {
                Url = "https://example.com/file.pdf",
                Caption = "Please see the attached file.",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsImageAsyncReturnsOk() =>
        await this.AssertResponse(new MmsImageRequest
        {
            To = "441234567890",
            From = "015417543010",
            Image = new Attachment
            {
                Url = "https://test.com/image.png",
                Caption = "Caption",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsImageAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsImageRequest
        {
            To = "441234567890",
            From = "015417543010",
            Image = new Attachment
            {
                Url = "https://test.com/image.png",
                Caption = "Caption",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsTextAsyncReturnsOk() =>
        await this.AssertResponse(new MmsTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello there",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsTextAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello there",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsVcardAsyncReturnsOk() =>
        await this.AssertResponse(new MmsVcardRequest
        {
            To = "441234567890",
            From = "015417543010",
            Vcard = new Attachment
            {
                Url = "https://test.com/contact.vcf",
                Caption = "Caption",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsVcardAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsVcardRequest
        {
            To = "441234567890",
            From = "015417543010",
            Vcard = new Attachment
            {
                Url = "https://test.com/contact.vcf",
                Caption = "Caption",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsVideoAsyncReturnsOk() =>
        await this.AssertResponse(new MmsVideoRequest
        {
            To = "441234567890",
            From = "015417543010",
            Video = new CaptionedAttachment
            {
                Url = "https://test.com/image.mp4",
                Caption = "A video of me",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        }, this.helper.GetRequestJson());

    [Fact]
    public async Task SendMmsVideoAsyncReturnsOkWithFull() =>
        await this.AssertResponse(new MmsVideoRequest
        {
            To = "441234567890",
            From = "015417543010",
            Video = new CaptionedAttachment
            {
                Url = "https://test.com/image.mp4",
                Caption = "A video of me",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        }, this.helper.GetRequestJson());

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