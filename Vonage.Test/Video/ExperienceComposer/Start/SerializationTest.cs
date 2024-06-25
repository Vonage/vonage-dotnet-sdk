using System;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.Start;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.Start;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldSerialize() => BuildRequest()
        .GetStringContent()
        .Should()
        .BeSuccess(this.helper.GetRequestJson());

    internal static Result<StartRequest> BuildRequest() =>
        StartRequest
            .Build()
            .WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"))
            .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
            .WithToken("830c9c9d-d09e-4513-9cc8-29c90a760248")
            .WithUrl(new Uri("https://example.com/"))
            .WithResolution(RenderResolution.StandardDefinitionLandscape)
            .WithName("Composed stream for Live event #1")
            .WithMaxDuration(1800)
            .Create();
}