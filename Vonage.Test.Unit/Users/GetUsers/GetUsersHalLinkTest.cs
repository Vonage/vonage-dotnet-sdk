using System;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect;
using Vonage.Users.GetUsers;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUsers
{
    public class GetUsersHalLinkTest
    {
        [Fact]
        public void BuildRequestForPrevious_ShouldReturnSuccess_WithoutCursor() =>
            new GetUsersHalLink(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&name=Test"))
                .BuildRequest()
                .Should()
                .BeSuccess(new GetUsersRequest(Maybe<string>.None, "Test",
                    FetchOrder.Descending, 10));

        [Fact]
        public void BuildRequestForPrevious_ShouldReturnFailure_WhenOrderIsMissing() =>
            new GetUsersHalLink(new Uri(
                    "https://api.nexmo.com/v1/users?page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
                .BuildRequest()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Order is missing from Uri."));

        [Fact]
        public void BuildRequestForPrevious_ShouldReturnFailure_WhenPageSizeIsMissing() =>
            new GetUsersHalLink(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
                .BuildRequest()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("PageSize is missing from Uri."));

        [Fact]
        public void BuildRequestForPrevious_ShouldReturnSuccess_WithAllProperties() =>
            new GetUsersHalLink(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&name=Test"))
                .BuildRequest()
                .Should()
                .BeSuccess(new GetUsersRequest("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=", "Test",
                    FetchOrder.Descending, 10));

        [Fact]
        public void BuildRequestForPrevious_ShouldReturnSuccess_WithoutName() =>
            new GetUsersHalLink(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
                .BuildRequest()
                .Should()
                .BeSuccess(new GetUsersRequest("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=",
                    Maybe<string>.None, FetchOrder.Descending, 10));
    }
}