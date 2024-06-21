using System;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.GetSession;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<Session>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedSession());

    internal static Session BuildExpectedSession() =>
        new Session("1248e7070b81464c9789f46ad10e7764",
            "flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN",
            new Guid("93e36bb9-b72c-45b6-a9ea-5c37dbc49906"),
            1437676551000,
            new Uri("https://example.com/video/events"),
            1437676551000,
            "Composed stream for Live event #1",
            new Uri("https://example.com/"),
            RenderResolution.HighDefinitionPortrait,
            SessionStatus.Failed,
            "e32445b743678c98230f238",
            "Could not load URL");
}