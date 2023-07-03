using System;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.ExtractItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ExtractItems
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ExtractItemsRequest.Parse(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/download");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
            ExtractItemsRequest.Parse(Guid.Empty)
                .Should()
                .BeParsingFailure("ListId cannot be empty.");

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            ExtractItemsRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.ListId)
                .Should()
                .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
    }
}