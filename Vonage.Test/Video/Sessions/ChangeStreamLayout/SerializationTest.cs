using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Xunit;

namespace Vonage.Test.Video.Sessions.ChangeStreamLayout
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() => this.helper =
            new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            ChangeStreamLayoutRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithSessionId("SomeSessionId")
                .WithItem(new ChangeStreamLayoutRequest.LayoutItem("8b732909-0a06-46a2-8ea8-074e64d43422",
                    new[] {"full"}))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}