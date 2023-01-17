using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Meetings.GetAvailableRooms;

namespace Vonage.Meetings;

public class MeetingsClient : IMeetingsClient
{
    public Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(Result<GetAvailableRoomsRequest> request) =>
        throw new NotImplementedException();
}