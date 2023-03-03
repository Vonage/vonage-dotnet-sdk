using System;
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

    /// <summary>
    /// </summary>
    /// <param name="credentials"></param>
    public MessagesClient(Credentials credentials)
    {
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, Url);
        this.credentials = credentials;
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<MessagesResponse> SendAsync(IMessage message) =>
        ApiRequest.DoRequestWithJsonContentAsync(
            "POST", this.uri,
            message,
            ApiRequest.AuthType.Bearer,
            this.credentials,
            value => JsonSerializerBuilder.Build().SerializeObject(value),
            value => JsonSerializerBuilder.Build().DeserializeObject<MessagesResponse>(value).GetSuccessUnsafe());
}