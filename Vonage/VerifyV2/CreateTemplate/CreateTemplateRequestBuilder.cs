#region
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.CreateTemplate;

internal struct CreateTemplateRequestBuilder : IVonageRequestBuilder<CreateTemplateRequest>, IBuildForName
{
    private string name;

    public IVonageRequestBuilder<CreateTemplateRequest> WithName(string value) =>
        new CreateTemplateRequestBuilder {name = value};

    public Result<CreateTemplateRequest> Create() => Result<CreateTemplateRequest>.FromSuccess(new CreateTemplateRequest
        {
            Name = this.name,
        })
        .Map(InputEvaluation<CreateTemplateRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyNameNotEmpty));

    private static Result<CreateTemplateRequest> VerifyNameNotEmpty(
        CreateTemplateRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Name, nameof(request.Name));
}

/// <summary>
///     Represents a builder to set the Name.
/// </summary>
public interface IBuildForName
{
    /// <summary>
    ///     Sets the Name.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CreateTemplateRequest> WithName(string value);
}