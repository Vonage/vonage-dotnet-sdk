using System;
using System.Collections.Generic;
using AutoFixture;
using Vonage.ProactiveConnect.Items.CreateItem;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.CreateItem
{
    public class RequestTest
    {
        private readonly KeyValuePair<string, object> element;

        public RequestTest() => this.element = new Fixture().Create<KeyValuePair<string, object>>();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateItemRequest
                .Build()
                .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                .WithCustomData(this.element)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items");
    }
}