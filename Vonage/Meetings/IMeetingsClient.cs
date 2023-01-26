using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetRoom;

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
    ///     Retrieves a room details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The room.</returns>
    Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request);
}