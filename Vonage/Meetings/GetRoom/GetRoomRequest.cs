using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRoom;

/// <summary>
///     Represents a request to retrieve a room details.
/// </summary>
public readonly struct GetRoomRequest : IVonageRequest
{
    private GetRoomRequest(string roomId) => this.RoomId = roomId;

    /// <summary>
    ///     The room identifier.
    /// </summary>
    public string RoomId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/rooms/{this.RoomId}";

    /// <summary>
    ///     Parses the input into a GetRoomRequest.
    /// </summary>
    /// <param name="roomId">The room identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetRoomRequest> Parse(string roomId) =>
        Result<GetRoomRequest>
            .FromSuccess(new GetRoomRequest(roomId))
            .Bind(VerifyRoomId);

    private static Result<GetRoomRequest> VerifyRoomId(GetRoomRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RoomId, nameof(RoomId));
}