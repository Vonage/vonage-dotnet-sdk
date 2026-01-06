#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Conversions;

[Trait("Category", "Legacy")]
public class ConversionTest : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ConversionTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    [Fact]
    public async Task SmsConversion()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/conversions/sms")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody("message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&")
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response =
            await this.context.VonageClient.ConversionClient.SmsConversionAsync(ConversionTestData
                .CreateBasicRequest());
        response.ShouldBeSuccessfulConversion();
    }

    [Fact]
    public async Task VoiceConversion()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/conversions/voice")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .WithBody("message-id=00A0B0C0&delivered=true&timestamp=2020-01-01+12%3A00%3A00&")
                .UsingPost())
            .RespondWith(this.RespondWithSuccess());
        var response =
            await this.context.VonageClient.ConversionClient.VoiceConversionAsync(
                ConversionTestData.CreateBasicRequest());
        response.ShouldBeSuccessfulConversion();
    }
}