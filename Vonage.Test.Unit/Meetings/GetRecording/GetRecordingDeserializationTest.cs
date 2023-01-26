using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecording
{
    public class GetRecordingDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetRecordingDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetRecordingDeserializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Recording>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be("abc123");
                    success.SessionId.Should()
                        .Be("2_MX40NjMwODczMn5-MTU3NTgyODEwNzQ2MH5OZDJrVmdBRUNDbG5MUzNqNXgya20yQ1Z-fg");
                    success.StartedAt.Should().Be("abc123");
                    success.EndedAt.Should().Be("abc123");
                    success.Status.Should().Be(RecordingStatus.Started);
                    success.Links.Url.Href.Should().Be("abc123");
                });
    }
}