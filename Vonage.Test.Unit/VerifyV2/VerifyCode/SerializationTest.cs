using System;
using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.VerifyCode
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            VerifyCodeRequest.Build()
                .WithRequestId(Guid.NewGuid())
                .WithCode("123456789")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}