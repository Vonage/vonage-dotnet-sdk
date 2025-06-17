#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Messages;
using Vonage.Messages.Rcs;
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