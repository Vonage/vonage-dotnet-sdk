#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Broadcast.GetBroadcasts;

/// <summary>
///     Represents a request to retrieve broadcasts.
/// </summary>
[Builder]
public readonly partial struct GetBroadcastsRequest : IVonageRequest, IHasApplicationId
{
    private const int MaxCount = 1000;

    /// <summary>
    ///     Sets the maximum number of broadcasts to return. The default is 50 and the maximum is 1000.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCount(100)
    /// ]]></code>
    /// </example>
    [OptionalWithDefault("int", "50")]
    public int Count { get; internal init; }

    /// <summary>
    ///     Sets the index offset of the first broadcast to return. 0 (the default) is the most recently started broadcast.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithOffset(10)
    /// ]]></code>
    /// </example>
    [OptionalWithDefault("int", "0")]
    public int Offset { get; internal init; }

    /// <summary>
    ///     Sets a session ID filter to list broadcasts for a specific session.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> SessionId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get,
                UriHelpers.BuildUri($"/v2/project/{this.ApplicationId}/broadcast", this.GetQueryStringParameters()))
            .Build();

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

    [ValidationRule]
    internal static Result<GetBroadcastsRequest> VerifyApplicationId(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetBroadcastsRequest> VerifyCount(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Count, nameof(request.Count))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaxCount, nameof(request.Count)));

    [ValidationRule]
    internal static Result<GetBroadcastsRequest> VerifyOffset(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Offset, nameof(request.Offset));
}