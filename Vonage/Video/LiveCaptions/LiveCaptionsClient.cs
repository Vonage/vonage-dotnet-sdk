#region
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.LiveCaptions.Stop;
#endregion

namespace Vonage.Video.LiveCaptions;

/// <summary>
///     Represents a client exposing Live Captions features.
/// </summary>
public class LiveCaptionsClient
{
    private readonly VonageHttpClient vonageClient;

    internal LiveCaptionsClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Stops live captions for a session
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> StopAsync(Result<StopRequest> request) =>
        this.vonageClient.SendAsync(request);
}