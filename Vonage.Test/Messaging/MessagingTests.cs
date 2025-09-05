#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Messaging;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

[Trait("Category", "Legacy")]
public class MessagingTests : TestBase
{
    private readonly SerializationTestHelper helper =
        new SerializationTestHelper(typeof(MessagingTests).Namespace, JsonSerializerBuilder.BuildWithCamelCase());

    private string ExpectedUri => $"{this.RestUrl}/sms/json";
    private Credentials Credentials => Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
    private ISmsClient Client => this.BuildVonageClient(this.Credentials).SmsClient;

    [Fact]
    public async Task SendSmsAsyncBadResponse()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
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
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateRequestWithAllProperties());
        response.ShouldMatchExpectedResponseWithClientRef();
    }

    [Fact]
    public async Task SendSmsTypicalUsage()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateBasicRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task SendSmsTypicalUsageSimplifiedAsync()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync("AcmeInc", "447700900000", "Hello World!");
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task SendSmsUnicode()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(MessagingTestData.CreateUnicodeRequest());
        response.ShouldMatchExpectedBasicResponse();
    }

    [Fact]
    public async Task ShouldThrowException_GivenResponseIsNull()
    {
        this.Setup(this.ExpectedUri, string.Empty, this.helper.GetRequest("txt"));
        var act = async () => await this.Client.SendAnSmsAsync(MessagingTestData.CreateBasicRequest());
        await act.Should().ThrowAsync<VonageSmsResponseException>().WithMessage("Encountered an Empty SMS response");
    }
}