using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldSerialize() =>
            ChangeBroadcastLayoutRequestBuilder.Build()
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