using System;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.ClearList;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.ClearList
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ClearListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8/clear");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
            ClearListRequest.Parse(Guid.Empty)
                .Should()
                .BeParsingFailure("Id cannot be empty.");

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            ClearListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.Id)
                .Should()
                .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
    }
}