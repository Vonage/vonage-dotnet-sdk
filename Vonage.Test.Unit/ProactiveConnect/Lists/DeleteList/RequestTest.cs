using System;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.DeleteList;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.DeleteList
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenIdIsEmpty() =>
            DeleteListRequest.Parse(Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Id cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            DeleteListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"))
                .Map(request => request.Id)
                .Should()
                .BeSuccess(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8"));
    }
}