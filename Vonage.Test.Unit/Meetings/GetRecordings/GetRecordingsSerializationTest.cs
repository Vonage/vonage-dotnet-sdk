using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetRecordings;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecordings
{
    public class GetRecordingsSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetRecordingsSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetRecordingsSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetRecordingsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Embedded.Recordings.Should().HaveCount(1);
                    success.Embedded.Recordings[0].Id.Should().Be(new Guid("7734a958-6793-4438-9978-ab97d3091912"));
                    success.Embedded.Recordings[0].SessionId.Should()
                        .Be("2_MX40NjMwODczMn5-MTU3NTgyODEwNzQ2MH5OZDJrVmdBRUNDbG5MUzNqNXgya20yQ1Z-fg");
                    success.Embedded.Recordings[0].StartedAt.Should().Be(new DateTime(2023, 02, 06, 13, 58, 37));
                    success.Embedded.Recordings[0].EndedAt.Should().Be(new DateTime(2023, 02, 06, 13, 59, 37));
                    success.Embedded.Recordings[0].Status.Should().Be(RecordingStatus.Started);
                    success.Embedded.Recordings[0].Links.Url.Href.Should().Be(
                        "https://prod-meetings-recordings.s3.amazonaws.com/46339892/7734a958-6793-4438-9978-ab97d3091912");
                });
    }
}