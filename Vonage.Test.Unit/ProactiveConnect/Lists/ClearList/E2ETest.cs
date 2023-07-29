using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.ClearList;
using Vonage.Test.Unit.ProactiveConnect.Items.CreateItem;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.ClearList
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task ClearList()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8/clear")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result =
                await this.Helper.VonageClient.ProactiveConnectClient.ClearListAsync(
                    ClearListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8")));
            result.Should().BeSuccess();
        }
    }
}