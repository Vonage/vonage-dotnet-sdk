using System;
using System.Linq;
using AutoFixture;
using Vonage.ProactiveConnect.Items.ImportItems;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.ImportItems;

public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        ImportItemsRequest.Build()
            .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
            .WithFileData(new Fixture().CreateMany<byte>().ToArray())
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/import");
}