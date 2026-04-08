#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragments;

/// <summary>
///     Represents a request to retrieve a paginated list of template fragments for a specific template.
/// </summary>
[Builder]
public readonly partial struct GetTemplateFragmentsRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the template to retrieve fragments from.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTemplateId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Sets the number of fragments to return per page.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPageSize(10)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<int> PageSize { get; internal init; }

    /// <summary>
    ///     Sets the page number to retrieve (1-based index).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPage(2)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<int> Page { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri($"/v2/verify/templates/{this.TemplateId}/template_fragments",
            this.GetQueryStringParameters()))
        .Build();

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.PageSize.IfSome(some => parameters.Add("page_size", some.ToString()));
        this.Page.IfSome(some => parameters.Add("page", some.ToString()));
        return parameters;
    }

    [ValidationRule]
    internal static Result<GetTemplateFragmentsRequest> VerifyTemplateId(
        GetTemplateFragmentsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));
}