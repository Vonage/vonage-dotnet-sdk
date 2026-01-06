#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messaging;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

[Trait("Category", "Legacy")]
public class MessagingTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper =
        new SerializationTestHelper(typeof(MessagingTests).Namespace, JsonSerializerBuilder.BuildWithCamelCase());

    private ISmsClient Client => this.context.VonageClient.SmsClient;

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    private IResponseBuilder RespondWithBadRequest([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    [Fact]
    public async Task SendSmsAsyncBadResponse()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(this.RespondWithBadRequest());
        var exception = await Assert.ThrowsAsync<VonageSmsResponseException>(async () =>
            await this.Client.SendAnSmsAsync(MessagingTestData.CreateBasicRequest()));
        exception.Should().NotBeNull();
        exception.Message.Should()
            .Be(
                $"SMS Request Failed with status: {exception.Response.Messages[0].Status} and error message: {exception.Response.Messages[0].ErrorText}");
        exception.Response.Messages[0].StatusCode.Should().Be(SmsStatusCode.InvalidCredentials);
    }

    [Fact]
    public async Task SendSmsAsyncWithAllPropertiesSet()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateRequestWithAllProperties());
        response.ShouldMatchExpectedResponseWithClientRef();
    }

    [Fact]
    public async Task SendSmsTypicalUsage()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateBasicRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task SendSmsTypicalUsageSimplifiedAsync()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.Client.SendAnSmsAsync("AcmeInc", "447700900000", "Hello World!");
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task SendSmsUnicode()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateUnicodeRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task ShouldThrowException_GivenResponseIsNull()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/sms/json")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody(this.helper.GetRequest("txt"))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(string.Empty));
        var act = async () => await this.Client.SendAnSmsAsync(MessagingTestData.CreateBasicRequest());
        await act.Should().ThrowAsync<VonageSmsResponseException>().WithMessage("Encountered an Empty SMS response");
    }
}