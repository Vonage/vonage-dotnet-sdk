using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.WhatsApp;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
/// Represents the base builder for StartVerificationRequest.
/// </summary>
public static class StartVerificationRequestBuilder
{
    /// <summary>
    ///     Returns a builder for SMS verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrandV2<SmsWorkflow> ForSms() =>
        new StartVerificationRequestBuilderV2<SmsWorkflow>();

    /// <summary>
    ///     Returns a builder for WhatsApp verification request.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrandV2<WhatsAppWorkflow> ForWhatsApp() =>
        new StartVerificationRequestBuilderV2<WhatsAppWorkflow>();
}

/// <summary>
///     Represents a builder for a StartVerificationRequest.
/// </summary>
/// <typeparam name="T">Type of the workflow.</typeparam>
internal class StartVerificationRequestBuilderV2<T> :
    IOptionalBuilderV2<T>,
    IBuilderForBrandV2<T>,
    IBuilderForWorkflowV2<T>
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
    public IBuilderForWorkflowV2<T> WithBrand(string value)
    {
        this.brand = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilderV2<T> WithChannelTimeout(int value)
    {
        this.channelTimeout = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilderV2<T> WithClientReference(string value)
    {
        this.clientReference = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilderV2<T> WithCodeLength(int value)
    {
        this.codeLength = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilderV2<T> WithLocale(Locale value)
    {
        this.locale = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilderV2<T> WithWorkflow(Result<T> value)
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
public interface IOptionalOptionalBuilderForLocaleV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Locale.
    /// </summary>
    /// <param name="value">The Locale.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilderV2<T> WithLocale(Locale value);
}

/// <summary>
///     Represents a builder for ChannelTimeout.
/// </summary>
public interface IOptionalOptionalBuilderForChannelTimeoutV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the ChannelTimeout.
    /// </summary>
    /// <param name="value">The ChannelTimeout.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilderV2<T> WithChannelTimeout(int value);
}

/// <summary>
///     Represents a builder for ClientReference.
/// </summary>
public interface IOptionalOptionalBuilderForClientReferenceV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the ClientReference.
    /// </summary>
    /// <param name="value">The ClientReference.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilderV2<T> WithClientReference(string value);
}

/// <summary>
///     Represents a builder for CodeLength.
/// </summary>
public interface IOptionalBuilderForCodeLengthV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the CodeLength.
    /// </summary>
    /// <param name="value">The CodeLength.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilderV2<T> WithCodeLength(int value);
}

/// <summary>
///     Represents a builder for Brand.
/// </summary>
public interface IBuilderForBrandV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Brand.
    /// </summary>
    /// <param name="value">The Brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForWorkflowV2<T> WithBrand(string value);
}

/// <summary>
///     Represents a builder for Workflow.
/// </summary>
public interface IBuilderForWorkflowV2<T> where T : IVerificationWorkflow
{
    /// <summary>
    ///     Sets the Workflow.
    /// </summary>
    /// <param name="value">The Workflow.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilderV2<T> WithWorkflow(Result<T> value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilderV2<T> :
    IVonageRequestBuilder<StartVerificationRequest<T>>,
    IOptionalOptionalBuilderForLocaleV2<T>,
    IOptionalOptionalBuilderForChannelTimeoutV2<T>,
    IOptionalOptionalBuilderForClientReferenceV2<T>,
    IOptionalBuilderForCodeLengthV2<T>
    where T : IVerificationWorkflow
{
}