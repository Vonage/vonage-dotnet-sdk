using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetRecordings;

internal class GetRecordingsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetRecordingsUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal Task<Result<GetRecordingsResponse>> GetRecordingsAsync(Result<GetRecordingsRequest> request) =>
        this.httpClient.SendWithResponseAsync<GetRecordingsResponse, GetRecordingsRequest>(request,
            this.generateToken());
}