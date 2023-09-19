using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Serialization;

namespace Vonage.Messages;

/// <summary>
/// </summary>
public class MessagesClient : IMessagesClient
{
    private const string Url = "/v1/messages";
    private readonly Credentials credentials;
    private readonly Uri uri;
    private readonly Configuration configuration;

    /// <summary>
    /// </summary>
    /// <param name="credentials"></param>
    public MessagesClient(Credentials credentials)
    {
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, Url);
        this.configuration = Configuration.Instance;
        this.credentials = credentials;
    }

    public MessagesClient(Credentials credentials, Configuration configuration)
    {
        this.credentials = credentials;
        this.configuration = configuration;
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, Url);
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<MessagesResponse> SendAsync(IMessage message)
    {
        var authType = this.credentials.GetPreferredAuthenticationType()
            .IfFailure(failure => throw failure.ToException());
        return new ApiRequest(this.credentials, this.configuration).DoRequestWithJsonContentAsync(
            HttpMethod.Post, this.uri,
            message,
            authType,
            value => JsonSerializerBuilder.Build().SerializeObject(value),
            value => JsonSerializerBuilder.Build().DeserializeObject<MessagesResponse>(value).GetSuccessUnsafe());
    }
}