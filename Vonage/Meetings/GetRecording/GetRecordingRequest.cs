using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRecording;

/// <summary>
///  Represents a request to retrieve a recording details.
/// </summary>
public readonly struct GetRecordingRequest : IVonageRequest
{
    private GetRecordingRequest(string recordingId) => this.RecordingId = recordingId;

    /// <summary>
    ///     The recording identifier.
    /// </summary>
    public string RecordingId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/recordings/{this.RecordingId}";

    /// <summary>
    ///     Parses the input into a GetRecordingRequest.
    /// </summary>
    /// <param name="recordingId">The recording identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetRecordingRequest> Parse(string recordingId) =>
        Result<GetRecordingRequest>
            .FromSuccess(new GetRecordingRequest(recordingId))
            .Bind(VerifyRecordingId);

    private static Result<GetRecordingRequest> VerifyRecordingId(GetRecordingRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RecordingId, nameof(RecordingId));
}