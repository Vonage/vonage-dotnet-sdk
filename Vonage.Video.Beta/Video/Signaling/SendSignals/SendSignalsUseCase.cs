using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

/// <inheritdoc />
public class SendSignalsUseCase : ISendSignalsUseCase
{
    private readonly HttpClient client;
    private readonly Func<string> generateToken;
    private readonly JsonSerializer jsonSerializer;
    private readonly CustomClient customClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public SendSignalsUseCase(HttpClient httpClient, Func<string> generateToken)
    {
        this.client = httpClient;
        this.generateToken = generateToken;
        this.jsonSerializer = new JsonSerializer();
        this.customClient = new CustomClient(httpClient);
    }

    /// <inheritdoc />
    public Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request) =>
        this.customClient.SendAsync(request, this.generateToken());
}