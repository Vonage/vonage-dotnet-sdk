using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.DeleteRecording;

/// <summary>
///     Represents a request to delete a recording.
/// </summary>
public class DeleteRecordingRequest : IVonageRequest
{
    private DeleteRecordingRequest(Guid recordingId) => this.RecordingId = recordingId;

    /// <summary>
    ///     The recording id.
    /// </summary>
    public Guid RecordingId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/meetings/recordings/{this.RecordingId}";

    /// <summary>
    ///     Parses the input into a DeleteRecordingRequest.
    /// </summary>
    /// <param name="recordingId">The recording id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteRecordingRequest> Parse(Guid recordingId) =>
        Result<DeleteRecordingRequest>
            .FromSuccess(new DeleteRecordingRequest(recordingId))
            .Bind(VerifyRecordingId);

    private static Result<DeleteRecordingRequest> VerifyRecordingId(DeleteRecordingRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RecordingId, nameof(RecordingId));
}