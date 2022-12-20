using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

/// <inheritdoc />
public class ChangeStreamLayoutUseCase : IChangeStreamLayoutUseCase
{
    private readonly HttpClient client;
    private readonly Credentials credentials;
    private readonly JsonSerializer jsonSerializer;
    private readonly ITokenGenerator tokenGenerator;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further connections.</param>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGenerator">Generator for authentication tokens.</param>
    public ChangeStreamLayoutUseCase(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    /// <inheritdoc />
    public async Task<Result<Unit>> ChangeStreamLayoutAsync(ChangeStreamLayoutRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.tokenGenerator.GenerateToken(this.credentials));
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return string.IsNullOrWhiteSpace(responseContent)
                ? Result<Unit>.FromFailure(HttpFailure.From(response.StatusCode))
                : this.jsonSerializer
                    .DeserializeObject<ErrorResponse>(responseContent)
                    .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
                    .Bind(failure => Result<Unit>.FromFailure(failure));
        }

        return Result<Unit>.FromSuccess(Unit.Default);
    }
}