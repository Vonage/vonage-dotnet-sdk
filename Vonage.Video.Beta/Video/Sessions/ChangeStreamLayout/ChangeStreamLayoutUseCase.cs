﻿using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

/// <inheritdoc />
public class ChangeStreamLayoutUseCase : IChangeStreamLayoutUseCase
{
    private readonly Func<string> generateToken;
    private readonly CustomClient customClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public ChangeStreamLayoutUseCase(CustomClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.customClient = client;
    }

    /// <inheritdoc />
    public Task<Result<Unit>> ChangeStreamLayoutAsync(ChangeStreamLayoutRequest request) =>
        this.customClient.SendAsync(request, this.generateToken());
}