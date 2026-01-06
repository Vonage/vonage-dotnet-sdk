#region
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.Messenger;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Messenger;

[Trait("Category", "Legacy")]
public class MessengerMessagesTest : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(MessengerMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendMessengerAudioAsyncReturnsOk()
    {
        await this.VerifySendMessage(this.helper.GetRequestJson(), new MessengerAudioRequest
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
            Data = new MessengerRequestData
            {
                Category = MessengerMessageCategory.Response,
                Tag = MessengerTagType.ConfirmedEventUpdate,
            },
        });
    }

    [Fact]
    public async Task SendMessengerFileAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new MessengerFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new Attachment
            {
                Url = "https://test.com/me.txt",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            Data = new MessengerRequestData
            {
                Category = MessengerMessageCategory.Response,
                Tag = MessengerTagType.ConfirmedEventUpdate,
            },
        });

    [Fact]
    public async Task SendMessengerImageAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new MessengerImageRequest
        {
            To = "441234567890",
            From = "015417543010",
            Image = new Attachment
            {
                Url = "https://test.com/image.png",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            Data = new MessengerRequestData
            {
                Category = MessengerMessageCategory.Response,
                Tag = MessengerTagType.ConfirmedEventUpdate,
            },
        });

    [Fact]
    public async Task SendMessengerTextAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new MessengerTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            Data = new MessengerRequestData
            {
                Category = MessengerMessageCategory.Response,
                Tag = MessengerTagType.ConfirmedEventUpdate,
            },
        });

    [Fact]
    public async Task SendMessengerVideoAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new MessengerVideoRequest
        {
            To = "441234567890",
            From = "015417543010",
            Video = new Attachment
            {
                Url = "https://test.com/me.mp4",
            },
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            Data = new MessengerRequestData
            {
                Category = MessengerMessageCategory.Response,
                Tag = MessengerTagType.ConfirmedEventUpdate,
            },
        });

    private async Task VerifySendMessage(string expectedRequest, IMessage request)
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/messages")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(expectedRequest)
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.helper.GetResponseJson("SendMessage")));
        var response = await this.context.VonageClient.MessagesClient.SendAsync(request);
        response.Should().BeEquivalentTo(new MessagesResponse(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"),
            "3TcNjguHxr2vcCZ9Ddsnq6tw8yQUpZ9rMHv9QXSxLan5ibMxqSzLdx9"));
    }
}