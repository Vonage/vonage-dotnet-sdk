using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.ProactiveConnect.Lists.DeleteList;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.DeleteList
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DeleteList()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.ProactiveConnectClient.DeleteListAsync(
                    DeleteListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8")))
                .Should()
                .BeSuccessAsync();
        }
    }
}