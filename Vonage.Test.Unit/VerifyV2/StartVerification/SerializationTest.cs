using System;
using Vonage.Common;
using Vonage.Common.Monads;
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
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<StartVerificationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new StartVerificationResponse(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315"),
                    Maybe<Uri>.None));

        [Fact]
        public void ShouldDeserialize200WithCheckUrl() =>
            this.helper.Serializer
                .DeserializeObject<StartVerificationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new StartVerificationResponse(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315"),
                    new Uri(
                        "https://api.nexmo.com/v2/verify/c11236f4-00bf-4b89-84ba-88b25df97315/silent-auth/redirect")));

        [Fact]
        public void ShouldSerialize() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .WithLocale(Locale.EsEs)
                .WithChannelTimeout(300)
                .WithClientReference("my-personal-reference")
                .WithCodeLength(4)
                .SkipFraudCheck()
                .WithCode("123456")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeEmailWorkflow() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeFallbackWorkflows() =>
            StartVerificationRequest.Build()
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
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SilentAuthWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeSmsWorkflow() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeVoiceWorkflow() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWhatsAppInteractiveWorkflow() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWhatsAppWorkflow() =>
            StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}