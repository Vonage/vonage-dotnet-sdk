#region
using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;
#endregion

namespace Vonage.Test.Video.Broadcast.RemoveStreamFromBroadcast;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldSerialize() =>
        RemoveStreamFromBroadcastRequest.Build()
            .WithApplicationId(Guid.NewGuid())
            .WithBroadcastId(Guid.NewGuid())
            .WithStreamId(new Guid("12312312-3811-4726-b508-e41a0f96c68f"))
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}