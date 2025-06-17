#region
using System;
using System.Threading.Tasks;
using Vonage.Messages;
using Vonage.Messages.Viber;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Viber;

[Trait("Category", "Legacy")]
public class ViberMessagesTest : TestBase
{
    private readonly string expectedUri;
    private readonly SerializationTestHelper helper;

    public ViberMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.helper = new SerializationTestHelper(typeof(ViberMessagesTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());
    }

    [Fact]
    public async Task SendViberFileAsyncReturnsOk()
    {
        var request = new ViberFileRequest
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
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendViberImageAsyncReturnsOk()
    {
        var request = new ViberImageRequest
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
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendViberTextAsyncReturnsOk()
    {
        var request = new ViberTextRequest
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
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    [Fact]
    public async Task SendViberVideoAsyncReturnsOk()
    {
        var request = new ViberVideoRequest
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
        };
        await this.VerifySendMessage(this.helper.GetRequestJson(), request);
    }

    private async Task VerifySendMessage(string expectedRequest, IMessage request)
    {
        var expectedResponse = this.helper.GetResponseJson("SendMessage");
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}