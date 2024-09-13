#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.DeleteTemplateFragment;

internal struct DeleteTemplateFragmentRequestBuilder : IBuilderForTemplateId, IBuilderForTemplateFragmentId,
    IVonageRequestBuilder<DeleteTemplateFragmentRequest>
{
    private Guid templateFragmentId;
    private Guid templateId;

    public IVonageRequestBuilder<DeleteTemplateFragmentRequest> WithTemplateFragmentId(Guid value) =>
        this with {templateFragmentId = value};

    public IBuilderForTemplateFragmentId WithTemplateId(Guid value) => this with {templateId = value};

    public Result<DeleteTemplateFragmentRequest> Create() => Result<DeleteTemplateFragmentRequest>.FromSuccess(
            new DeleteTemplateFragmentRequest
            {
                TemplateId = this.templateId,
                TemplateFragmentId = this.templateFragmentId,
            })
        .Map(InputEvaluation<DeleteTemplateFragmentRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyTemplateId, VerifyTemplateFragmentId));

    private static Result<DeleteTemplateFragmentRequest> VerifyTemplateId(
        DeleteTemplateFragmentRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    private static Result<DeleteTemplateFragmentRequest> VerifyTemplateFragmentId(
        DeleteTemplateFragmentRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.TemplateFragmentId, nameof(request.TemplateFragmentId));
}

/// <summary>
///     Represents a builder to set the TemplateId.
/// </summary>
public interface IBuilderForTemplateId
{
    /// <summary>
    ///     Sets the template id.
    /// </summary>
    /// <param name="value">The template id.</param>
    /// <returns>The builder.</returns>
    IBuilderForTemplateFragmentId WithTemplateId(Guid value);
}

/// <summary>
///     Represents a builder to set the TemplateFragmentId.
/// </summary>
public interface IBuilderForTemplateFragmentId
{
    /// <summary>
    ///     Sets the template fragment id.
    /// </summary>
    /// <param name="value">The template fragment id.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<DeleteTemplateFragmentRequest> WithTemplateFragmentId(Guid value);
}