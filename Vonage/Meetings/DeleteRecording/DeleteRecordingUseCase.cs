using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.DeleteRecording;

internal class DeleteRecordingUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal DeleteRecordingUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal Task<Result<Unit>> DeleteRecordingAsync(Result<DeleteRecordingRequest> request) =>
        this.httpClient.SendAsync(request, this.generateToken());
}