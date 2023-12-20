using Vonage.ProactiveConnect;
using Vonage.Test.Common.Extensions;
using Vonage.Users.GetUsers;
using Xunit;

namespace Vonage.Test.Users.GetUsers
{
    public class RequestBuilderTest
    {
        [Fact]
        public void Build_ShouldHaveDefaultPageSize() =>
            GetUsersRequest
                .Build()
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(10);

        [Fact]
        public void Build_ShouldHaveNoDefaultName() =>
            GetUsersRequest
                .Build()
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(name => name.Should().BeNone());

        [Fact]
        public void Build_ShouldSetName() =>
            GetUsersRequest
                .Build()
                .WithName("Administrator")
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(name => name.Should().BeSome("Administrator"));

        [Fact]
        public void Build_ShouldSetPageSize() =>
            GetUsersRequest
                .Build()
                .WithPageSize(50)
                .Create()
                .Map(request => request.PageSize)
                .Should()
                .BeSuccess(50);

        [Fact]
        public void Build_ShouldHaveDefaultOrder() =>
            GetUsersRequest
                .Build()
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Ascending);

        [Fact]
        public void Build_ShouldSetOrder() =>
            GetUsersRequest
                .Build()
                .WithOrder(FetchOrder.Descending)
                .Create()
                .Map(request => request.Order)
                .Should()
                .BeSuccess(FetchOrder.Descending);
    }
}