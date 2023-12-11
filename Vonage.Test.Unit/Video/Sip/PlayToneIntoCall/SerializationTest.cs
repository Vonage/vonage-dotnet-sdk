using System;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.PlayToneIntoCall
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(Guid.NewGuid())
                .WithSessionId("414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d")
                .WithDigits("1713")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}