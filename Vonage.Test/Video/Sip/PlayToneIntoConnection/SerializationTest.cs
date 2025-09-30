#region
using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sip.PlayToneIntoConnection;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sip.PlayToneIntoConnection;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldSerialize() =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(Guid.NewGuid())
            .WithSessionId("414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d")
            .WithConnectionId("414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d")
            .WithDigits("1713")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}