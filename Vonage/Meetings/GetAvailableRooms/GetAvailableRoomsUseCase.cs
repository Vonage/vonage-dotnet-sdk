using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetAvailableRooms;

internal class GetAvailableRoomsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetAvailableRoomsUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request) =>
        this.httpClient.SendWithResponseAsync<GetAvailableRoomsResponse, GetAvailableRoomsRequest>(request,
            this.generateToken());
}