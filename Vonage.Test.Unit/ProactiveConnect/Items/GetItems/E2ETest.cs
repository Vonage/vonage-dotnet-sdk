using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.GetItems;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItems
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task GetItems()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v0.1/bulk/lists/e250d4ae-eb43-4eee-a901-88ce4420aed3/items")
                    .WithParam("page", "25")
                    .WithParam("page_size", "50")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.ProactiveConnectClient.GetItemsAsync(GetItemsRequest.Build()
                .WithListId(new Guid("e250d4ae-eb43-4eee-a901-88ce4420aed3"))
                .WithPage(25)
                .WithPageSize(50)
                .Create());
            result.Should().BeSuccess();
        }
    }
}