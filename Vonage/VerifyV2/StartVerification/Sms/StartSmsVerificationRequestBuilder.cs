using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <summary>
///     Represents a builder for a StartVerificationRequest over SMS.
/// </summary>
internal class StartSmsVerificationRequestBuilder :
    IOptionalBuilder,
    IBuilderForBrand,
    IBuilderForWorkflow
{
    private int channelTimeout = 300;
    private int codeLength = 4;
    private readonly List<SmsWorkflow> workflows = new();
    private Locale locale = Locale.EnUs;
    private Maybe<string> clientReference = Maybe<string>.None;
    private string brand;

    /// <inheritdoc />
    public Result<StartSmsVerificationRequest> Create() => Result<StartSmsVerificationRequest>.FromSuccess(
            new StartSmsVerificationRequest
            {
                Brand = this.brand,
                Locale = this.locale,
                ChannelTimeout = this.channelTimeout,
                ClientReference = this.clientReference,
                CodeLength = this.codeLength,
                Workflows = this.workflows.ToArray(),
            })
        .Bind(VerifyBrandNotEmpty)
        .Bind(VerifyChannelTimeoutHigherThanMinimum)
        .Bind(VerifyChannelTimeoutLowerThanMaximum)
        .Bind(VerifyCodeLengthHigherThanMinimum)
        .Bind(VerifyCodeLengthLowerThanMaximum)
        .Bind(VerifyWorkflowToNotEmpty)
        .Bind(VerifyWorkflowHashNotEmpty)
        .Bind(VerifyWorkflowHashLength);

    /// <inheritdoc />
    public IBuilderForWorkflow WithBrand(string value)
    {
        this.brand = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithChannelTimeout(int value)
    {
        this.channelTimeout = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithClientReference(string value)
    {
        this.clientReference = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithCodeLength(int value)
    {
        this.codeLength = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithLocale(Locale value)
    {
        this.locale = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithWorkflow(SmsWorkflow value)
    {
        this.workflows.Add(value);
        return this;
    }

    private static Result<StartSmsVerificationRequest> VerifyBrandNotEmpty(
        StartSmsVerificationRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Brand, nameof(request.Brand));

    private static Result<StartSmsVerificationRequest> VerifyChannelTimeoutHigherThanMinimum(
        StartSmsVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.ChannelTimeout, 60, nameof(request.ChannelTimeout));

    private static Result<StartSmsVerificationRequest> VerifyChannelTimeoutLowerThanMaximum(
        StartSmsVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.ChannelTimeout, 900, nameof(request.ChannelTimeout));

    private static Result<StartSmsVerificationRequest> VerifyCodeLengthHigherThanMinimum(
        StartSmsVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.CodeLength, 4, nameof(request.CodeLength));

    private static Result<StartSmsVerificationRequest> VerifyCodeLengthLowerThanMaximum(
        StartSmsVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.CodeLength, 10, nameof(request.CodeLength));

    private static Result<StartSmsVerificationRequest> VerifyWorkflowHashLength(
        StartSmsVerificationRequest request)
    {
        var workflow = request.Workflows.First();
        return workflow.Hash.Match(some => InputValidation.VerifyLength(request, some, 11, nameof(workflow.Hash)),
            () => request);
    }

    private static Result<StartSmsVerificationRequest> VerifyWorkflowHashNotEmpty(
        StartSmsVerificationRequest request)
    {
        var workflow = request.Workflows.First();
        return workflow.Hash.Match(some => InputValidation.VerifyNotEmpty(request, some, nameof(workflow.Hash)),
            () => request);
    }

    private static Result<StartSmsVerificationRequest> VerifyWorkflowToNotEmpty(
        StartSmsVerificationRequest request)
    {
        var workflow = request.Workflows.First();
        return InputValidation.VerifyNotEmpty(request, workflow.To, nameof(workflow.To));
    }
}

/// <summary>
///     Represents a builder for Locale.
/// </summary>
public interface IOptionalOptionalBuilderForLocale
{
    /// <summary>
    ///     Sets the Locale.
    /// </summary>
    /// <param name="value">The Locale.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithLocale(Locale value);
}

/// <summary>
///     Represents a builder for ChannelTimeout.
/// </summary>
public interface IOptionalOptionalBuilderForChannelTimeout
{
    /// <summary>
    ///     Sets the ChannelTimeout.
    /// </summary>
    /// <param name="value">The ChannelTimeout.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithChannelTimeout(int value);
}

/// <summary>
///     Represents a builder for ClientReference.
/// </summary>
public interface IOptionalOptionalBuilderForClientReference
{
    /// <summary>
    ///     Sets the ClientReference.
    /// </summary>
    /// <param name="value">The ClientReference.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithClientReference(string value);
}

/// <summary>
///     Represents a builder for CodeLength.
/// </summary>
public interface IOptionalBuilderForCodeLength
{
    /// <summary>
    ///     Sets the CodeLength.
    /// </summary>
    /// <param name="value">The CodeLength.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithCodeLength(int value);
}

/// <summary>
///     Represents a builder for Brand.
/// </summary>
public interface IBuilderForBrand
{
    /// <summary>
    ///     Sets the Brand.
    /// </summary>
    /// <param name="value">The Brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForWorkflow WithBrand(string value);
}

/// <summary>
///     Represents a builder for Workflow.
/// </summary>
public interface IBuilderForWorkflow
{
    /// <summary>
    ///     Sets the Workflow.
    /// </summary>
    /// <param name="value">The Workflow.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithWorkflow(SmsWorkflow value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilder :
    IVonageRequestBuilder<StartSmsVerificationRequest>,
    IOptionalOptionalBuilderForLocale,
    IOptionalOptionalBuilderForChannelTimeout,
    IOptionalOptionalBuilderForClientReference,
    IOptionalBuilderForCodeLength
{
}