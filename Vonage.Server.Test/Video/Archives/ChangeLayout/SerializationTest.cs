using System;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.ChangeLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.ChangeLayout
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            ChangeLayoutRequest
                .Build()
                .WithApplicationId(Guid.NewGuid())
                .WithArchiveId(Guid.NewGuid())
                .WithLayout(new Layout(LayoutType.Pip,
                    "stream.instructor {position: absolute; width: 100%;  height:50%;}", LayoutType.BestFit))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}