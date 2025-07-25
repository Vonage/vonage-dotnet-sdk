﻿#region
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.StopArchive;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<Archive>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyArchive);

    internal static void VerifyArchive(Archive success)
    {
        success.Id.Should().Be("b40ef09b-3811-4726-b508-e41a0f96c68f");
        success.Name.Should().Be("Foo");
        success.CreatedAt.Should().Be(1384221730000);
        success.Duration.Should().Be(5049);
        success.HasAudio.Should().BeTrue();
        success.HasVideo.Should().BeTrue();
        success.ApplicationId.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
        success.Reason.Should().Be("random");
        success.Resolution.Should().Be(RenderResolution.FullHighDefinitionLandscape);
        success.SessionId.Should().Be("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN");
        success.Size.Should().Be(247748791);
        success.Status.Should().Be("available");
        success.StreamMode.Should().Be("manual");
        success.Url.Should()
            .Be(
                "https://tokbox.com.archive2.s3.amazonaws.com/123456/09141e29-8770-439b-b180-337d7e637545/archive.mp4");
        success.MultiArchiveTag.Should().Be("custom-tag");
        success.MaxBitrate.Should().Be(200000);
        success.QuantizationParameter.Should().Be(15);
        success.Streams.Length.Should().Be(1);
        success.Streams[0].StreamId.Should().Be("abc123");
        success.Streams[0].HasAudio.Should().BeTrue();
        success.Streams[0].HasVideo.Should().BeTrue();
    }
}