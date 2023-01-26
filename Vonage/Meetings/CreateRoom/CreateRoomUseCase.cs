using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.CreateRoom;

internal class CreateRoomUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal CreateRoomUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal Task<Result<Room>> CreateRoomAsync(Result<CreateRoomRequest> request) =>
        this.httpClient.SendWithResponseAsync<Room, CreateRoomRequest>(request,
            this.generateToken());
}