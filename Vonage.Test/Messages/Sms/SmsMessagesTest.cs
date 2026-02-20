#region
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Messages;
using Vonage.Messages.Sms;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messages.Sms;

[Trait("Category", "Legacy")]
public class SmsMessagesTest : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBearerCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(SmsMessagesTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task SendSmsAsyncReturnsInvalidCredentials()
    {
        var expectedResponse = this.helper.GetResponseJson();
        var expectedRequest = this.helper.GetRequestJson();
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/messages")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(expectedRequest)
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.Unauthorized).WithBody(expectedResponse));
        var exception =
            await Assert.ThrowsAsync<VonageHttpRequestException>(async () =>
                await this.context.VonageClient.MessagesClient.SendAsync(new SmsRequest
                {
                    To = "441234567890",
                    From = "015417543010",
                    Text = "This is a test",
                    ClientRef = "abcdefg",
                    WebhookUrl = new Uri("https://example.com/status"),
                    WebhookVersion = "v1",
                }));
        exception.Should().NotBeNull();
        exception.Json.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task SendSmsAsyncReturnsOk() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        });

    [Fact]
    public async Task SendSmsAsyncReturnsOkWithPoolId() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            PoolId = "123456789",
        });

    [Fact]
    public async Task SendSmsAsyncReturnsOkWithSettings() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TimeToLive = 90000,
            Settings = new OptionalSettings("text", "1107457532145798767", "1101456324675322134"),
        });

    [Fact]
    public async Task SendSmsAsyncReturnsOkWithTrustedNumber() =>
        await this.VerifySendMessage(this.helper.GetRequestJson(), new SmsRequest
        {
            To = "441234567890",
            From = "015417543010",
            Text = "This is a test",
            ClientRef = "abcdefg",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            TrustedNumber = true,
            TrustedRecipient = true,
        });

    private async Task VerifySendMessage(string expectedRequest, SmsRequest request)
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