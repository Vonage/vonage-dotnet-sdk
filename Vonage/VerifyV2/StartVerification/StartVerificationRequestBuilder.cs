using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents the base builder for StartVerificationRequest.
/// </summary>
public static class StartVerificationRequestBuilder
{
    /// <summary>
    ///     Returns a builder for Email verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand<EmailWorkflow> ForEmail() =>
        new StartVerificationRequestBuilder<EmailWorkflow>();

    /// <summary>
    ///     Returns a builder for SilentAuth verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand ForSilentAuth() =>
        new StartSilentAuthVerificationRequestBuilder();

    /// <summary>
    ///     Returns a builder for SMS verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand<SmsWorkflow> ForSms() =>
        new StartVerificationRequestBuilder<SmsWorkflow>();

    /// <summary>
    ///     Returns a builder for Voice verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand<VoiceWorkflow> ForVoice() =>
        new StartVerificationRequestBuilder<VoiceWorkflow>();

    /// <summary>
    ///     Returns a builder for WhatsApp verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand<WhatsAppWorkflow> ForWhatsApp() =>
        new StartVerificationRequestBuilder<WhatsAppWorkflow>();

    /// <summary>
    ///     Returns a builder for WhatsAppInteractive verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand<WhatsAppInteractiveWorkflow> ForWhatsAppInteractive() =>
        new StartVerificationRequestBuilder<WhatsAppInteractiveWorkflow>();
}

/// <summary>
///     Represents a builder for a StartVerificationRequest.
/// </summary>
/// <typeparam name="T">Type of the workflow.</typeparam>
internal class StartVerificationRequestBuilder<T> :
    IOptionalBuilder<T>,
    IBuilderForBrand<T>,
    IBuilderForWorkflow<T>
    where T : IVerificationWorkflow
{
    private int channelTimeout = 300;
    private int codeLength = 4;
    private Locale locale = Locale.EnUs;
    private Maybe<string> clientReference = Maybe<string>.None;
    private Result<T> workflow;

    private string brand;

    /// <inheritdoc />
    public Result<StartVerificationRequest<T>> Create() =>
        this.workflow
            .Map(this.ToVerificationRequest)
            .Bind(VerifyBrandNotEmpty)
            .Bind(VerifyChannelTimeoutHigherThanMinimum)
            .Bind(VerifyChannelTimeoutLowerThanMaximum)
            .Bind(VerifyCodeLengthHigherThanMinimum)
            .Bind(VerifyCodeLengthLowerThanMaximum);

    /// <inheritdoc />
    public IBuilderForWorkflow<T> WithBrand(string value)
    {
        this.brand = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder<T> WithChannelTimeout(int value)
    {
        this.channelTimeout = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder<T> WithClientReference(string value)
    {
        this.clientReference = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder<T> WithCodeLength(int value)
    {
        this.codeLength = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder<T> WithLocale(Locale value)
    {
        this.locale = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder<T> WithWorkflow(Result<T> value)
    {
        this.workflow = value;
        return this;
    }

    private StartVerificationRequest<T> ToVerificationRequest(T value) =>
        new()
        {
            Brand = this.brand,
            Locale = this.locale,
            ChannelTimeout = this.channelTimeout,
            ClientReference = this.clientReference,
            CodeLength = this.codeLength,
            Workflows = new[] {value},
        };

    private static Result<StartVerificationRequest<T>> VerifyBrandNotEmpty(
        StartVerificationRequest<T> request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Brand, nameof(request.Brand));

    private static Result<StartVerificationRequest<T>> VerifyChannelTimeoutHigherThanMinimum(
        StartVerificationRequest<T> request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.ChannelTimeout, 60, nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest<T>> VerifyChannelTimeoutLowerThanMaximum(
        StartVerificationRequest<T> request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.ChannelTimeout, 900, nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest<T>> VerifyCodeLengthHigherThanMinimum(
        StartVerificationRequest<T> request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.CodeLength, 4, nameof(request.CodeLength));

    private static Result<StartVerificationRequest<T>> VerifyCodeLengthLowerThanMaximum(
        StartVerificationRequest<T> request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.CodeLength, 10, nameof(request.CodeLength));
}

/// <summary>
///     Represents a builder for Locale.
/// </summary>
public interface IOptionalOptionalBuilderForLocale<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Locale.
    /// </summary>
    /// <param name="value">The Locale.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder<T> WithLocale(Locale value);
}

/// <summary>
///     Represents a builder for ChannelTimeout.
/// </summary>
public interface IOptionalOptionalBuilderForChannelTimeout<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the ChannelTimeout.
    /// </summary>
    /// <param name="value">The ChannelTimeout.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder<T> WithChannelTimeout(int value);
}

/// <summary>
///     Represents a builder for ClientReference.
/// </summary>
public interface IOptionalOptionalBuilderForClientReference<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the ClientReference.
    /// </summary>
    /// <param name="value">The ClientReference.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder<T> WithClientReference(string value);
}

/// <summary>
///     Represents a builder for CodeLength.
/// </summary>
public interface IOptionalBuilderForCodeLength<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the CodeLength.
    /// </summary>
    /// <param name="value">The CodeLength.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder<T> WithCodeLength(int value);
}

/// <summary>
///     Represents a builder for Brand.
/// </summary>
public interface IBuilderForBrand<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Brand.
    /// </summary>
    /// <param name="value">The Brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForWorkflow<T> WithBrand(string value);
}

/// <summary>
///     Represents a builder for Workflow.
/// </summary>
public interface IBuilderForWorkflow<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Workflow.
    /// </summary>
    /// <param name="value">The Workflow.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder<T> WithWorkflow(Result<T> value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilder<T> :
    IVonageRequestBuilder<StartVerificationRequest<T>>,
    IOptionalOptionalBuilderForLocale<T>,
    IOptionalOptionalBuilderForChannelTimeout<T>,
    IOptionalOptionalBuilderForClientReference<T>,
    IOptionalBuilderForCodeLength<T>
    where T : IVerificationWorkflow
{
}