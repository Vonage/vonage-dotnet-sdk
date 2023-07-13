using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.CreateItem;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.CreateItem
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task CreateItem()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.ProactiveConnectClient.CreateItemAsync(CreateItemRequest
                .Build()
                .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                .WithCustomData(new KeyValuePair<string, object>("value1", "value"))
                .WithCustomData(new KeyValuePair<string, object>("value2", 0))
                .WithCustomData(new KeyValuePair<string, object>("value3", true))
                .Create());
            result.Should().BeSuccess();
        }
    }
}