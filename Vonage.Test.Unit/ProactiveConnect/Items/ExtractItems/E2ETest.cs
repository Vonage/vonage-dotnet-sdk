using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.ExtractItems;
using Vonage.Test.Unit.ProactiveConnect.Items.CreateItem;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ExtractItems
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task ExtractItems()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/download")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody("CSV content"));
            var result =
                await this.Helper.VonageClient.ProactiveConnectClient.ExtractItemsAsync(
                    ExtractItemsRequest.Parse(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b")));
            result.Should().BeSuccess();
        }
    }
}