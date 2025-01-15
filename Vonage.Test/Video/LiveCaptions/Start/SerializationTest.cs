#region
using System;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.LiveCaptions.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.LiveCaptions.Start;

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

    [Fact]
    public void ShouldSerializeWithOptionalValues() => BuildRequestWithOptionalValues()
        .GetStringContent()
        .Should()
        .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<StartResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedResponse());

    internal static StartResponse BuildExpectedResponse() => new StartResponse("7c0680fc-6274-4de5-a66f-d0648e8d3ac2");

    internal static Result<StartRequest> BuildRequest() =>
        StartRequest
            .Build()
            .WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"))
            .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
            .WithToken("830c9c9d-d09e-4513-9cc8-29c90a760248")
            .Create();

    internal static Result<StartRequest> BuildRequestWithOptionalValues() =>
        StartRequest
            .Build()
            .WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"))
            .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
            .WithToken("830c9c9d-d09e-4513-9cc8-29c90a760248")
            .WithMaxDuration(10000)
            .WithLanguage("fr-FR")
            .WithStatusCallbackUrl(new Uri("https://example.com"))
            .DisablePartialCaptions()
            .Create();
}