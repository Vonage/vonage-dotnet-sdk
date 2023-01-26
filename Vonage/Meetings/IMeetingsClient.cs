using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Meetings.GetRecording;
using Vonage.Meetings.GetRecordings;
using Vonage.Meetings.GetRoom;
using Vonage.Meetings.GetTheme;

namespace Vonage.Meetings;

/// <summary>
///     Exposes Meetings features.
/// </summary>
public interface IMeetingsClient
{
    /// <summary>
    ///     Creates a room.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The room.</returns>
    Task<Result<Room>> CreateRoomAsync(Result<CreateRoomRequest> request);

    /// <summary>
    ///     Retrieves all available rooms.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The list of available rooms.</returns>
    Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request);

    /// <summary>
    ///     Retrieves numbers that can be used to dial into a meeting.
    /// </summary>
    /// <returns>The numbers.</returns>
    Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync();

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

    /// <summary>
    ///     Retrieves a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The theme.</returns>
    Task<Result<Theme>> GetThemeAsync(Result<GetThemeRequest> request);

    /// <summary>
    ///     Retrieves all themes.
    /// </summary>
    /// <returns>The themes.</returns>
    Task<Result<Theme[]>> GetThemesAsync();
}