using System;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

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
            .WithFallbackWorkflow(WhatsAppWorkflow.Parse("447700900000", "447700900001"))
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
    public void ShouldSerializeSilentAuthWorkflowWithRedirectUrl() =>
        StartVerificationRequest.Build()
            .WithBrand("ACME, Inc")
            .WithWorkflow(SilentAuthWorkflow.Parse("447700900000", new Uri("https://acme-app.com/sa/redirect")))
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeSmsWorkflow() =>
        StartVerificationRequest.Build()
            .WithBrand("ACME, Inc")
            .WithWorkflow(BuildSmsWorkflow())
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<SmsWorkflow> BuildSmsWorkflow() => SmsWorkflow.Parse("447700900000");

    [Fact]
    public void ShouldSerializeSmsWorkflowWithOptionalValues() =>
        StartVerificationRequest.Build()
            .WithBrand("ACME, Inc")
            .WithWorkflow(BuildSmsWorkflowWithOptionalValues())
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<SmsWorkflow> BuildSmsWorkflowWithOptionalValues() =>
        SmsWorkflow.Parse("447700900000", "12345678901", "entity", "content", "447700900001");

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
            .WithWorkflow(WhatsAppWorkflow.Parse("447700900000", "447700900001"))
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}