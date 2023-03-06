using System.IO.Abstractions;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.CreateTheme;
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
using Vonage.Meetings.UpdateTheme;
using Vonage.Meetings.UpdateThemeLogo;

namespace Vonage.Meetings;

/// <inheritdoc />
public class MeetingsClient : IMeetingsClient
{
    private readonly GetThemesUseCase getThemesUseCase;
    private readonly UpdateThemeLogoUseCase updateThemeLogoUseCase;
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    /// <param name="fileSystem">The file system.</param>
    public MeetingsClient(VonageHttpClientConfiguration configuration, IFileSystem fileSystem)
    {
        this.vonageClient =
            new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());
        this.getThemesUseCase = new GetThemesUseCase(this.vonageClient);
        this.updateThemeLogoUseCase =
            new UpdateThemeLogoUseCase(this.vonageClient, fileSystem.File.Exists, fileSystem.File.ReadAllBytes);
    }

    /// <inheritdoc />
    public Task<Result<Room>> CreateRoomAsync(Result<CreateRoomRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateRoomRequest, Room>(request);

    /// <inheritdoc />
    public Task<Result<Theme>> CreateThemeAsync(Result<CreateThemeRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateThemeRequest, Theme>(request);

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
        this.getThemesUseCase.GetThemesAsync();

    /// <inheritdoc />
    public Task<Result<UpdateApplicationResponse>> UpdateApplicationAsync(Result<UpdateApplicationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateApplicationRequest, UpdateApplicationResponse>(request);

    /// <inheritdoc />
    public Task<Result<Room>> UpdateRoomAsync(Result<UpdateRoomRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateRoomRequest, Room>(request);

    /// <inheritdoc />
    public Task<Result<Theme>> UpdateThemeAsync(Result<UpdateThemeRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateThemeRequest, Theme>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> UpdateThemeLogoAsync(Result<UpdateThemeLogoRequest> request) =>
        this.updateThemeLogoUseCase.UpdateThemeLogoAsync(request);
}