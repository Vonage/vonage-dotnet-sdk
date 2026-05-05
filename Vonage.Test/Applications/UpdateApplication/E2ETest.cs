using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Applications.UpdateApplication;

[Trait("Category", "E2E")]
[Trait("Product", "ApplicationsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task UpdateApplication()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa-323d-0114-9c3d-d6f0d48968cf")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPut())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(
                    nameof(ApplicationResponseSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsClient
            .UpdateApplicationAsync(SerializationTest.BuildRequest())
            .Should()
            .BeSuccessAsync(ApplicationResponseSerializationTest.VerifyExpectedResponse);
    }
}
