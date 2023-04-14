using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<StartVerificationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success => success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));

        [Fact]
        public void ShouldSerialize() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .WithLocale(Locale.EsEs)
                .WithChannelTimeout(300)
                .WithClientReference("my-personal-reference")
                .WithCodeLength(4)
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeEmailWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeFallbackWorkflows() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeSilentAuthWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SilentAuthWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeSmsWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeVoiceWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWhatsAppInteractiveWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWhatsAppWorkflow() =>
            StartVerificationRequestBuilder.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}