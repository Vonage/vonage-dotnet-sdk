#region
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;
#endregion

namespace Vonage.Test.Common.Client;

[Trait("Category", "Unit")]
public class ApiErrorsTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(ApiErrorsTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    private readonly JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public async Task ShouldReturnErrorContent_GivenVideoApiError()
    {
        var expectedJson = this.helper.GetResponseJson("ApiErrorVideo");
        var sut = this.BuildClient<VideoApiError>(expectedJson);
        var result = await sut.SendAsync(BuildFakeRequest());
        result.Should().BeFailure(HttpFailure.From(HttpStatusCode.BadRequest,
            "You do not pass in a session ID or you pass in an invalid session ID.", expectedJson));
    }

    [Fact]
    public async Task ShouldReturnErrorContent_GivenNetworkApiError()
    {
        var expectedJson = this.helper.GetResponseJson("ApiErrorNetwork");
        var sut = this.BuildClient<NetworkApiError>(expectedJson);
        var result = await sut.SendAsync(BuildFakeRequest());
        result.Should().BeFailure(HttpFailure.From(HttpStatusCode.BadRequest,
            "Client specified an invalid argument, request body or query param", expectedJson));
    }

    [Fact]
    public async Task ShouldReturnErrorContent_GivenStandardApiError()
    {
        var expectedJson = this.helper.GetResponseJson("ApiErrorStandard");
        var sut = this.BuildClient<StandardApiError>(expectedJson);
        var result = await sut.SendAsync(BuildFakeRequest());
        result.Should().BeFailure(HttpFailure.From(HttpStatusCode.BadRequest,
            "You did not provide correct credentials.", expectedJson));
    }

    private static Result<VonageHttpClientTest.FakeRequest> BuildFakeRequest() =>
        Result<VonageHttpClientTest.FakeRequest>.FromSuccess(new VonageHttpClientTest.FakeRequest());

    private VonageHttpClient<T> BuildClient<T>(string responseContent) where T : IApiError
    {
        var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.BadRequest)
            .WithResponseContent(responseContent);
        var configuration = new VonageHttpClientConfiguration(messageHandler.ToHttpClient(),
            new AuthenticationHeaderValue("Anonymous"), "userAgent");
        return new VonageHttpClient<T>(configuration, this.serializer);
    }
}