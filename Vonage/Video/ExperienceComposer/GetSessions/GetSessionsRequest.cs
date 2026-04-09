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
    ///     Sets the maximum number of Experience Composers to return. The default is 50 and the maximum is 1000.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCount(100)
    /// ]]></code>
    /// </example>
    [OptionalWithDefault("int", "50")]
    public int Count { get; internal init; }

    /// <summary>
    ///     Sets the index offset of the first Experience Composer to return. 0 (the default) is the most recently started
    ///     Experience Composer.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithOffset(10)
    /// ]]></code>
    /// </example>
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