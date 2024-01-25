using System;
using Vonage.ProactiveConnect.Lists.ReplaceItems;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.ReplaceItems;

public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        ReplaceItemsRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8/fetch");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
        ReplaceItemsRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("Id cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        ReplaceItemsRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
            .Map(request => request.Id)
            .Should()
            .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
}