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
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateItem()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.CreateItemAsync(CreateItemRequest
                    .Build()
                    .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                    .WithCustomData(new KeyValuePair<string, object>("value1", "value"))
                    .WithCustomData(new KeyValuePair<string, object>("value2", 0))
                    .WithCustomData(new KeyValuePair<string, object>("value3", true))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyCreatedItem);
        }

        [Fact]
        public async Task CreateItemWithComplexObjects()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeComplexObject)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.CreateItemAsync(CreateItemRequest
                    .Build()
                    .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                    .WithCustomData(new KeyValuePair<string, object>("fizz", new {Foo = "bar"}))
                    .WithCustomData(new KeyValuePair<string, object>("baz", 2))
                    .WithCustomData(new KeyValuePair<string, object>("Bat", "qux"))
                    .WithCustomData(new KeyValuePair<string, object>("more_items", new object[]
                    {
                        1,
                        2,
                        "three",
                        true,
                        new {Foo = "bar"},
                    }))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyCreatedItem);
        }
    }
}