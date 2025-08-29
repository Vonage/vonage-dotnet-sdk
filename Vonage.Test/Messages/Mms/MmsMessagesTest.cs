#region
using System;
using System.Threading.Tasks;
using Vonage.Messages;
using Vonage.Messages.Mms;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Mms;

[Trait("Category", "Legacy")]
public class MmsMessagesTest : TestBase
{
    private const string ResponseKey = "SendMessage";
    private readonly VonageClient client;
    private readonly string expectedResponse;
    private readonly string expectedUri;

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(MmsMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public MmsMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.client = this.BuildVonageClient(Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey));
        this.expectedResponse = this.helper.GetResponseJson(ResponseKey);
    }

    [Fact]
    public async Task SendMmsAudioAsyncReturnsOk()
    {
        var request = new MmsAudioRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsAudioAsyncReturnsOkWithFull()
    {
        var request = new MmsAudioRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsContentAsyncReturnsOk()
    {
        var request = new MmsContentRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsContentAsyncReturnsOkWithFull()
    {
        var request = new MmsContentRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsFileAsyncReturnsOk()
    {
        var request = new MmsFileRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsFileAsyncReturnsOkWithFull()
    {
        var request = new MmsFileRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsImageAsyncReturnsOk()
    {
        var request = new MmsImageRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsImageAsyncReturnsOkWithFull()
    {
        var request = new MmsImageRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsTextAsyncReturnsOk()
    {
        var request = new MmsTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello there",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsTextAsyncReturnsOkWithFull()
    {
        var request = new MmsTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello there",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 600,
            TrustedNumber = true,
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsVcardAsyncReturnsOk()
    {
        var request = new MmsVcardRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsVcardAsyncReturnsOkWithFull()
    {
        var request = new MmsVcardRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsVideoAsyncReturnsOk()
    {
        var request = new MmsVideoRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    [Fact]
    public async Task SendMmsVideoAsyncReturnsOkWithFull()
    {
        var request = new MmsVideoRequest
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
        };
        await this.AssertResponse(request, this.helper.GetRequestJson());
    }

    private async Task AssertResponse(IMessage request, string expectedRequest)
    {
        this.Setup(this.expectedUri, this.expectedResponse, expectedRequest);
        var response = await this.client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}