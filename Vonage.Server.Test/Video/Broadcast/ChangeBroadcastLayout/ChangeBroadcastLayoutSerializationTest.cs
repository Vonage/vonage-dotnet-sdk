using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class ChangeBroadcastLayoutSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public ChangeBroadcastLayoutSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(ChangeBroadcastLayoutSerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldSerialize() =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithBroadcastId("broadcastId")
                .WithLayout(new ArchiveLayout
                {
                    Type = LayoutType.BestFit,
                    ScreenshareType = LayoutType.Pip,
                    Stylesheet = "stream.instructor {position: absolute; width: 100%;  height:50%;}",
                })
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}