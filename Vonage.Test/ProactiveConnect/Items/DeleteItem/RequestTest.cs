using System;
using Vonage.ProactiveConnect.Items.DeleteItem;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.DeleteItem;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        DeleteItemRequest
            .Build()
            .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
            .WithItemId(new Guid("0f3e672d-e60e-4869-9eac-fce9047532b5"))
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess(
                "/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/0f3e672d-e60e-4869-9eac-fce9047532b5");
}