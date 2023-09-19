using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Redaction;

public class RedactClient : IRedactClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public RedactClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal RedactClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public bool Redact(RedactRequest request, Credentials creds = null)
    {
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContent<object>
        (
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v1/redact/transaction"),
            request,
            AuthType.Basic
        );
        return true;
    }

    public async Task<bool> RedactAsync(RedactRequest request, Credentials creds = null)
    {
        await new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContentAsync<object>
        (
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v1/redact/transaction"),
            request,
            AuthType.Basic
        );
        return true;
    }
}