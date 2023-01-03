using FluentAssertions;
using Vonage.Video.Beta.Test.Common;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions.GetStreams;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStreams
{
    public class GetStreamsDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetStreamsDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetStreamsDeserializationTest).Namespace);

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetStreamsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Count.Should().Be(1);
                    success.Items.Length.Should().Be(1);
                    success.Items[0].Id.Should().Be("8b732909-0a06-46a2-8ea8-074e64d43422");
                    success.Items[0].VideoType.Should().Be("camera");
                    success.Items[0].Name.Should().Be("random");
                    success.Items[0].LayoutClassList.Length.Should().Be(1);
                    success.Items[0].LayoutClassList[0].Should().Be("full");
                });
    }
}