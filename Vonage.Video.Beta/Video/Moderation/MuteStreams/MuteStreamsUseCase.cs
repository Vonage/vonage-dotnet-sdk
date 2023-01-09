﻿using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

/// <inheritdoc />
internal class MuteStreamsUseCase : IMuteStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public MuteStreamsUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<MuteStreamsResponse, MuteStreamsRequest>(request,
            this.generateToken());
}