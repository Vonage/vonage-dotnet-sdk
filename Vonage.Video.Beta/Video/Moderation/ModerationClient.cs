﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Moderation.DisconnectConnection;
using Vonage.Video.Beta.Video.Moderation.MuteStream;
using Vonage.Video.Beta.Video.Moderation.MuteStreams;

namespace Vonage.Video.Beta.Video.Moderation;

/// <summary>
///     Represents a client exposing moderation features.
/// </summary>
public class ModerationClient
{
    private readonly DisconnectConnectionUseCase disconnectConnectionUseCase;
    private readonly MuteStreamUseCase muteStreamUseCase;
    private readonly MuteStreamsUseCase muteStreamsUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public ModerationClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        this.disconnectConnectionUseCase =
            new DisconnectConnectionUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.muteStreamUseCase = new MuteStreamUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.muteStreamsUseCase = new MuteStreamsUseCase(new VideoHttpClient(httpClient), tokenGeneration);
    }

    /// <summary>
    ///     Forces a client to disconnect from a session
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.disconnectConnectionUseCase.DisconnectConnectionAsync(request);

    /// <summary>
    ///     Mutes a specific publisher stream
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.muteStreamUseCase.MuteStreamAsync(request);

    /// <summary>
    ///     Forces all streams (except for an optional list of streams) in a session to mute published audio. You can also use
    ///     this method to disable the force mute state of a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.muteStreamsUseCase.MuteStreamsAsync(request);
}