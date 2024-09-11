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
///     Represents a builder to set the Id.
/// </summary>
public interface IBuilderForId
{
    /// <summary>
    ///     Sets the Id.
    /// </summary>
    /// <param name="value">The template id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithId(Guid value);
}

/// <summary>
///     Represents a builder to set optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateTemplateRequest>
{
    /// <summary>
    ///     Sets the Name.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the template as default.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional SetAsDefaultTemplate();

    /// <summary>
    ///     Sets the template as non-default.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional SetAsNonDefaultTemplate();
}