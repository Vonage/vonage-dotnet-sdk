#region
using System.Net;
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

    private ISmsClient Client =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).SmsClient;

    [Fact]
    public async Task SendSmsAsyncBadResponse()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var exception = await Assert.ThrowsAsync<VonageSmsResponseException>(async () =>
            await this.Client.SendAnSmsAsync(new SendSmsRequest
                {From = "AcmeInc", To = "447700900000", Text = "Hello World!"}));
        Assert.NotNull(exception);
        Assert.Equal(
            $"SMS Request Failed with status: {exception.Response.Messages[0].Status} and error message: {exception.Response.Messages[0].ErrorText}",
            exception.Message);
        Assert.Equal(SmsStatusCode.InvalidCredentials, exception.Response.Messages[0].StatusCode);
    }

    [Fact]
    public async Task SendSmsAsyncWithAllPropertiesSet()
    {
        var request = new SendSmsRequest
        {
            AccountRef = "customer1234",
            Body = "638265253311",
            Callback = "https://example.com/sms-dlr",
            ClientRef = "my-personal-reference",
            From = "AcmeInc",
            To = "447700900000",
            MessageClass = 0,
            ProtocolId = 127,
            StatusReportReq = true,
            Text = "Hello World!",
            Ttl = 900000,
            Type = SmsType.Text,
            Udh = "06050415811581",
            ContentId = "testcontent",
            EntityId = "testEntity",
            TrustedNumber = true,
        };
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(request);
        Assert.Equal("1", response.MessageCount);
        Assert.Equal("447700900000", response.Messages[0].To);
        Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
        Assert.Equal("0", response.Messages[0].Status);
        Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
        Assert.Equal("12345", response.Messages[0].Network);
        Assert.Equal("customer1234", response.Messages[0].AccountRef);
        response.Messages[0].ClientRef.Should().Be("my-personal-reference");
    }

    [Fact]
    public async Task SendSmsTypicalUsage()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(new SendSmsRequest
            {From = "AcmeInc", To = "447700900000", Text = "Hello World!"});
        Assert.Equal("1", response.MessageCount);
        Assert.Equal("447700900000", response.Messages[0].To);
        Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
        Assert.Equal("0", response.Messages[0].Status);
        Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
        Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
        Assert.Equal("12345", response.Messages[0].Network);
        Assert.Equal("customer1234", response.Messages[0].AccountRef);
    }

    [Fact]
    public async Task SendSmsTypicalUsageSimplifiedAsync()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync("AcmeInc", "447700900000", "Hello World!");
        Assert.Equal("1", response.MessageCount);
        Assert.Equal("447700900000", response.Messages[0].To);
        Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
        Assert.Equal("0", response.Messages[0].Status);
        Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
        Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
        Assert.Equal("12345", response.Messages[0].Network);
        Assert.Equal("customer1234", response.Messages[0].AccountRef);
    }

    [Fact]
    public async Task SendSmsUnicode()
    {
        this.Setup(this.ExpectedUri, this.helper.GetResponseJson(), this.helper.GetRequest("txt"));
        var response = await this.Client.SendAnSmsAsync(new SendSmsRequest
            {From = "AcmeInc", To = "447700900000", Text = "こんにちは世界"});
        Assert.Equal("1", response.MessageCount);
        Assert.Equal("447700900000", response.Messages[0].To);
        Assert.Equal("0A0000000123ABCD1", response.Messages[0].MessageId);
        Assert.Equal("0", response.Messages[0].Status);
        Assert.Equal(SmsStatusCode.Success, response.Messages[0].StatusCode);
        Assert.Equal("3.14159265", response.Messages[0].RemainingBalance);
        Assert.Equal("12345", response.Messages[0].Network);
        Assert.Equal("customer1234", response.Messages[0].AccountRef);
    }

    [Fact]
    public async Task ShouldThrowException_GivenResponseIsNull()
    {
        var expectedRequestContent =
            $"from=AcmeInc&to=447700900000&text={WebUtility.UrlEncode("Hello World!")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
        this.Setup(this.ExpectedUri, string.Empty, this.helper.GetRequest("txt"));
        var act = async () => await this.Client.SendAnSmsAsync(new SendSmsRequest
            {From = "AcmeInc", To = "447700900000", Text = "Hello World!"});
        await act.Should().ThrowAsync<VonageSmsResponseException>().WithMessage("Encountered an Empty SMS response");
    }
}