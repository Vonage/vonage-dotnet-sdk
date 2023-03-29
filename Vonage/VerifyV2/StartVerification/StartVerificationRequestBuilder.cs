using System;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using IBuilderForBrand = Vonage.VerifyV2.StartVerification.Sms.IBuilderForBrand;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents the base builder for StartVerificationRequest.
/// </summary>
public static class StartVerificationRequestBuilder
{
    public static StartEmailVerificationRequestBuilder ForEmail() => throw new NotImplementedException();
    public static StartSilentAuthVerificationRequestBuilder ForSilentAuth() => throw new NotImplementedException();

    /// <summary>
    ///     Returns a builder for SMS verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand ForSms() => new StartSmsVerificationRequestBuilder();

    public static StartVoiceVerificationRequestBuilder ForVoice() => throw new NotImplementedException();

    /// <summary>
    ///     Returns a builder for WhatsApp verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static WhatsApp.IBuilderForBrand ForWhatsApp() =>
        new StartWhatsAppVerificationRequestBuilder();

    public static StartWhatsAppInteractiveVerificationRequestBuilder ForWhatsAppInteractive() =>
        throw new NotImplementedException();
}