using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Broadcast.GetBroadcasts;

/// <summary>
///     Represents a request to retrieve broadcasts.
/// </summary>
public readonly struct GetBroadcastsRequest : IVonageRequest, IHasApplicationId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The count query parameter to limit the number of archives to be returned. The default number of archives returned
    ///     is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call will return is 1000.
    /// </summary>
    public int Count { get; internal init; }

    /// <summary>
    ///     The offset query parameters to specify the index offset of the first archive. 0 is offset of the most recently
    ///     started archive (excluding deleted archive). 1 is the offset of the archive that started prior to the most recent
    ///     archive. The default value is 0.
    /// </summary>
    public int Offset { get; internal init; }

    /// <summary>
    ///     The sessionId query parameter to list archives for a specific session ID. (This is useful when listing multiple
    ///     archives for an automatically archived session.)
    /// </summary>
    public Maybe<string> SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        QueryHelpers.AddQueryString($"/v2/project/{this.ApplicationId}/broadcast", this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"offset", this.Offset.ToString()},
            {"count", this.Count.ToString()},
        };
        this.SessionId.Bind(VerifyIfNotEmpty).IfSome(value => parameters.Add("sessionId", value));
        return parameters;
    }

    private static Maybe<string> VerifyIfNotEmpty(string value) =>
        string.IsNullOrWhiteSpace(value) ? Maybe<string>.None : value;
}