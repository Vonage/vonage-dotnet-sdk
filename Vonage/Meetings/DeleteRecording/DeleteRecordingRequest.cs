using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.DeleteRecording;

/// <summary>
///     Represents a request to delete a recording.
/// </summary>
public class Request : IVonageRequest
{
    private Request(Guid recordingId) => this.RecordingId = recordingId;

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
    public string GetEndpointPath() => $"/v1/meetings/recordings/{this.RecordingId}";

    /// <summary>
    ///     Parses the input into a DeleteRecordingRequest.
    /// </summary>
    /// <param name="recordingId">The recording id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<Request> Parse(Guid recordingId) =>
        Result<Request>
            .FromSuccess(new Request(recordingId))
            .Map(InputEvaluation<Request>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRecordingId));

    private static Result<Request> VerifyRecordingId(Request request) =>
        InputValidation.VerifyNotEmpty(request, request.RecordingId, nameof(RecordingId));
}