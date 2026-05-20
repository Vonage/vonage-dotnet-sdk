#region
using System;
using System.Collections.Generic;
using System.Linq;
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
[Trait("Product", "Messages.Sms")]
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

    [Fact]
    public void GetErrors_ReturnsEmpty_WhenRequestIsValid() =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = "Hello" }
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenFromIsNullOrEmpty(string from) =>
        new SmsRequest { To = "441234567890", From = from, Text = "Hello" }
            .GetErrors().Should().Contain("From must not be null or empty.");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenToIsNullOrEmpty(string to) =>
        new SmsRequest { To = to, From = "015417543010", Text = "Hello" }
            .GetErrors().Should().Contain("To must not be null or empty.");

    [Theory]
    [InlineData("1")]
    [InlineData("123456")]
    public void GetErrors_ReturnsError_WhenToIsTooShort(string to) =>
        new SmsRequest { To = to, From = "015417543010", Text = "Hello" }
            .GetErrors().Should().Contain("To length must be between 7 and 15 characters.");

    [Fact]
    public void GetErrors_ReturnsError_WhenToIsTooLong() =>
        new SmsRequest { To = "1234567890123456", From = "015417543010", Text = "Hello" }
            .GetErrors().Should().Contain("To length must be between 7 and 15 characters.");

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789012345")]
    public void GetErrors_ReturnsEmpty_WhenToIsAtLengthBoundary(string to) =>
        new SmsRequest { To = to, From = "015417543010", Text = "Hello" }
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetErrors_ReturnsError_WhenTextIsNullOrEmpty(string text) =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = text }
            .GetErrors().Should().Contain("Text must not be null or empty.");

    [Theory]
    [InlineData(0)]
    [InlineData(20)]
    [InlineData(604800)]
    public void GetErrors_ReturnsEmpty_WhenTtlIsValid(int ttl) =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = "Hello", TimeToLive = ttl }
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData(1)]
    [InlineData(19)]
    [InlineData(604801)]
    [InlineData(int.MaxValue)]
    public void GetErrors_ReturnsError_WhenTtlIsOutOfRange(int ttl) =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = "Hello", TimeToLive = ttl }
            .GetErrors().Should().Contain("TimeToLive must be between 20 and 604800.");

    [Theory]
    [InlineData("v0.1")]
    [InlineData("v1")]
    public void GetErrors_ReturnsEmpty_WhenWebhookVersionIsValid(string version) =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = "Hello", WebhookVersion = version }
            .GetErrors().Should().BeEmpty();

    [Theory]
    [InlineData("v2")]
    [InlineData("invalid")]
    [InlineData("V1")]
    public void GetErrors_ReturnsError_WhenWebhookVersionIsInvalid(string version) =>
        new SmsRequest { To = "441234567890", From = "015417543010", Text = "Hello", WebhookVersion = version }
            .GetErrors().Should().Contain("WebhookVersion must be 'v0.1' or 'v1'.");

    [Fact]
    public void GetErrors_ReturnsMultipleErrors_WhenMultipleFieldsAreInvalid()
    {
        var errors = new SmsRequest { TimeToLive = 5 }.GetErrors().ToList();
        errors.Should().Contain("From must not be null or empty.");
        errors.Should().Contain("To must not be null or empty.");
        errors.Should().Contain("Text must not be null or empty.");
        errors.Should().Contain("TimeToLive must be between 20 and 604800.");
    }

    [Fact]
    public async Task SendAsync_ThrowsVonageException_WhenTtlIsOutOfRange()
    {
        var exception = await Assert.ThrowsAsync<VonageException>(() =>
            this.context.VonageClient.MessagesClient.SendAsync(new SmsRequest
            {
                To = "441234567890",
                From = "015417543010",
                Text = "This is a test",
                TimeToLive = 5,
            }));
        exception.Message.Should().Be("TimeToLive must be between 20 and 604800.");
    }

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