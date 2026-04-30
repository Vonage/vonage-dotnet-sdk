using System.Net;
using System.Threading.Tasks;
using Vonage.ApplicationsNew.DeleteApplication;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ApplicationsNew.DeleteApplication;

[Trait("Category", "E2E")]
[Trait("Product", "ApplicationsNew")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task DeleteApplication()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications/78d335fa-323d-0114-9c3d-d6f0d48968cf")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.NoContent));
        await this.Helper.VonageClient.ApplicationsNewClient
            .DeleteApplicationAsync(DeleteApplicationRequest.Build()
                .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}
