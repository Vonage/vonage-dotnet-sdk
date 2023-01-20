using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Meetings.GetRecording;
using Vonage.Meetings.GetRecordings;
using Vonage.Meetings.GetRoom;

namespace Vonage.Meetings;

/// <summary>
///     Exposes Meetings features.
/// </summary>
public interface IMeetingsClient
{
    /// <summary>
    ///     Retrieves all available rooms.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The list of available rooms.</returns>
    Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request);

    /// <summary>
    ///     Retrieves numbers that can be used to dial into a meeting.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The numbers.</returns>
    Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync(GetDialNumbersRequest request);

    /// <summary>
    ///     Retrieves a recording details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The recording.</returns>
    Task<Result<Recording>> GetRecordingAsync(Result<GetRecordingRequest> request);

    /// <summary>
    ///     Retrieves recordings from a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The recordings from the session.</returns>
    Task<Result<GetRecordingsResponse>> GetRecordingsAsync(Result<GetRecordingsRequest> request);

    /// <summary>
    ///     Retrieves a room details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The room.</returns>
    Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request);
}