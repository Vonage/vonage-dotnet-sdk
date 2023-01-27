using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.GetStream;

internal class GetStreamUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal GetStreamUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<GetStreamResponse, GetStreamRequest>(request);
}