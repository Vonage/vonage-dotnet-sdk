using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.GetLists;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetLists
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetLists()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists")
                    .WithParam("page", "25")
                    .WithParam("page_size", "50")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.GetListsAsync(GetListsRequest.Build()
                    .WithPage(25)
                    .WithPageSize(50)
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyLists);
        }
    }
}