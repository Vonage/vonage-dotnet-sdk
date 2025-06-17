#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.ExperienceComposer.GetSessions;

/// <summary>
///     Represents a request to retrieve sessions.
/// </summary>
[Builder]
public readonly partial struct GetSessionsRequest : IVonageRequest, IHasApplicationId
{
    private const int MinimumOffset = 0;
    private const int MinimumCount = 50;
    private const int MaximumCount = 1000;

    /// <summary>
    ///     Set a count query parameter to limit the number of experience composers to be returned. The default number of
    ///     archives returned is 50 (or fewer, if there are fewer than 50 archives). The default is 50 and the maximum is 1000
    /// </summary>
    [OptionalWithDefault("int", "50")]
    public int Count { get; internal init; }

    /// <summary>
    ///     Set an offset query parameters to specify the index offset of the first experience composer. 0 is offset of the
    ///     most recently started archive (excluding deleted archive). 1 is the offset of the experience composer that started
    ///     prior to the most recent composer. The default value is 0.
    /// </summary>
    [OptionalWithDefault("int", "0")]
    public int Offset { get; internal init; }

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
        $"/v2/project/{this.ApplicationId}/render?offset={this.Offset}&count={this.Count}";

    [ValidationRule]
    internal static Result<GetSessionsRequest> VerifyApplicationId(GetSessionsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetSessionsRequest> VerifyOffset(GetSessionsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Offset, MinimumOffset, nameof(request.Offset));

    [ValidationRule]
    internal static Result<GetSessionsRequest> VerifyCountMinimum(GetSessionsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Count, MinimumCount, nameof(request.Count));

    [ValidationRule]
    internal static Result<GetSessionsRequest> VerifyCountMaximum(GetSessionsRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaximumCount, nameof(request.Count));
}