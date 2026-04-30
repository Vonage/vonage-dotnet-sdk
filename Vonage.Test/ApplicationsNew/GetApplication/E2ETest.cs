using System.Net;
using System.Threading.Tasks;
using Vonage.ApplicationsNew.GetApplication;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ApplicationsNew.GetApplication;

[Trait("Category", "E2E")]
[Trait("Product", "ApplicationsNew")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task GetApplication()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa-323d-0114-9c3d-d6f0d48968cf")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(
                    nameof(ApplicationResponseSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsNewClient
            .GetApplicationAsync(GetApplicationRequest.Build()
                .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
                .Create())
            .Should()
            .BeSuccessAsync(ApplicationResponseSerializationTest.VerifyExpectedResponse);
    }
}
