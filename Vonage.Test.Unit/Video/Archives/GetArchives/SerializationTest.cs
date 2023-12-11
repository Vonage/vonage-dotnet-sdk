using System.Text.Json;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Test.Unit.Video.Archives.GetArchives
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
                .DeserializeObject<GetArchivesResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyArchives);

        internal static void VerifyArchives(GetArchivesResponse success)
        {
            success.Count.Should().Be(1);
            success.Items.Length.Should().Be(1);
            success.Items[0].Id.Should().Be("b40ef09b-3811-4726-b508-e41a0f96c68f");
            success.Items[0].Name.Should().Be("Foo");
            success.Items[0].CreatedAt.Should().Be(1384221730000);
            success.Items[0].Duration.Should().Be(5049);
            success.Items[0].HasAudio.Should().BeTrue();
            success.Items[0].HasVideo.Should().BeTrue();
            success.Items[0].ApplicationId.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
            success.Items[0].Reason.Should().Be("random");
            success.Items[0].Resolution.Should().Be(RenderResolution.FullHighDefinitionLandscape);
            success.Items[0].SessionId.Should().Be("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN");
            success.Items[0].Size.Should().Be(247748791);
            success.Items[0].Status.Should().Be("available");
            success.Items[0].StreamMode.Should().Be("manual");
            success.Items[0].Url.Should()
                .Be(
                    "https://tokbox.com.archive2.s3.amazonaws.com/123456/09141e29-8770-439b-b180-337d7e637545/archive.mp4");
            success.Items[0].Streams.Length.Should().Be(1);
            success.Items[0].Streams[0].StreamId.Should().Be("abc123");
            success.Items[0].Streams[0].HasAudio.Should().BeTrue();
            success.Items[0].Streams[0].HasVideo.Should().BeTrue();
        }
    }
}