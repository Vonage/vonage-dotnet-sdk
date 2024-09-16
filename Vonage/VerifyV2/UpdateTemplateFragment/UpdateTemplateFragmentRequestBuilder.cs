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
///     Represents a builder to set the Id.
/// </summary>
public interface IBuilderForId
{
    /// <summary>
    ///     Sets the Id.
    /// </summary>
    /// <param name="value">The template id.</param>
    /// <returns>The builder.</returns>
    IBuilderForFragmentId WithId(Guid value);
}

/// <summary>
///     Represents a builder to set the template fragment Id.
/// </summary>
public interface IBuilderForFragmentId
{
    /// <summary>
    ///     Sets the Id.
    /// </summary>
    /// <param name="value">The template fragment id.</param>
    /// <returns>The builder.</returns>
    IBuilderForText WithFragmentId(Guid value);
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
    IVonageRequestBuilder<UpdateTemplateFragmentRequest> WithText(string value);
}