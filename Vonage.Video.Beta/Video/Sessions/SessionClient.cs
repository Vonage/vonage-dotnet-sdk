using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions;

public class SessionClient : ISessionClient
{
    private readonly HttpClient client;
    private readonly JsonSerializer jsonSerializer;
    private readonly ITokenGenerator tokenGenerator;

    public SessionClient(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.Credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    public Credentials Credentials { get; set; }

    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request)
    {
        var token = this.tokenGenerator.GenerateToken(this.Credentials.ApplicationId, this.Credentials.ApplicationKey);
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, CreateSessionRequest.CreateSessionEndpoint);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content =
            new StringContent(request.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        return this.jsonSerializer
            .DeserializeObject<CreateSessionResponse[]>(responseContent)
            .Bind(GetFirstSessionIfAvailable);
    }

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions.First()
            : Result<CreateSessionResponse>.FromFailure(ResultFailure.FromErrorMessage(""));
}