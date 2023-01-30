using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Meetings.GetDialNumbers;
using Vonage.Meetings.GetRecording;
using Vonage.Meetings.GetRecordings;
using Vonage.Meetings.GetRoom;
using Vonage.Meetings.GetTheme;
using Vonage.Meetings.GetThemes;

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
        new VonageHttpClient(httpClient, JsonSerializerBuilder.Build(),
            new HttpClientOptions(tokenGeneration, userAgent));

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
    public Task<Result<Theme>> GetThemeAsync(Result<GetThemeRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetThemeRequest, Theme>(request);

    /// <inheritdoc />
    public Task<Result<Theme[]>> GetThemesAsync() =>
        this.vonageClient.SendWithResponseAsync<GetThemesRequest, Theme[]>(GetThemesRequest.Default);
}