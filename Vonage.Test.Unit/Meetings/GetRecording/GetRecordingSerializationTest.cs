using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecording
{
    public class GetRecordingSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetRecordingSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetRecordingSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Recording>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be(new Guid("7734a958-6793-4438-9978-ab97d3091912"));
                    success.SessionId.Should()
                        .Be("2_MX40NjMwODczMn5-MTU3NTgyODEwNzQ2MH5OZDJrVmdBRUNDbG5MUzNqNXgya20yQ1Z-fg");
                    success.StartedAt.Should().Be(new DateTime(2023, 02, 06, 13, 58, 37));
                    success.EndedAt.Should().Be(new DateTime(2023, 02, 06, 13, 59, 37));
                    success.Status.Should().Be(RecordingStatus.Started);
                    success.Links.Url.Href.Should()
                        .Be(
                            "https://prod-meetings-recordings.s3.amazonaws.com/46339892/7734a958-6793-4438-9978-ab97d3091912");
                });
    }
}