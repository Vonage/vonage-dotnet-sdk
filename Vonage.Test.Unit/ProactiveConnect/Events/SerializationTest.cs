using System;
using System.Linq;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Events;
using Vonage.ProactiveConnect.Events.GetEvents;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Events
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
                .DeserializeObject<EmbeddedEvents>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Events.ToArray()[0].OccurredAt.Should()
                        .Be(DateTimeOffset.Parse("2022-08-07T13:18:21.970Z"));
                    success.Events.ToArray()[0].Type.Should().Be(BulkEventType.ActionCallSucceeded);
                    success.Events.ToArray()[0].Id.Should().Be(new Guid("e8e1eb4d-61e0-4099-8fa7-c96f1c0764ba"));
                    success.Events.ToArray()[0].JobId.Should().Be(new Guid("c68e871a-c239-474d-a905-7b95f4563b7e"));
                    success.Events.ToArray()[0].ActionId.Should().Be(new Guid("26c5bbe2-113e-4201-bd93-f69e0a03d17f"));
                    success.Events.ToArray()[0].RunId.Should().Be(new Guid("7d0d4e5f-6453-4c63-87cf-f95b04377324"));
                    success.Events.ToArray()[0].RecipientId.Should().Be("14806904549");
                    success.Events.ToArray()[0].SourceContext.Should().Be("et-e4ab4b75-9e7c-4f26-9328-394a5b842648");
                    success.Events.ToArray()[0].Data.ToString().Should().Be(
                        "{\"url\":\"https://postman-echo.com/post\",\"args\":{},\"data\":{\"from\":\"\"},\"form\":{},\"json\":{\"from\":\"\"},\"files\":{},\"headers\":{\"host\":\"postman-echo.com\",\"user-agent\":\"got (https://github.com/sindresorhus/got)\",\"content-type\":\"application/json\",\"content-length\":\"11\",\"accept-encoding\":\"gzip, deflate, br\",\"x-amzn-trace-id\":\"Root=1-62efbb9e-53636b7b794accb87a3d662f\",\"x-forwarded-port\":\"443\",\"x-nexmo-trace-id\":\"8a6fed94-7296-4a39-9c52-348f12b4d61a\",\"x-forwarded-proto\":\"https\"}}");
                    success.Events.ToArray()[1].OccurredAt.Should()
                        .Be(DateTimeOffset.Parse("2022-08-07T13:18:20.289Z"));
                    success.Events.ToArray()[1].Type.Should().Be(BulkEventType.RecipientResponse);
                    success.Events.ToArray()[1].Id.Should().Be(new Guid("8c8e9894-81be-4f6e-88d4-046b6c70ff8c"));
                    success.Events.ToArray()[1].JobId.Should().Be(new Guid("c68e871a-c239-474d-a905-7b95f4563b7e"));
                    success.Events.ToArray()[1].ActionId.Should().Be(new Guid("26c5bbe2-113e-4201-bd93-f69e0a03d17f"));
                    success.Events.ToArray()[1].RunId.Should().Be(new Guid("7d0d4e5f-6453-4c63-87cf-f95b04377324"));
                    success.Events.ToArray()[1].RecipientId.Should().Be("441632960758");
                    success.Events.ToArray()[1].SourceContext.Should().Be("et-e4ab4b75-9e7c-4f26-9328-394a5b842648");
                    success.Events.ToArray()[1].Data.ToString().Should()
                        .Be("{\"from\":\"441632960411\",\"text\":\"Erin J. Yearby\"}");
                });
    }
}