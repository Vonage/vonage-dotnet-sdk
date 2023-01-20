using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetRecordings;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecordings
{
    public class GetRecordingsDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetRecordingsDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetRecordingsDeserializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetRecordingsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Embedded.Recordings.Should().HaveCount(1);
                    success.Embedded.Recordings[0].Id.Should().Be("abc123");
                    success.Embedded.Recordings[0].SessionId.Should()
                        .Be("2_MX40NjMwODczMn5-MTU3NTgyODEwNzQ2MH5OZDJrVmdBRUNDbG5MUzNqNXgya20yQ1Z-fg");
                    success.Embedded.Recordings[0].StartedAt.Should().Be("abc123");
                    success.Embedded.Recordings[0].EndedAt.Should().Be("abc123");
                    success.Embedded.Recordings[0].Status.Should().Be(RecordingStatus.Started);
                    success.Embedded.Recordings[0].Links.Url.Href.Should().Be("abc123");
                });
    }
}