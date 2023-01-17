using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.GetAvailableRooms;

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
}