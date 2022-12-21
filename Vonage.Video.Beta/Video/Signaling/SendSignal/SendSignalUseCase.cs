using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Video.Sessions;

namespace Vonage.Video.Beta.Video.Signaling.SendSignal;

/// <inheritdoc />
public class SendSignalUseCase : ISendSignalUseCase
{
    private readonly HttpClient client;
    private readonly Func<string> generateToken;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public SendSignalUseCase(HttpClient httpClient, Func<string> generateToken)
    {
        this.client = httpClient;
        this.generateToken = generateToken;
        this.jsonSerializer = new JsonSerializer();
    }

    /// <inheritdoc />
    public async Task<Result<Unit>> SendSignalAsync(SendSignalRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.generateToken());
        var response = await this.client.SendAsync(httpRequest);
        return !response.IsSuccessStatusCode ? await this.CreateFailureResponse(response) : CreateSuccessResponse();
    }

    private async Task<Result<Unit>> CreateFailureResponse(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(responseContent)
            ? CreateFailureResponseWithoutContent(response)
            : this.CreateFailureResponseWithContent(responseContent, response);
    }

    private Result<Unit> CreateFailureResponseWithContent(string responseContent, HttpResponseMessage response) =>
        this.jsonSerializer
            .DeserializeObject<ErrorResponse>(responseContent)
            .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
            .Bind(failure => Result<Unit>.FromFailure(failure));

    private static Result<Unit> CreateFailureResponseWithoutContent(HttpResponseMessage response) =>
        Result<Unit>.FromFailure(HttpFailure.From(response.StatusCode));

    private static Result<Unit> CreateSuccessResponse() => Result<Unit>.FromSuccess(Unit.Default);
}