#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.Archives.GetArchives;

/// <summary>
///     Represents a request to retrieve archives.
/// </summary>
[Builder]
public readonly partial struct GetArchivesRequest : IVonageRequest, IHasApplicationId
{
    private const int MaxCount = 1000;

    /// <summary>
    ///     Sets the maximum number of archives to return. The default is 50 and the maximum is 1000.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCount(100)
    /// ]]></code>
    /// </example>
    [OptionalWithDefault("int", "50")]
    public int Count { get; internal init; }

    /// <summary>
    ///     Sets the index offset of the first archive to return. 0 (the default) is the most recently started archive.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithOffset(10)
    /// ]]></code>
    /// </example>
    [OptionalWithDefault("int", "0")]
    public int Offset { get; internal init; }

    /// <summary>
    ///     Sets a session ID filter to list archives for a specific session. Useful when listing multiple archives for an
    ///     automatically archived session.
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
            .Initialize(HttpMethod.Get, this.BuildUri())
            .Build();

    private string BuildUri()
    {
        var path = $"/v2/project/{this.ApplicationId}/archive?offset={this.Offset}&count={this.Count}";
        var session = this.SessionId
            .Bind(value => string.IsNullOrWhiteSpace(value) ? Maybe<string>.None : value)
            .Map(value => $"&sessionId={value}")
            .IfNone(string.Empty);
        return string.Concat(path, session);
    }

    [ValidationRule]
    internal static Result<GetArchivesRequest> VerifyApplicationId(GetArchivesRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<GetArchivesRequest> VerifyCount(GetArchivesRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Count, nameof(request.Count))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaxCount, nameof(request.Count)));

    [ValidationRule]
    internal static Result<GetArchivesRequest> VerifyOffset(GetArchivesRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Offset, nameof(request.Offset));
}