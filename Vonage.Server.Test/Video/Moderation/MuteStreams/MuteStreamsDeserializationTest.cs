using FluentAssertions;
using Vonage.Server.Test.Common;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.MuteStreams
{
    public class MuteStreamsDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public MuteStreamsDeserializationTest()
        {
            this.helper = new SerializationTestHelper(typeof(MuteStreamsDeserializationTest).Namespace);
        }

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer.DeserializeObject<MuteStreamsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.ApplicationId.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
                    success.Status.Should().Be("ACTIVE");
                    success.Name.Should().Be("Joe Montana");
                    success.Environment.Should().Be("standard");
                    success.CreatedAt.Should().Be(1414642898000);
                });
    }
}