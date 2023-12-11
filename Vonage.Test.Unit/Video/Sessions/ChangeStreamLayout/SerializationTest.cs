using System;
using System.Text.Json;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Xunit;

namespace Vonage.Test.Unit.Video.Sessions.ChangeStreamLayout
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() => this.helper =
            new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase));

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