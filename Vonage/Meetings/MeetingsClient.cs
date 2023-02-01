﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.DeleteRecording;
using Vonage.Meetings.DeleteTheme;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Meetings.GetRecording;
using Vonage.Meetings.GetRecordings;
using Vonage.Meetings.GetRoom;
using Vonage.Meetings.GetRoomsByTheme;
using Vonage.Meetings.GetTheme;
using Vonage.Meetings.GetThemes;
using Vonage.Meetings.UpdateApplication;
using Vonage.Meetings.UpdateRoom;

namespace Vonage.Meetings;

/// <inheritdoc />
public class MeetingsClient : IMeetingsClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    /// <param name="userAgent">The user agent.</param>
    public MeetingsClient(HttpClient httpClient, Func<string> tokenGeneration, string userAgent) => this.vonageClient =
        new VonageHttpClient(httpClient, JsonSerializer.BuildWithSnakeCase(),
            new HttpClientOptions(tokenGeneration, userAgent));

    /// <inheritdoc />
    public Task<Result<Room>> CreateRoomAsync(Result<CreateRoomRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateRoomRequest, Room>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteRecordingAsync(Result<DeleteRecordingRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteThemeAsync(Result<DeleteThemeRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request) =>
        this.vonageClient.SendWithResponseAsync<GetAvailableRoomsRequest, GetAvailableRoomsResponse>(request);

    /// <inheritdoc />
    public Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync() =>
        this.vonageClient.SendWithResponseAsync<GetDialNumbersRequest, GetDialNumbersResponse[]>(GetDialNumbersRequest
            .Default);

    /// <inheritdoc />
    public Task<Result<Recording>> GetRecordingAsync(Result<GetRecordingRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetRecordingRequest, Recording>(request);

    /// <inheritdoc />
    public Task<Result<GetRecordingsResponse>> GetRecordingsAsync(Result<GetRecordingsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetRecordingsRequest, GetRecordingsResponse>(request);

    /// <inheritdoc />
    public Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetRoomRequest, Room>(request);

    /// <inheritdoc />
    public Task<Result<GetRoomsByThemeResponse>> GetRoomsByThemeAsync(Result<GetRoomsByThemeRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetRoomsByThemeRequest, GetRoomsByThemeResponse>(request);

    /// <inheritdoc />
    public Task<Result<Theme>> GetThemeAsync(Result<GetThemeRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetThemeRequest, Theme>(request);

    /// <inheritdoc />
    public Task<Result<Theme[]>> GetThemesAsync() =>
        this.vonageClient.SendWithResponseAsync<GetThemesRequest, Theme[]>(GetThemesRequest.Default);

    /// <inheritdoc />
    public Task<Result<UpdateApplicationResponse>> UpdateApplicationAsync(Result<UpdateApplicationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateApplicationRequest, UpdateApplicationResponse>(request);

    /// <inheritdoc />
    public Task<Result<Room>> UpdateRoomAsync(Result<UpdateRoomRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateRoomRequest, Room>(request);
}