using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ApplicationsNew.CreateApplication;

[Trait("Category", "E2E")]
[Trait("Product", "ApplicationsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task CreateApplication()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.Created)
                .WithBody(this.Serialization.GetResponseJson(
                    nameof(ApplicationResponseSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsNewClient
            .CreateApplicationAsync(SerializationTest.BuildRequest())
            .Should()
            .BeSuccessAsync(ApplicationResponseSerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task CreateApplicationWithRequiredFieldsOnly()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(
                    nameof(SerializationTest.ShouldSerializeWithRequiredFieldsOnly)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.Created)
                .WithBody(this.Serialization.GetResponseJson(
                    nameof(ApplicationResponseSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsNewClient
            .CreateApplicationAsync(SerializationTest.BuildRequestWithRequiredFieldsOnly())
            .Should()
            .BeSuccessAsync();
    }
}
