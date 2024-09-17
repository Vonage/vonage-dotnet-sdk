#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragments;

internal struct GetTemplateFragmentsRequestBuilder : IBuilderForOptional, IBuilderForTemplateId
{
    private Maybe<int> page;
    private Maybe<int> pageSize;
    private Guid templateId;

    /// <inheritdoc />
    public Result<GetTemplateFragmentsRequest> Create() => Result<GetTemplateFragmentsRequest>.FromSuccess(
            new GetTemplateFragmentsRequest
            {
                TemplateId = this.templateId,
                PageSize = this.pageSize,
                Page = this.page,
            })
        .Map(InputEvaluation<GetTemplateFragmentsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyTemplateId));

    public IBuilderForOptional WithPageSize(int value) => this with {pageSize = value};
    public IBuilderForOptional WithPage(int value) => this with {page = value};
    public IBuilderForOptional WithTemplateId(Guid value) => this with {templateId = value};

    private static Result<GetTemplateFragmentsRequest> VerifyTemplateId(
        GetTemplateFragmentsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));
}

/// <summary>
///     Represents a builder to set the TemplateId.
/// </summary>
public interface IBuilderForTemplateId
{
    /// <summary>
    ///     Sets the TemplateId.
    /// </summary>
    /// <param name="value">The template id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithTemplateId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetTemplateFragmentsRequest>
{
    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPageSize(int value);

    /// <summary>
    ///     Sets the page on the builder.
    /// </summary>
    /// <param name="value">The page.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPage(int value);
}