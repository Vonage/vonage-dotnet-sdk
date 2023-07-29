using System;
using System.Linq;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Events;
using Vonage.ProactiveConnect.Events.GetEvents;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Events.GetEvents
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<PaginationResult<EmbeddedEvents>>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyResponse);

        public static void VerifyResponse(PaginationResult<EmbeddedEvents> success)
        {
            success.Page.Should().Be(1);
            success.PageSize.Should().Be(100);
            success.TotalItems.Should().Be(1);
            success.TotalPages.Should().Be(1);
            success.Links.Self.Href.Should()
                .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/events?page_size=100&page=1"));
            success.Links.Previous.Href.Should()
                .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/events?page_size=100&page=1"));
            success.Links.Next.Href.Should()
                .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/events?page_size=100&page=1"));
            success.Links.First.Href.Should()
                .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/events?page_size=100&page=1"));
            success.Embedded.Events.Should().HaveCount(2);
            var firstEvent = success.Embedded.Events.ToList()[0];
            var secondEvent = success.Embedded.Events.ToList()[1];
            firstEvent.OccurredAt.Should().Be(DateTimeOffset.Parse("2022-08-07T13:18:21.970Z"));
            firstEvent.Type.Should().Be(BulkEventType.ActionCallSucceeded);
            firstEvent.Id.Should().Be(new Guid("e8e1eb4d-61e0-4099-8fa7-c96f1c0764ba"));
            firstEvent.JobId.Should().Be(new Guid("c68e871a-c239-474d-a905-7b95f4563b7e"));
            firstEvent.ActionId.Should().Be(new Guid("26c5bbe2-113e-4201-bd93-f69e0a03d17f"));
            firstEvent.RunId.Should().Be(new Guid("7d0d4e5f-6453-4c63-87cf-f95b04377324"));
            firstEvent.RecipientId.Should().Be("14806904549");
            firstEvent.SourceContext.Should().Be("et-e4ab4b75-9e7c-4f26-9328-394a5b842648");
            firstEvent.Data.ToString().Should().Be(
                "{\"url\":\"https://postman-echo.com/post\",\"args\":{},\"data\":{\"from\":\"\"},\"form\":{},\"json\":{\"from\":\"\"},\"files\":{},\"headers\":{\"host\":\"postman-echo.com\",\"user-agent\":\"got (https://github.com/sindresorhus/got)\",\"content-type\":\"application/json\",\"content-length\":\"11\",\"accept-encoding\":\"gzip, deflate, br\",\"x-amzn-trace-id\":\"Root=1-62efbb9e-53636b7b794accb87a3d662f\",\"x-forwarded-port\":\"443\",\"x-nexmo-trace-id\":\"8a6fed94-7296-4a39-9c52-348f12b4d61a\",\"x-forwarded-proto\":\"https\"}}");
            secondEvent.OccurredAt.Should().Be(DateTimeOffset.Parse("2022-08-07T13:18:20.289Z"));
            secondEvent.Type.Should().Be(BulkEventType.RecipientResponse);
            secondEvent.Id.Should().Be(new Guid("8c8e9894-81be-4f6e-88d4-046b6c70ff8c"));
            secondEvent.JobId.Should().Be(new Guid("c68e871a-c239-474d-a905-7b95f4563b7e"));
            secondEvent.ActionId.Should().Be(new Guid("26c5bbe2-113e-4201-bd93-f69e0a03d17f"));
            secondEvent.RunId.Should().Be(new Guid("7d0d4e5f-6453-4c63-87cf-f95b04377324"));
            secondEvent.RecipientId.Should().Be("441632960758");
            secondEvent.SourceContext.Should().Be("et-e4ab4b75-9e7c-4f26-9328-394a5b842648");
            secondEvent.Data.ToString().Should()
                .Be("{\"from\":\"441632960411\",\"text\":\"Erin J. Yearby\"}");
        }
    }
}