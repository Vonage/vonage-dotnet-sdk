using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.ProactiveConnect.Items.GetItems;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.GetItems
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetItems()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v0.1/bulk/lists/e250d4ae-eb43-4eee-a901-88ce4420aed3/items")
                    .WithParam("page", "25")
                    .WithParam("page_size", "50")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.GetItemsAsync(GetItemsRequest.Build()
                    .WithListId(new Guid("e250d4ae-eb43-4eee-a901-88ce4420aed3"))
                    .WithPage(25)
                    .WithPageSize(50)
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyItems);
        }
    }
}