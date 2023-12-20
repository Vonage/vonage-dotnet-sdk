using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Vonage.ProactiveConnect.Items.ImportItems;
using Vonage.Test.Unit.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ImportItems
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task ImportItems()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/import")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.ImportItemsAsync(ImportItemsRequest
                    .Build()
                    .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                    .WithFileData(new Fixture().CreateMany<byte>().ToArray())
                    .Create())
                .Should()
                .BeSuccessAsync(new ImportItemsResponse(50, 100, 200));
        }
    }
}