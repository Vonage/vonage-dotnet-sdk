#region
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

/// <inheritdoc />
[Builder]
public readonly partial struct GetTemplatesRequest : IVonageRequest
{
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
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri("/v2/verify/templates", this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.PageSize.IfSome(some => parameters.Add("page_size", some.ToString()));
        this.Page.IfSome(some => parameters.Add("page", some.ToString()));
        return parameters;
    }
}