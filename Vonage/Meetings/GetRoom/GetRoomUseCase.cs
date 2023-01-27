using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetRoom;

internal class GetRoomUseCase
{
    private readonly VonageHttpClient httpClient;

    internal GetRoomUseCase(VonageHttpClient client) => this.httpClient = client;

    internal Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request) =>
        this.httpClient.SendWithResponseAsync<Room, GetRoomRequest>(request);
}