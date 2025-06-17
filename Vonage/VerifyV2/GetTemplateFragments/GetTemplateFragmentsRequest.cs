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

/// <inheritdoc />
[Builder]
public readonly partial struct GetTemplateFragmentsRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    [Mandatory(0)]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    [Optional]
    public Maybe<int> PageSize { get; internal init; }

    /// <summary>
    ///     The page.
    /// </summary>
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