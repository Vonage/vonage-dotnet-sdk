using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.GetAvailableRooms;

namespace Vonage.Meetings;

/// <summary>
///     Exposes Meetings features.
/// </summary>
public interface IMeetingsClient
{
    Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(Result<GetAvailableRoomsRequest> request);
}