using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.ExperienceComposer.GetSession;

namespace Vonage.Video.ExperienceComposer;

/// <summary>
///     Represents a client exposing Experience Composer features.
/// </summary>
public class ExperienceComposerClient
{
    private readonly VonageHttpClient vonageClient;

    internal ExperienceComposerClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Retrieves details on an Experience Composer session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Session>> GetSessionAsync(Result<GetSessionRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetSessionRequest, Session>(request);
}