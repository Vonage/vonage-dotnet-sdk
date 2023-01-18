using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetRoom;

namespace Vonage.Meetings;

/// <inheritdoc />
public class MeetingsClient : IMeetingsClient
{
    private readonly GetAvailableRoomsUseCase getAvailableRoomsUseCase;
    private readonly GetRoomUseCase getRoomUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public MeetingsClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        var vonageClient = new VonageHttpClient(httpClient, JsonSerializerBuilder.Build());
        this.getAvailableRoomsUseCase = new GetAvailableRoomsUseCase(vonageClient, tokenGeneration);
        this.getRoomUseCase = new GetRoomUseCase(vonageClient, tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request) =>
        this.getAvailableRoomsUseCase.GetAvailableRoomsAsync(request);

    /// <inheritdoc />
    public Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request) =>
        this.getRoomUseCase.GetRoomAsync(request);
}