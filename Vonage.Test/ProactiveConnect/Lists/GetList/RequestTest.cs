using System;
using Vonage.ProactiveConnect.Lists.GetList;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.GetList;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
        GetListRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("Id cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        GetListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
            .Map(request => request.Id)
            .Should()
            .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
}