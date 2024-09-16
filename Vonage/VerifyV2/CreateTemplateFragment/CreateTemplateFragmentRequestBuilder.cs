#region
using System;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.VerifyV2.StartVerification;
#endregion

namespace Vonage.VerifyV2.CreateTemplateFragment;

internal struct CreateTemplateFragmentRequestBuilder
    : IVonageRequestBuilder<CreateTemplateFragmentRequest>, IBuilderForText, IBuilderForChannel, IBuilderForLocale,
        IBuilderForTemplateId
{
    private VerificationChannel channel;
    private Locale locale;
    private Guid templateId;
    private string text;

    public IVonageRequestBuilder<CreateTemplateFragmentRequest> WithChannel(VerificationChannel value) =>
        this with {channel = value};

    public IBuilderForChannel WithLocale(Locale value) => this with {locale = value};
    public IBuilderForText WithTemplateId(Guid value) => this with {templateId = value};
    public IBuilderForLocale WithText(string value) => this with {text = value};

    public Result<CreateTemplateFragmentRequest> Create() => Result<CreateTemplateFragmentRequest>.FromSuccess(
            new CreateTemplateFragmentRequest
            {
                Text = this.text,
                Channel = this.channel,
                Locale = this.locale,
                TemplateId = this.templateId,
            })
        .Map(InputEvaluation<CreateTemplateFragmentRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyTemplate, VerifyName, VerifyChannel));

    private static Result<CreateTemplateFragmentRequest> VerifyName(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Text, nameof(request.Text));

    private static Result<CreateTemplateFragmentRequest> VerifyTemplate(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    private static Result<CreateTemplateFragmentRequest> VerifyChannel(
        CreateTemplateFragmentRequest request) =>
        IsChannelSupported(request.Channel)
            ? request
            : Result<CreateTemplateFragmentRequest>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{nameof(request.Channel)} must be one of {VerificationChannel.Sms}, {VerificationChannel.Voice} or {VerificationChannel.Email}."));

    private static bool IsChannelSupported(VerificationChannel channel) =>
        new[] {VerificationChannel.Sms, VerificationChannel.Voice, VerificationChannel.Email}.Contains(channel);
}

/// <summary>
///     Represents a builder to set the TemplateId.
/// </summary>
public interface IBuilderForTemplateId
{
    /// <summary>
    ///     Sets the TemplateId.
    /// </summary>
    /// <param name="value">The templateId.</param>
    /// <returns>The builder.</returns>
    IBuilderForText WithTemplateId(Guid value);
}

/// <summary>
///     Represents a builder to set the Text.
/// </summary>
public interface IBuilderForText
{
    /// <summary>
    ///     Sets the Text.
    /// </summary>
    /// <param name="value">The text.</param>
    /// <returns>The builder.</returns>
    IBuilderForLocale WithText(string value);
}

/// <summary>
///     Represents a builder to set the Locale.
/// </summary>
public interface IBuilderForLocale
{
    /// <summary>
    ///     Sets the Locale.
    /// </summary>
    /// <param name="value">The Locale.</param>
    /// <returns>The builder.</returns>
    IBuilderForChannel WithLocale(Locale value);
}

/// <summary>
///     Represents a builder to set the Channel.
/// </summary>
public interface IBuilderForChannel
{
    /// <summary>
    ///     Sets the Channel.
    /// </summary>
    /// <param name="value">The channel.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CreateTemplateFragmentRequest> WithChannel(VerificationChannel value);
}