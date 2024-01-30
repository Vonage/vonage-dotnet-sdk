using System;
using FluentAssertions;
using Vonage.Meetings.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.GetRecording;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper;

    public SerializationTest() =>
        this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<Recording>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyRecording);

    internal static void VerifyRecording(Recording success)
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
    }
}