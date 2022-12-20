using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

/// <inheritdoc />
public class GetStreamsUseCase : IGetStreamsUseCase
{
    private readonly HttpClient client;
    private readonly Func<string> generateToken;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetStreamsUseCase(HttpClient httpClient, Func<string> generateToken)
    {
        this.client = httpClient;
        this.generateToken = generateToken;
        this.jsonSerializer = new JsonSerializer();
    }

    /// <inheritdoc />
    public async Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.generateToken());
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return string.IsNullOrWhiteSpace(responseContent)
                ? Result<GetStreamsResponse>.FromFailure(HttpFailure.From(response.StatusCode))
                : this.jsonSerializer
                    .DeserializeObject<ErrorResponse>(responseContent)
                    .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
                    .Bind(failure => Result<GetStreamsResponse>.FromFailure(failure));
        }

        return this.jsonSerializer
            .DeserializeObject<GetStreamsResponse>(responseContent)
            .Match(_ => _, Result<GetStreamsResponse>.FromFailure);
    }
}