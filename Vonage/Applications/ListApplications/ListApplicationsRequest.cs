using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Applications.ListApplications;

/// <summary>
///     Represents a request to list applications with optional pagination.
/// </summary>
[Builder]
public readonly partial struct ListApplicationsRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the maximum number of applications returned per page. Defaults to 10 when omitted.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithPageSize(20)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<int> PageSize { get; internal init; }

    /// <summary>
    ///     Sets the page number to retrieve. Starts at 1.
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
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri("/v2/applications", this.GetQueryParameters()))
        .Build();

    private Dictionary<string, string> GetQueryParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.PageSize.IfSome(pageSize => parameters.Add("page_size", pageSize.ToString()));
        this.Page.IfSome(page => parameters.Add("page", page.ToString()));
        return parameters;
    }
}
