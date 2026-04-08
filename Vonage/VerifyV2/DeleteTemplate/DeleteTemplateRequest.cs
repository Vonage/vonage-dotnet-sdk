#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.DeleteTemplate;

/// <summary>
///     Represents a request to delete a custom verification template. A template can only be deleted if it has no template fragments attached.
/// </summary>
public readonly struct DeleteTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     The unique identifier (UUID) of the template to delete.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <summary>
    ///     Creates a new request to delete a template.
    /// </summary>
    /// <param name="templateId">The UUID of the template to delete.</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if the ID is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteTemplateRequest.Parse(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"));
    /// var response = await client.DeleteTemplateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<DeleteTemplateRequest> Parse(Guid templateId) =>
        Result<DeleteTemplateRequest>
            .FromSuccess(new DeleteTemplateRequest {TemplateId = templateId})
            .Map(InputEvaluation<DeleteTemplateRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v2/verify/templates/{this.TemplateId}")
        .Build();

    private static Result<DeleteTemplateRequest> VerifyRequestId(DeleteTemplateRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));
}