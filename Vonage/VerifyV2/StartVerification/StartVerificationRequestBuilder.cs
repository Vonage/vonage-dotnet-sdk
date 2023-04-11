using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a builder for a StartVerificationRequest.
/// </summary>
internal class StartVerificationRequestBuilder :
    IOptionalBuilder,
    IBuilderForBrand,
    IBuilderForWorkflow
{
    private int channelTimeout = 300;
    private int codeLength = 4;
    private readonly List<IVerificationWorkflow> workflows = new();
    private Locale locale = Locale.EnUs;
    private Maybe<IResultFailure> failure = Maybe<IResultFailure>.None;
    private Maybe<string> clientReference = Maybe<string>.None;
    private string brand;

    /// <summary>
    ///     Initializes a builder for StartVerificationRequest.
    /// </summary>
    /// <returns></returns>
    public static IBuilderForBrand Build() => new StartVerificationRequestBuilder();

    /// <inheritdoc />
    public Result<StartVerificationRequest> Create() =>
        this.failure
            .Match(
                Result<StartVerificationRequest>.FromFailure,
                () => Result<StartVerificationRequest>.FromSuccess(new StartVerificationRequest
                {
                    Brand = this.brand,
                    Locale = this.locale,
                    ChannelTimeout = this.channelTimeout,
                    ClientReference = this.clientReference,
                    CodeLength = this.codeLength,
                    Workflows = this.workflows.ToArray(),
                }))
            .Bind(VerifyWorkflowsNotEmpty)
            .Bind(VerifyBrandNotEmpty)
            .Bind(VerifyChannelTimeoutHigherThanMinimum)
            .Bind(VerifyChannelTimeoutLowerThanMaximum)
            .Bind(VerifyCodeLengthHigherThanMinimum)
            .Bind(VerifyCodeLengthLowerThanMaximum);

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
    public IOptionalBuilder WithFallbackWorkflow<T>(Result<T> value) where T : IVerificationWorkflow
    {
        value.Match(this.AddWorkflow, this.SetFailure);
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithLocale(Locale value)
    {
        this.locale = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithWorkflow<T>(Result<T> value) where T : IVerificationWorkflow
    {
        value.Match(this.AddWorkflow, this.SetFailure);
        return this;
    }

    private Unit AddWorkflow<T>(T workflow) where T : IVerificationWorkflow
    {
        this.workflows.Add(workflow);
        return Unit.Default;
    }

    private Unit SetFailure(IResultFailure failureValue)
    {
        if (this.failure.IsNone)
        {
            this.failure = Maybe<IResultFailure>.Some(failureValue);
        }

        return Unit.Default;
    }

    private StartVerificationRequest ToVerificationRequest(IVerificationWorkflow value) =>
        new()
        {
            Brand = this.brand,
            Locale = this.locale,
            ChannelTimeout = this.channelTimeout,
            ClientReference = this.clientReference,
            CodeLength = this.codeLength,
            Workflows = new[] {value},
        };

    private static Result<StartVerificationRequest> VerifyBrandNotEmpty(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Brand, nameof(request.Brand));

    private static Result<StartVerificationRequest> VerifyChannelTimeoutHigherThanMinimum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.ChannelTimeout, 60, nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest> VerifyChannelTimeoutLowerThanMaximum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.ChannelTimeout, 900, nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest> VerifyCodeLengthHigherThanMinimum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.CodeLength, 4, nameof(request.CodeLength));

    private static Result<StartVerificationRequest> VerifyCodeLengthLowerThanMaximum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.CodeLength, 10, nameof(request.CodeLength));

    private static Result<StartVerificationRequest> VerifyWorkflowsNotEmpty(StartVerificationRequest request) =>
        request.Workflows.Any()
            ? request
            : Result<StartVerificationRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Workflows should contain at least one element."));
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
///     Represents a builder for fallback workflow.
/// </summary>
public interface IOptionalBuilderForFallbackWorkflow
{
    /// <summary>
    ///     Sets a fallback workflow.
    /// </summary>
    /// <param name="value">The fallback workflow.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithFallbackWorkflow<T>(Result<T> value) where T : IVerificationWorkflow;
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
    IOptionalBuilder WithWorkflow<T>(Result<T> value) where T : IVerificationWorkflow;
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilder :
    IVonageRequestBuilder<StartVerificationRequest>,
    IOptionalOptionalBuilderForLocale,
    IOptionalOptionalBuilderForChannelTimeout,
    IOptionalOptionalBuilderForClientReference,
    IOptionalBuilderForCodeLength,
    IOptionalBuilderForFallbackWorkflow
{
}