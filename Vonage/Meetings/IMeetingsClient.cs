using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.CreateTheme;
using Vonage.Meetings.DeleteRecording;
using Vonage.Meetings.DeleteTheme;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Meetings.GetRecording;
using Vonage.Meetings.GetRecordings;
using Vonage.Meetings.GetRoom;
using Vonage.Meetings.GetRooms;
using Vonage.Meetings.GetRoomsByTheme;
using Vonage.Meetings.GetTheme;
using Vonage.Meetings.UpdateApplication;
using Vonage.Meetings.UpdateRoom;
using Vonage.Meetings.UpdateTheme;
using Vonage.Meetings.UpdateThemeLogo;

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
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Room>> CreateRoomAsync(Result<CreateRoomRequest> request);

    /// <summary>
    ///     Creates a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Theme>> CreateThemeAsync(Result<CreateThemeRequest> request);

    /// <summary>
    ///     Deletes a recording.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Unit>> DeleteRecordingAsync(Result<DeleteRecordingRequest> request);

    /// <summary>
    ///     Deletes a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Unit>> DeleteThemeAsync(Result<DeleteThemeRequest> request);

    /// <summary>
    ///     Retrieves numbers that can be used to dial into a meeting.
    /// </summary>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync();

    /// <summary>
    ///     Retrieves a recording details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Recording>> GetRecordingAsync(Result<GetRecordingRequest> request);

    /// <summary>
    ///     Retrieves recordings from a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetRecordingsResponse>> GetRecordingsAsync(Result<GetRecordingsRequest> request);

    /// <summary>
    ///     Retrieves a room details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request);

    /// <summary>
    ///     Retrieves all rooms.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetRoomsResponse>> GetRoomsAsync(GetRoomsRequest request);

    /// <summary>
    ///     Retrieves rooms by theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetRoomsByThemeResponse>> GetRoomsByThemeAsync(Result<GetRoomsByThemeRequest> request);

    /// <summary>
    ///     Retrieves a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Theme>> GetThemeAsync(Result<GetThemeRequest> request);

    /// <summary>
    ///     Retrieves all themes.
    /// </summary>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Theme[]>> GetThemesAsync();

    /// <summary>
    ///     Updates an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<UpdateApplicationResponse>> UpdateApplicationAsync(Result<UpdateApplicationRequest> request);

    /// <summary>
    ///     Updates a room.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Room>> UpdateRoomAsync(Result<UpdateRoomRequest> request);

    /// <summary>
    ///     Updates a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Theme>> UpdateThemeAsync(Result<UpdateThemeRequest> request);

    /// <summary>
    ///     Updates a logo image and associates it with a theme.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Unit>> UpdateThemeLogoAsync(Result<UpdateThemeLogoRequest> request);
}