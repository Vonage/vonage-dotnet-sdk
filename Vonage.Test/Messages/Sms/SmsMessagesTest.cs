#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Exceptions;
using Vonage.Messages.Sms;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Sms;

[Trait("Category", "Legacy")]
public class SmsMessagesTest : TestBase
{
    private readonly string expectedUri;
    private readonly SerializationTestHelper helper;

    public SmsMessagesTest()
    {
        this.expectedUri = $"{this.ApiUrl}/v1/messages";
        this.helper =
            new SerializationTestHelper(typeof(SmsMessagesTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());
    }

    [Fact]
    public async Task SendSmsAsyncReturnsInvalidCredentials()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest, HttpStatusCode.Unauthorized);
        var client = this.BuildVonageClient(creds);
        var exception =
            await Assert.ThrowsAsync<VonageHttpRequestException>(async () =>
                await client.MessagesClient.SendAsync(request));
        Assert.NotNull(exception);
        Assert.Equal(expectedResponse, exception.Json);
    }

    [Fact]
    public async Task SendSmsAsyncReturnsOk()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        var request = new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(creds);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }

    [Fact]
    public async Task SendSmsAsyncReturnsOkWithSettings()
    {
        var expectedResponse = this.helper.GetResponseJson(nameof(this.SendSmsAsyncReturnsOk));
        var expectedRequest = this.helper.GetRequestJson();
        var request = new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 90000,
            Settings = new OptionalSettings("text", "1107457532145798767", "1101456324675322134"),
        };
        var creds = Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);
        this.Setup(this.expectedUri, expectedResponse, expectedRequest);
        var client = this.BuildVonageClient(creds);
        var response = await client.MessagesClient.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(new Guid("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"), response.MessageUuid);
    }
}