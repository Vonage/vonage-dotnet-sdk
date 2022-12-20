using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

/// <inheritdoc />
public class GetStreamUseCase : IGetStreamUseCase
{
    private readonly HttpClient client;
    private readonly Func<string> generateToken;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetStreamUseCase(HttpClient httpClient, Func<string> generateToken)
    {
        this.client = httpClient;
        this.generateToken = generateToken;
        this.jsonSerializer = new JsonSerializer();
    }

    public async Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.generateToken());
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        return !response.IsSuccessStatusCode
            ? this.jsonSerializer
                .DeserializeObject<ErrorResponse>(responseContent)
                .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
                .Bind(failure => Result<GetStreamResponse>.FromFailure(failure))
            : this.jsonSerializer
                .DeserializeObject<GetStreamResponse>(responseContent)
                .Match(_ => _, Result<GetStreamResponse>.FromFailure);
    }
}