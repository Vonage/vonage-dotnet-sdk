using System;
using Vonage.Common;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect;
using Vonage.Users.GetUsers;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUsers
{
    public class GetUsersResponseTest
    {
        [Fact]
        public void BuildRequestForNext_ShouldReturnRequest_WithAllProperties() =>
            BuildResponse(new HalLinks
                {
                    Next = new HalLink(new Uri(
                        "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&name=Test")),
                })
                .BuildRequestForNext()
                .Should()
                .BeSuccess(new GetUsersRequest("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=", "Test",
                    FetchOrder.Descending, 10));
        
        [Fact]
        public void BuildRequestForNext_ShouldReturnRequest_WithAllProperties2() =>
            BuildResponse(new HalLinks
                {
                    Next = new HalLink(new Uri(
                        "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&name=Test")),
                })
                .BuildRequestForNext()
                .Should()
                .BeSuccess(new GetUsersRequest("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=", "Test",
                    FetchOrder.Descending, 10));

        private static GetUsersResponse BuildResponse(HalLinks links) => new GetUsersResponse(10,
            new EmbeddedUsers(Array.Empty<UserSummary>()), links);
    }
}