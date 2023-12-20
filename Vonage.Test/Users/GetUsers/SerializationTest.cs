using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Users.GetUsers;
using Xunit;

namespace Vonage.Test.Users.GetUsers
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetUsersResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyResponse);

        internal static void VerifyResponse(GetUsersResponse success)
        {
            success.PageSize.Should().Be(10);
            success.Embedded.Users.Should().BeEquivalentTo(new[]
            {
                new UserSummary("USR-82e028d9-5201-4f1e-8188-604b2d3471ec", "my_user_name", "My User Name", new HalLinks
                {
                    Self = new HalLink(
                        new Uri("https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")),
                }),
            });
            success.Links.First.Href.Should().Be(new Uri("https://api.nexmo.com/v1/users?order=desc&page_size=10"));
            success.Links.Self.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
            success.Links.Next.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
            success.Links.Previous.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
        }
    }
}