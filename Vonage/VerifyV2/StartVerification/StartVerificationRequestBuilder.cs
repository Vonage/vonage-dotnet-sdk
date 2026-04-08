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
///     Builder interface for setting the locale on a verification request.
/// </summary>
public interface IOptionalOptionalBuilderForLocale
{
    /// <summary>
    ///     Sets the language locale for verification messages.
    /// </summary>
    /// <param name="value">The IETF BCP 47 locale code (e.g., "en-us", "de-de"). Default is "en-us".</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithLocale("de-de")
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithLocale(string value);
}

/// <summary>
///     Builder interface for setting the channel timeout on a verification request.
/// </summary>
public interface IOptionalOptionalBuilderForChannelTimeout
{
    /// <summary>
    ///     Sets the wait time in seconds between delivery attempts.
    /// </summary>
    /// <param name="value">The timeout in seconds (15-900). Default is 300.</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannelTimeout(60)
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithChannelTimeout(int value);
}

/// <summary>
///     Builder interface for setting the client reference on a verification request.
/// </summary>
public interface IOptionalOptionalBuilderForClientReference
{
    /// <summary>
    ///     Sets an optional client reference for tracking in webhook callbacks.
    /// </summary>
    /// <param name="value">An alphanumeric reference string (1-40 characters).</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithClientReference("my-ref-12345")
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithClientReference(string value);
}

/// <summary>
///     Builder interface for setting the PIN code length on a verification request.
/// </summary>
public interface IOptionalBuilderForCodeLength
{
    /// <summary>
    ///     Sets the length of the generated PIN code.
    /// </summary>
    /// <param name="value">The code length (4-10 digits). Default is 4.</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithCodeLength(6)
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithCodeLength(int value);
}

/// <summary>
///     Builder interface for setting a custom PIN code on a verification request.
/// </summary>
public interface IOptionalBuilderForCode
{
    /// <summary>
    ///     Sets a custom alphanumeric PIN code instead of using auto-generation. Only available on the Verify Conversion pricing model.
    /// </summary>
    /// <param name="value">The custom code (4-10 alphanumeric characters).</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithCode("ABC123")
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithCode(string value);
}

/// <summary>
///     Builder interface for adding fallback workflows to a verification request.
/// </summary>
public interface IOptionalBuilderForFallbackWorkflow
{
    /// <summary>
    ///     Adds a fallback workflow to try if previous workflows fail or time out. Up to 3 total workflows are supported.
    /// </summary>
    /// <typeparam name="T">The workflow type implementing <see cref="IVerificationWorkflow"/>.</typeparam>
    /// <param name="value">The fallback workflow (e.g., <see cref="Voice.VoiceWorkflow"/>).</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithFallbackWorkflow<T>(Result<T> value) where T : IVerificationWorkflow;
}

/// <summary>
///     Builder interface for bypassing fraud checking on a verification request.
/// </summary>
public interface IOptionalBuilderForSkipFraudCheck
{
    /// <summary>
    ///     Disables fraud checking for this verification request. Use with caution for testing or special cases.
    /// </summary>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .SkipFraudCheck()
    /// ]]></code>
    /// </example>
    IOptionalBuilder SkipFraudCheck();
}

/// <summary>
///     Builder interface for setting the brand name on a verification request. This is the first mandatory step.
/// </summary>
public interface IBuilderForBrand
{
    /// <summary>
    ///     Sets the brand name that appears in the verification message.
    /// </summary>
    /// <param name="value">The brand name (1-16 characters). Cannot contain \ / { } : $.</param>
    /// <returns>The builder for setting the primary workflow.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithBrand("MyApp")
    /// ]]></code>
    /// </example>
    IBuilderForWorkflow WithBrand(string value);
}

/// <summary>
///     Builder interface for setting the primary workflow on a verification request. This is required after setting the brand.
/// </summary>
public interface IBuilderForWorkflow
{
    /// <summary>
    ///     Sets the primary verification workflow for delivering the PIN code.
    /// </summary>
    /// <typeparam name="T">The workflow type implementing <see cref="IVerificationWorkflow"/>.</typeparam>
    /// <param name="value">The workflow to use (e.g., <see cref="Sms.SmsWorkflow"/>, <see cref="Voice.VoiceWorkflow"/>).</param>
    /// <returns>The builder for setting optional parameters or creating the request.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithWorkflow(SmsWorkflow.Parse("447700900000"))
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithWorkflow<T>(Result<T> value) where T : IVerificationWorkflow;
}

/// <summary>
///     Builder interface for setting a custom template ID on a verification request.
/// </summary>
public interface IOptionalOptionalBuilderForTemplateId
{
    /// <summary>
    ///     Sets a custom template ID to use for SMS or Voice messages.
    /// </summary>
    /// <param name="value">The UUID of the custom template.</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithTemplateId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    IOptionalBuilder WithTemplateId(Guid value);
}

/// <summary>
///     Composite builder interface for setting optional parameters on a <see cref="StartVerificationRequest"/>.
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