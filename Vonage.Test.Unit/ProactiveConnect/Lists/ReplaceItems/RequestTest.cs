using System;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.ReplaceItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.ReplaceItems
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ReplaceItemsRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v.01/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8/fetch");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
            ReplaceItemsRequest.Parse(Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Id cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            ReplaceItemsRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.Id)
                .Should()
                .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
    }
}