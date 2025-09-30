#region
using System;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives;
using Vonage.Video.Archives.CreateArchive;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.CreateArchive;

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
            .BeSuccess(ArchiveTest.VerifyArchive);

    [Fact]
    public void ShouldSerialize() =>
        BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeDefault() =>
        BuildDefaultRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<CreateArchiveRequest> BuildRequest() =>
        CreateArchiveRequest
            .Build()
            .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
            .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
            .WithLayout(new Layout(LayoutType.Pip,
                "stream.instructor {position: absolute; width: 100%;  height:50%;}", LayoutType.BestFit))
            .WithName("Foo")
            .WithOutputMode(OutputMode.Individual)
            .WithResolution(RenderResolution.HighDefinitionLandscape)
            .WithStreamMode(StreamMode.Manual)
            .DisableVideo()
            .DisableAudio()
            .WithMultiArchiveTag("custom-tag")
            .WithMaxBitrate(3000000)
            .EnableTranscription()
            .WithTranscription(new TranscriptionProperties {HasSummary = true, PrimaryLanguageCode = "en-US"})
            .Create();

    internal static Result<CreateArchiveRequest> BuildDefaultRequest() =>
        CreateArchiveRequest
            .Build()
            .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
            .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
            .Create();
}