using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Video.Sessions;

namespace Vonage.Video.Beta.Video;

/// <summary>
/// </summary>
public class CustomClient
{
    private readonly HttpClient client;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    /// </summary>
    /// <param name="httpClient"></param>
    public CustomClient(HttpClient httpClient)
    {
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
    }

    public async Task<Result<T>> SendWithResponseAsync<T>(IVideoRequest request, string token)
    {
        var response = await this.client.SendAsync(request.BuildRequestMessage(token));
        var responseContent = await response.Content.ReadAsStringAsync();
        return !response.IsSuccessStatusCode
            ? await this.CreateFailureResponse<T>(response)
            : this.jsonSerializer
                .DeserializeObject<T>(responseContent)
                .Match(_ => _, Result<T>.FromFailure);
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Result<Unit>> SendAsync(IVideoRequest request, string token)
    {
        var response = await this.client.SendAsync(request.BuildRequestMessage(token));
        return !response.IsSuccessStatusCode
            ? await this.CreateFailureResponse<Unit>(response)
            : CreateSuccessResponse();
    }

    private async Task<Result<T>> CreateFailureResponse<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(responseContent)
            ? CreateFailureResponseWithoutContent<T>(response)
            : this.CreateFailureResponseWithContent<T>(responseContent, response);
    }

    private Result<T> CreateFailureResponseWithContent<T>(string responseContent, HttpResponseMessage response) =>
        this.jsonSerializer
            .DeserializeObject<ErrorResponse>(responseContent)
            .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
            .Bind(failure => Result<T>.FromFailure(failure));

    private static Result<T> CreateFailureResponseWithoutContent<T>(HttpResponseMessage response) =>
        Result<T>.FromFailure(HttpFailure.From(response.StatusCode));

    private static Result<Unit> CreateSuccessResponse() => Result<Unit>.FromSuccess(Unit.Default);
}