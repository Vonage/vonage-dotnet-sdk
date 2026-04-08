#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.UpdateTemplate;

internal struct UpdateTemplateRequestBuilder : IBuilderForId, IBuilderForOptional
{
    private Maybe<bool> isDefault;
    private Maybe<string> name;
    private Guid templateId;

    public IBuilderForOptional WithId(Guid value) => this with {templateId = value};

    public IBuilderForOptional SetAsDefaultTemplate() => this with {isDefault = true};

    public IBuilderForOptional SetAsNonDefaultTemplate() => this with {isDefault = false};

    public Result<UpdateTemplateRequest> Create() => Result<UpdateTemplateRequest>.FromSuccess(new UpdateTemplateRequest
        {
            Name = this.name,
            IsDefault = this.isDefault,
            TemplateId = this.templateId,
        })
        .Map(InputEvaluation<UpdateTemplateRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyIdNotEmpty));

    public IBuilderForOptional WithName(string value) => this with {name = value};

    private static Result<UpdateTemplateRequest> VerifyIdNotEmpty(
        UpdateTemplateRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));
}

/// <summary>
///     Builder interface for setting the template ID on an update request. This is the first mandatory step.
/// </summary>
public interface IBuilderForId
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the template to update.
    /// </summary>
    /// <param name="value">The template UUID.</param>
    /// <returns>The builder for setting optional properties or creating the request.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithId(Guid value);
}

/// <summary>
///     Builder interface for setting optional properties on an <see cref="UpdateTemplateRequest"/>.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateTemplateRequest>
{
    /// <summary>
    ///     Sets the new reference name for the template. Must be unique within the account.
    /// </summary>
    /// <param name="value">The new template name (1-64 characters, alphanumeric with underscores and hyphens).</param>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("new-template-name")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Marks this template as the default template, used when no template_id is specified in verification requests.
    /// </summary>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .SetAsDefaultTemplate()
    /// ]]></code>
    /// </example>
    IBuilderForOptional SetAsDefaultTemplate();

    /// <summary>
    ///     Marks this template as non-default, so it will not be used unless explicitly specified.
    /// </summary>
    /// <returns>The builder for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .SetAsNonDefaultTemplate()
    /// ]]></code>
    /// </example>
    IBuilderForOptional SetAsNonDefaultTemplate();
}