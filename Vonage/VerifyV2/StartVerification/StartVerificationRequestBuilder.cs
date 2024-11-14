#region
using System;
using System.Collections.Generic;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.StartVerification;

internal class StartVerificationRequestBuilder :
    IOptionalBuilder,
    IBuilderForBrand,
    IBuilderForWorkflow
{
    private const int MaxBrandLength = 16;

    private const int MinChannelTimeout = 15;
    private const int MaxChannelTimeout = 900;

    private const int MinCodeLength = 4;
    private const int MaxCodeLength = 10;
    private readonly List<IVerificationWorkflow> workflows = new List<IVerificationWorkflow>();
    private string brand;
    private int channelTimeout = 300;
    private Maybe<string> clientReference = Maybe<string>.None;
    private Maybe<string> code;
    private int codeLength = 4;
    private Maybe<IResultFailure> failure = Maybe<IResultFailure>.None;
    private bool fraudCheck = true;
    private Locale locale = Locale.EnUs;
    private Maybe<Guid> templateId;

    /// <inheritdoc />
    public IBuilderForWorkflow WithBrand(string value)
    {
        this.brand = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithWorkflow<T>(Result<T> value) where T : IVerificationWorkflow => this.SetWorkflow(value);

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
                    FraudCheck = this.fraudCheck,
                    Code = this.code,
                    TemplateId = this.templateId,
                }))
            .Map(InputEvaluation<StartVerificationRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(
                VerifyBrandNotEmpty,
                VerifyBrandLength,
                VerifyChannelTimeoutHigherThanMinimum,
                VerifyChannelTimeoutLowerThanMaximum,
                VerifyCodeLengthHigherThanMinimum,
                VerifyCodeLengthLowerThanMaximum));

    /// <inheritdoc />
    public IOptionalBuilder SkipFraudCheck()
    {
        this.fraudCheck = false;
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
    public IOptionalBuilder WithCode(string value)
    {
        this.code = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithCodeLength(int value)
    {
        this.codeLength = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithFallbackWorkflow<T>(Result<T> value) where T : IVerificationWorkflow =>
        this.SetWorkflow(value);

    /// <inheritdoc />
    public IOptionalBuilder WithLocale(string value)
    {
        this.locale = value;
        return this;
    }

    public IOptionalBuilder WithTemplateId(Guid value)
    {
        this.templateId = value;
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

    private IOptionalBuilder SetWorkflow<T>(Result<T> value) where T : IVerificationWorkflow
    {
        value.Match(this.AddWorkflow, this.SetFailure);
        return this;
    }

    private static Result<StartVerificationRequest> VerifyBrandNotEmpty(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Brand, nameof(request.Brand));

    private static Result<StartVerificationRequest> VerifyBrandLength(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyLengthLowerOrEqualThan(request, request.Brand, MaxBrandLength, nameof(request.Brand));

    private static Result<StartVerificationRequest> VerifyChannelTimeoutHigherThanMinimum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.ChannelTimeout, MinChannelTimeout,
                nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest> VerifyChannelTimeoutLowerThanMaximum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.ChannelTimeout, MaxChannelTimeout, nameof(request.ChannelTimeout));

    private static Result<StartVerificationRequest> VerifyCodeLengthHigherThanMinimum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.CodeLength, MinCodeLength, nameof(request.CodeLength));

    private static Result<StartVerificationRequest> VerifyCodeLengthLowerThanMaximum(
        StartVerificationRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.CodeLength, MaxCodeLength, nameof(request.CodeLength));
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
    IOptionalBuilder WithLocale(string value);
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
///     Represents a builder for Code.
/// </summary>
public interface IOptionalBuilderForCode
{
    /// <summary>
    ///     Sets a custom code, if you don't want Vonage to generate the code.
    /// </summary>
    /// <param name="value">The custom code.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithCode(string value);
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
///     Represents a builder for SkipFraudCheck.
/// </summary>
public interface IOptionalBuilderForSkipFraudCheck
{
    /// <summary>
    ///     Sets a fallback workflow.
    /// </summary>
    /// <returns>The builder.</returns>
    IOptionalBuilder SkipFraudCheck();
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
///     Represents a builder for TemplateId.
/// </summary>
public interface IOptionalOptionalBuilderForTemplateId
{
    /// <summary>
    ///     Sets the TemplateId.
    /// </summary>
    /// <param name="value">The template id.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithTemplateId(Guid value);
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
    IOptionalBuilderForFallbackWorkflow,
    IOptionalBuilderForCode,
    IOptionalBuilderForSkipFraudCheck,
    IOptionalOptionalBuilderForTemplateId
{
}