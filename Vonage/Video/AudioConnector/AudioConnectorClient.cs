#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.AudioConnector.Start;
#endregion

namespace Vonage.Video.AudioConnector;

/// <summary>
///     Represents a client exposing Audio Connector features.
/// </summary>
public class AudioConnectorClient
{
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    internal AudioConnectorClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Sends audio from a Vonage Video API session to a WebSocket.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = StartRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithToken(token)
    ///     .WithUrl(new Uri("wss://example.com/audio"))
    ///     .Create();
    /// var result = await client.VideoClient.AudioConnectorClient.StartAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<StartResponse>> StartAsync(Result<StartRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartRequest, StartResponse>(request);
}