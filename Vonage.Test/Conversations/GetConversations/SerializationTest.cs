using System;
using System.Globalization;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetConversations;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetConversations
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() => this.helper.Serializer
            .DeserializeObject<GetConversationsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

        internal static void VerifyExpectedResponse(GetConversationsResponse response)
        {
            response.PageSize.Should().Be(10);
            response.Embedded.Conversations.Should().BeEquivalentTo(new[]
            {
                new Conversation(
                    "CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a",
                    "customer_chat",
                    "Customer Chat",
                    new Uri("https://example.com/image.png"),
                    null,
                    0,
                    new Timestamp(DateTimeOffset.Parse("2019-09-03T18:40:24.324Z", CultureInfo.InvariantCulture),
                        DateTimeOffset.Parse("2019-09-03T18:40:24.324Z", CultureInfo.InvariantCulture),
                        DateTimeOffset.Parse("2019-09-03T18:40:24.324Z", CultureInfo.InvariantCulture)),
                    Maybe<Properties>.None,
                    new Links(new HalLink(new Uri(
                        "https://api.nexmo.com/v1/conversations/CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a")))),
            });
            response.Links.First.Href.Should()
                .Be(new Uri("https://api.nexmo.com/v1/conversations?order=desc&page_size=10"));
            response.Links.Self.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
            response.Links.Next.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
            response.Links.Previous.Href.Should()
                .Be(new Uri(
                    "https://api.nexmo.com/v1/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
        }
    }
}