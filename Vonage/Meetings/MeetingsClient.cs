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
using Vonage.Meetings.GetThemes;

namespace Vonage.Meetings;

/// <inheritdoc />
public class MeetingsClient : IMeetingsClient
{
    private readonly GetAvailableRoomsUseCase getAvailableRoomsUseCase;
    private readonly GetDialNumbersUseCase getDialNumbersUseCase;
    private readonly GetRecordingsUseCase getRecordingsUseCase;
    private readonly GetRecordingUseCase getRecordingUseCase;
    private readonly GetRoomUseCase getRoomUseCase;
    private readonly GetThemesUseCase getThemesUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public MeetingsClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        var vonageClient = new VonageHttpClient(httpClient, JsonSerializerBuilder.Build());
        this.getAvailableRoomsUseCase = new GetAvailableRoomsUseCase(vonageClient, tokenGeneration);
        this.getRoomUseCase = new GetRoomUseCase(vonageClient, tokenGeneration);
        this.getRecordingUseCase = new GetRecordingUseCase(vonageClient, tokenGeneration);
        this.getRecordingsUseCase = new GetRecordingsUseCase(vonageClient, tokenGeneration);
        this.getDialNumbersUseCase = new GetDialNumbersUseCase(vonageClient, tokenGeneration);
        this.getThemesUseCase = new GetThemesUseCase(vonageClient, tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<GetAvailableRoomsResponse>> GetAvailableRoomsAsync(GetAvailableRoomsRequest request) =>
        this.getAvailableRoomsUseCase.GetAvailableRoomsAsync(request);

    /// <inheritdoc />
    public Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync() =>
        this.getDialNumbersUseCase.GetDialNumbersAsync();

    /// <inheritdoc />
    public Task<Result<Recording>> GetRecordingAsync(Result<GetRecordingRequest> request) =>
        this.getRecordingUseCase.GetRecordingAsync(request);

    /// <inheritdoc />
    public Task<Result<GetRecordingsResponse>> GetRecordingsAsync(Result<GetRecordingsRequest> request) =>
        this.getRecordingsUseCase.GetRecordingsAsync(request);

    /// <inheritdoc />
    public Task<Result<Room>> GetRoomAsync(Result<GetRoomRequest> request) =>
        this.getRoomUseCase.GetRoomAsync(request);

    /// <inheritdoc />
    public Task<Result<Theme[]>> GetThemesAsync() =>
        this.getThemesUseCase.GetThemesAsync();
}