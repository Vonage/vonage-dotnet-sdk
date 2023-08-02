using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Sessions.GetStream;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.GetStream
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build());

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