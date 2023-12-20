using System;
using Vonage.ProactiveConnect;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Users.GetUsers;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUsers
{
    public class RequestTest
    {
        [Theory]
        [InlineData(null, null, null, "/v1/users?page_size=10&order=asc")]
        [InlineData(50, null, null, "/v1/users?page_size=50&order=asc")]
        [InlineData(null, FetchOrder.Descending, null, "/v1/users?page_size=10&order=desc")]
        [InlineData(null, null, "test_name", "/v1/users?page_size=10&order=asc&name=test_name")]
        [InlineData(50, FetchOrder.Descending, "test_name", "/v1/users?page_size=50&order=desc&name=test_name")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(int? pageSize, FetchOrder? order, string name,
            string expectedEndpoint)
        {
            var builder = GetUsersRequest.Build();
            if (pageSize.HasValue)
            {
                builder.WithPageSize(pageSize.Value);
            }

            if (order.HasValue)
            {
                builder.WithOrder(order.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                builder.WithName(name);
            }

            builder.Create()
                .Map(request => request.GetEndpointPath())
                .Should().BeSuccess(expectedEndpoint);
        }

        [Theory]
        [InlineData(
            "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D",
            "/v1/users?page_size=10&order=desc&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D")]
        [InlineData(
            "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&name=Test",
            "/v1/users?page_size=10&order=desc&name=Test&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D")]
        public void GetEndpointPath_ShouldReturnApiEndpoint_GivenGeneratedFromLink(string uri,
            string expectedEndpoint) =>
            new GetUsersHalLink(new Uri(uri))
                .BuildRequest()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(expectedEndpoint);
    }
}