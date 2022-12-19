using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Video.Beta.Video.Sessions.GetStreams;

namespace Vonage.Video.Beta.Video.Sessions;

/// <summary>
///     Exposes features for managing sessions.
/// </summary>
public interface ISessionClient
{
    /// <summary>
    ///     Represents the credentials that will be used for further connections.
    /// </summary>
    Credentials Credentials { get; }

    /// <summary>
    ///     Creates a new session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request);

    /// <summary>
    ///     Retrieves a stream.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the stream if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request);

    /// <summary>
    ///     Retrieves information on all Vonage Video streams in a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with streams if the operation succeeded. A failure state with the error message if it failed.</returns>
    Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request);
}