using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using Vonage.Server.Video.Broadcast.StartBroadcast;

namespace Vonage.Server.Video.Broadcast;

/// <summary>
///     Represents a client exposing live broadcast features.
/// </summary>
public class BroadcastClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    /// <param name="userAgent">The user agent.</param>
    public BroadcastClient(HttpClient httpClient, Func<string> tokenGeneration, string userAgent) => this.vonageClient =
        new VonageHttpClient(httpClient, JsonSerializerBuilder.Build(),
            new HttpClientOptions(tokenGeneration, userAgent));

    /// <summary>
    ///     Retrieves all live streaming broadcasts.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with broadcasts if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<GetBroadcastsResponse>> GetBroadcastsAsync(Result<GetBroadcastsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetBroadcastsRequest, GetBroadcastsResponse>(request);

    /// <summary>
    ///     Starts a live stream broadcast for an OpenTok session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the broadcast if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Common.Broadcast>> StartBroadcastsAsync(Result<StartBroadcastRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartBroadcastRequest, Common.Broadcast>(request);
}