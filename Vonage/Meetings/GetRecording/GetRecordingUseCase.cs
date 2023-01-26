﻿using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetRecording;

internal class GetRecordingUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetRecordingUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal Task<Result<Recording>> GetRecordingAsync(Result<GetRecordingRequest> request) =>
        this.httpClient.SendWithResponseAsync<Recording, GetRecordingRequest>(request,
            this.generateToken());
}