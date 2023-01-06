using FluentAssertions;
using Vonage.Video.Beta.Test.Common;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Moderation.MuteStream
{
    public class MuteStreamDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public MuteStreamDeserializationTest()
        {
            this.helper = new SerializationTestHelper(typeof(MuteStreamDeserializationTest).Namespace);
        }

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer.DeserializeObject<MuteStreamResponse>(this.helper.GetResponseJson())
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