using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Archives.GetArchives;

/// <inheritdoc />
public readonly struct GetArchivesRequest : IVideoRequest
{
    /// <summary>
    ///     The default value is 0.
    /// </summary>
    public const int DefaultOffset = 0;

    /// <summary>
    ///     The default number of archives returned is 50.
    /// </summary>
    public const int DefaultCount = 50;

    /// <summary>
    ///     The maximum number of archives the call will return is 1000.
    /// </summary>
    public const int MaxCount = 1000;

    private GetArchivesRequest(string applicationId, int offset, int count, string sessionId)
    {
        this.ApplicationId = applicationId;
        this.Offset = offset;
        this.Count = count;
        this.SessionId = sessionId;
    }

    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     Set an offset query parameters to specify the index offset of the first archive. 0 is offset of the most recently
    ///     started archive (excluding deleted archive). 1 is the offset of the archive that started prior to the most recent
    ///     archive. The default value is 0.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    ///     Set a count query parameter to limit the number of archives to be returned. The default number of archives returned
    ///     is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call will return is 1000.
    /// </summary>
    public int Count { get; }

    /// <summary>
    ///     Set a sessionId query parameter to list archives for a specific session ID. (This is useful when listing multiple
    ///     archives for an automatically archived session.)
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     Parses the input into a GetArchivesRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage Application UUID.</param>
    /// <param name="offset">
    ///     Set an offset query parameters to specify the index offset of the first archive. 0 is offset of
    ///     the most recently started archive (excluding deleted archive). 1 is the offset of the archive that started prior to
    ///     the most recent archive. The default value is 0.
    /// </param>
    /// <param name="count">
    ///     Set a count query parameter to limit the number of archives to be returned. The default number of
    ///     archives returned is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call
    ///     will return is 1000.
    /// </param>
    /// <param name="sessionId">
    ///     Set a sessionId query parameter to list archives for a specific session ID. (This is useful
    ///     when listing multiple archives for an automatically archived session.)
    /// </param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetArchivesRequest> Parse(string applicationId, int offset = DefaultOffset,
        int count = DefaultCount,
        string sessionId = null) =>
        Result<GetArchivesRequest>
            .FromSuccess(new GetArchivesRequest(applicationId, offset, count, sessionId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyOffset)
            .Bind(VerifyCount);

    /// <inheritdoc />
    public string GetEndpointPath()
    {
        var path = $"/v2/project/{this.ApplicationId}/archive?offset={this.Offset}&count={this.Count}";
        return string.IsNullOrWhiteSpace(this.SessionId) switch
        {
            false => string.Concat(path, $"&sessionId={this.SessionId}"),
            _ => path,
        };
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<GetArchivesRequest> VerifyApplicationId(GetArchivesRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetArchivesRequest> VerifyOffset(GetArchivesRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Offset, nameof(Offset));

    private static Result<GetArchivesRequest> VerifyCount(GetArchivesRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Count, nameof(Count))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaxCount, nameof(Count)));
}