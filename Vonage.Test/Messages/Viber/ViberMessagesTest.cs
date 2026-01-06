#region
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.Viber;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Viber;

[Trait("Category", "Legacy")]
public class ViberMessagesTest : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ViberMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendViberFileAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new ViberFileRequest
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendViberImageAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new ViberImageRequest
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendViberTextAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new ViberTextRequest
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendViberVideoAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new ViberVideoRequest
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
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
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
        response.Should().BeEquivalentTo(new MessagesResponse(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")));
    }
}