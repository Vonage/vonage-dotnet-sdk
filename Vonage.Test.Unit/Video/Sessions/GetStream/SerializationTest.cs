using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Sessions.GetStream;
using Xunit;

namespace Vonage.Test.Unit.Video.Sessions.GetStream
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetStreamResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyStream);

        internal static void VerifyStream(GetStreamResponse success)
        {
            success.Id.Should().Be("8b732909-0a06-46a2-8ea8-074e64d43422");
            success.VideoType.Should().Be("camera");
            success.Name.Should().Be("random");
            success.LayoutClassList.Length.Should().Be(1);
            success.LayoutClassList[0].Should().Be("full");
        }
    }
}