using System.Text.Json;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Sessions.GetStreams;
using Xunit;

namespace Vonage.Test.Unit.Video.Sessions.GetStreams
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase));

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetStreamsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success => { VerifyStream(success); });

        internal static void VerifyStream(GetStreamsResponse success)
        {
            success.Count.Should().Be(1);
            success.Items.Length.Should().Be(1);
            success.Items[0].Id.Should().Be("8b732909-0a06-46a2-8ea8-074e64d43422");
            success.Items[0].VideoType.Should().Be("camera");
            success.Items[0].Name.Should().Be("random");
            success.Items[0].LayoutClassList.Length.Should().Be(1);
            success.Items[0].LayoutClassList[0].Should().Be("full");
        }
    }
}