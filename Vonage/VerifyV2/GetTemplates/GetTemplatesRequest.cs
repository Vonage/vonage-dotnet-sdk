#region
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

/// <summary>
///     Represents a request to retrieve a paginated list of custom verification templates.
/// </summary>
[Builder]
public readonly partial struct GetTemplatesRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the number of templates to return per page.
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
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri("/v2/verify/templates", this.GetQueryStringParameters()))
        .Build();

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.PageSize.IfSome(some => parameters.Add("page_size", some.ToString()));
        this.Page.IfSome(some => parameters.Add("page", some.ToString()));
        return parameters;
    }
}