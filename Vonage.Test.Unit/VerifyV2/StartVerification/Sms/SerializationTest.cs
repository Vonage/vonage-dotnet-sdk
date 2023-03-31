using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Sms
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<StartVerificationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success => success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));

        [Fact]
        public void ShouldSerialize() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000", "12345678901"))
                .WithLocale(Locale.EsEs)
                .WithChannelTimeout(300)
                .WithClientReference("my-personal-reference")
                .WithCodeLength(4)
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeDefaultValues() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}