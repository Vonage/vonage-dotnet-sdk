#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplate;

/// <summary>
///     Represents a request to retrieve a single custom verification template by its ID.
/// </summary>
public readonly struct GetTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     The unique identifier (UUID) of the template to retrieve.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <summary>
    ///     Creates a new request to retrieve a template.
    /// </summary>
    /// <param name="templateId">The UUID of the template to retrieve.</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if the ID is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplateRequest.Parse(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"));
    /// var response = await client.GetTemplateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<GetTemplateRequest> Parse(Guid templateId) =>
        Result<GetTemplateRequest>
            .FromSuccess(new GetTemplateRequest {TemplateId = templateId})
            .Map(InputEvaluation<GetTemplateRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v2/verify/templates/{this.TemplateId}")
        .Build();

    private static Result<GetTemplateRequest> VerifyRequestId(GetTemplateRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));
}