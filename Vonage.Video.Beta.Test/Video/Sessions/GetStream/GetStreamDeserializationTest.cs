using FluentAssertions;
using Vonage.Video.Beta.Test.Common;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetStreamDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetStreamDeserializationTest).Namespace);

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetStreamResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be("8b732909-0a06-46a2-8ea8-074e64d43422");
                    success.VideoType.Should().Be("camera");
                    success.Name.Should().Be("random");
                    success.LayoutClassList.Length.Should().Be(1);
                    success.LayoutClassList[0].Should().Be("full");
                });
    }
}