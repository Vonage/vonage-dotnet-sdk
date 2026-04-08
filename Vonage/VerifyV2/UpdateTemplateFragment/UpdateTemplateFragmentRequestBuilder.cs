#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.UpdateTemplateFragment;

internal struct UpdateTemplateFragmentRequestBuilder : IBuilderForId, IBuilderForFragmentId, IBuilderForText,
    IVonageRequestBuilder<UpdateTemplateFragmentRequest>
{
    private Guid templateFragmentId;
    private Guid templateId;
    private string text;

    public IBuilderForText WithFragmentId(Guid value) => this with {templateFragmentId = value};
    public IBuilderForFragmentId WithId(Guid value) => this with {templateId = value};

    public IVonageRequestBuilder<UpdateTemplateFragmentRequest> WithText(string value) => this with {text = value};

    public Result<UpdateTemplateFragmentRequest> Create() => Result<UpdateTemplateFragmentRequest>.FromSuccess(
            new UpdateTemplateFragmentRequest
            {
                TemplateId = this.templateId,
                TemplateFragmentId = this.templateFragmentId,
                Text = this.text,
            })
        .Map(InputEvaluation<UpdateTemplateFragmentRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyTemplateIdNotEmpty,
            VerifyTemplateFragmentIdNotEmpty,
            VerifyTextNotEmpty));

    private static Result<UpdateTemplateFragmentRequest> VerifyTemplateIdNotEmpty(
        UpdateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    private static Result<UpdateTemplateFragmentRequest> VerifyTemplateFragmentIdNotEmpty(
        UpdateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateFragmentId, nameof(request.TemplateFragmentId));

    private static Result<UpdateTemplateFragmentRequest> VerifyTextNotEmpty(
        UpdateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Text, nameof(request.Text));
}

/// <summary>
///     Builder interface for setting the template ID on an update fragment request. This is the first mandatory step.
/// </summary>
public interface IBuilderForId
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the parent template.
    /// </summary>
    /// <param name="value">The template UUID.</param>
    /// <returns>The builder for setting the fragment ID.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    IBuilderForFragmentId WithId(Guid value);
}

/// <summary>
///     Builder interface for setting the template fragment ID on an update request.
/// </summary>
public interface IBuilderForFragmentId
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the template fragment to update.
    /// </summary>
    /// <param name="value">The template fragment UUID.</param>
    /// <returns>The builder for setting the text content.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithFragmentId(Guid.Parse("c70f446e-997a-4313-a081-60a02a31dc19"))
    /// ]]></code>
    /// </example>
    IBuilderForText WithFragmentId(Guid value);
}

/// <summary>
///     Builder interface for setting the message text on an update fragment request.
/// </summary>
public interface IBuilderForText
{
    /// <summary>
    ///     Sets the new message text content with optional placeholders: ${code}, ${brand}, ${time-limit}, and ${time-limit-unit}.
    /// </summary>
    /// <param name="value">The message text content.</param>
    /// <returns>The builder for creating the request.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithText("Your ${brand} verification code is ${code}. Valid for ${time-limit} ${time-limit-unit}.")
    /// ]]></code>
    /// </example>
    IVonageRequestBuilder<UpdateTemplateFragmentRequest> WithText(string value);
}