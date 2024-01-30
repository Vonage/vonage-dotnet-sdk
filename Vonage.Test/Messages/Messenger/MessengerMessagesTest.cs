using System;
using System.Threading.Tasks;
using Vonage.Messages;
using Vonage.Messages.Messenger;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;

namespace Vonage.Test.Messages.Messenger;

[Trait("Category", "Legacy")]
public class MessengerMessagesTest : TestBase
{
    private readonly string expectedUri;
    private readonly SerializationTestHelper helper;

    public MessengerMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.helper = new SerializationTestHelper(typeof(MessengerMessagesTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());
    }

    [Fact]
    public async Task SendMessengerAudioAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new MessengerAudioRequest
        {
            To = "441234567890",
            From = "015417543010",
            Audio = new Attachment
            {
                Url = "https://test.com/voice.mp3",
            },
            ClientRef = "abcdefg",
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(creds);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }

    [Fact]
    public async Task SendMessengerFileAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new MessengerFileRequest
        {
            To = "441234567890",
            From = "015417543010",
            File = new Attachment
            {
                Url = "https://test.com/me.txt",
            },
            ClientRef = "abcdefg",
        };
        var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(credentials);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }

    [Fact]
    public async Task SendMessengerImageAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new MessengerImageRequest
        {
            To = "441234567890",
            From = "015417543010",
            Image = new Attachment
            {
                Url = "https://test.com/image.png",
            },
            ClientRef = "abcdefg",
        };
        var credentials = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(credentials);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }

    [Fact]
    public async Task SendMessengerTextAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new MessengerTextRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "Hello mum",
            ClientRef = "abcdefg",
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(creds);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }

    [Fact]
    public async Task SendMessengerVideoAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new MessengerVideoRequest
        {
            To = "441234567890",
            From = "015417543010",
            Video = new Attachment
            {
                Url = "https://test.com/me.mp4",
            },
            ClientRef = "abcdefg",
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(creds);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}