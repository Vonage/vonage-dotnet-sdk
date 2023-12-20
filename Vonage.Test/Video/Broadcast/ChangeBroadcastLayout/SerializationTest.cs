using System;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            ChangeBroadcastLayoutRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId(Guid.NewGuid())
                .WithLayout(new Layout(LayoutType.Pip,
                    "stream.instructor {position: absolute; width: 100%;  height:50%;}", LayoutType.BestFit))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}