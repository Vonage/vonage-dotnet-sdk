using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
using Vonage.Serialization;

namespace Vonage.Messages;

/// <summary>
/// </summary>
public class MessagesClient : IMessagesClient
{
    private const string Url = "/v1/messages";
    private readonly Configuration configuration;
    private readonly Credentials credentials;
    private readonly ITimeProvider timeProvider = new TimeProvider();
    private readonly Uri uri;

    /// <summary>
    /// </summary>
    /// <param name="credentials"></param>
    public MessagesClient(Credentials credentials)
    {
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, Url);
        this.configuration = Configuration.Instance;
        this.credentials = credentials;
    }

    internal MessagesClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.credentials = credentials;
        this.configuration = configuration;
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, Url);
        this.timeProvider = timeProvider;
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task<MessagesResponse> SendAsync(IMessage message)
    {
        var authType = this.credentials.GetPreferredAuthenticationType()
            .IfFailure(failure => throw failure.ToException());
        return ApiRequest.Build(this.credentials, this.configuration, this.timeProvider).DoRequestWithJsonContentAsync(
            HttpMethod.Post, this.uri,
            message,
            authType,
            value => JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(value),
            value => JsonSerializerBuilder.BuildWithSnakeCase().DeserializeObject<MessagesResponse>(value)
                .GetSuccessUnsafe());
    }
}