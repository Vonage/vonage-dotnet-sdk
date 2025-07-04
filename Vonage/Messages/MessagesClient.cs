#region
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
using Vonage.Serialization;
#endregion

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
        this.configuration = Configuration.Instance;
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, Url);
        this.credentials = credentials;
    }

    internal MessagesClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.credentials = credentials;
        this.configuration = configuration;
        this.uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, Url);
        this.timeProvider = timeProvider;
    }

    /// <inheritdoc />
    public Task<MessagesResponse> SendAsync(IMessage message)
    {
        var authType = this.credentials.GetPreferredAuthenticationType()
            .IfFailure(failure => throw failure.ToException());
        return ApiRequest.Build(this.credentials, this.configuration, this.timeProvider).DoRequestWithJsonContentAsync(
            HttpMethod.Post, this.uri,
            message,
            authType,
            _ => message.Serialize(),
            value => JsonSerializerBuilder.BuildWithSnakeCase().DeserializeObject<MessagesResponse>(value)
                .GetSuccessUnsafe());
    }

    /// <inheritdoc />
    public async Task UpdateAsync(IUpdateMessageRequest request)
    {
        var authType = this.credentials.GetPreferredAuthenticationType()
            .IfFailure(failure => throw failure.ToException());
        await ApiRequest.Build(this.credentials, this.configuration, this.timeProvider).DoRequestWithJsonContentAsync(
            new HttpMethod("PATCH"), ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration,
                $"{Url}/{request.MessageUuid}"),
            request,
            authType,
            value => JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(value),
            _ => _);
    }
}