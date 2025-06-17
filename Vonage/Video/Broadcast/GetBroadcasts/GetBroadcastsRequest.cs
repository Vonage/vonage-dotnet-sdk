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
    ///     The count query parameter to limit the number of archives to be returned. The default number of archives returned
    ///     is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call will return is 1000.
    /// </summary>
    [OptionalWithDefault("int", "50")]
    public int Count { get; internal init; }

    /// <summary>
    ///     The offset query parameters to specify the index offset of the first archive. 0 is offset of the most recently
    ///     started archive (excluding deleted archive). 1 is the offset of the archive that started prior to the most recent
    ///     archive. The default value is 0.
    /// </summary>
    [OptionalWithDefault("int", "0")]
    public int Offset { get; internal init; }

    /// <summary>
    ///     The sessionId query parameter to list archives for a specific session ID. (This is useful when listing multiple
    ///     archives for an automatically archived session.)
    /// </summary>
    [Optional]
    public Maybe<string> SessionId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        UriHelpers.BuildUri($"/v2/project/{this.ApplicationId}/broadcast", this.GetQueryStringParameters());

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