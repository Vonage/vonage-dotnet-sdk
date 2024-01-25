using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Test.Video.Moderation.MuteStream;

public class SerializationTest
{
    private readonly SerializationTestHelper helper;

    public SerializationTest() =>
        this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer.DeserializeObject<MuteStreamResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyResponse);

    internal static void VerifyResponse(MuteStreamResponse success)
    {
        success.ApplicationId.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
        success.Status.Should().Be("ACTIVE");
        success.Name.Should().Be("Joe Montana");
        success.Environment.Should().Be("standard");
        success.CreatedAt.Should().Be(1414642898000);
    }
}