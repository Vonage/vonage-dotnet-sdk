#region
using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;
#endregion

namespace Vonage.Meetings.UpdateThemeLogo;

internal class UpdateThemeLogoUseCase
{
    /// <summary>
    ///     Indicates no matching logo was found.
    /// </summary>
    public const string NoMatchingLogo = "No logo matches the requested type.";

    private readonly Func<string, bool> fileExistOperation;
    private readonly Func<string, byte[]> readFileOperation;
    private readonly VonageHttpClient<StandardApiError> vonageHttpClient;

    internal UpdateThemeLogoUseCase(VonageHttpClient<StandardApiError> client,
        Func<string, bool> fileExistOperation,
        Func<string, byte[]> readFileOperation)
    {
        this.vonageHttpClient = client;
        this.readFileOperation = readFileOperation;
        this.fileExistOperation = fileExistOperation;
    }

    private static Result<GetUploadLogosUrlResponse> FilterLogosOnType(GetUploadLogosUrlResponse[] url,
        ThemeLogoType logoType) =>
        url.Any(VerifyMatchingLogoType(logoType))
            ? url.Where(VerifyMatchingLogoType(logoType)).ToArray()[0]
            : ResultFailure.FromErrorMessage(NoMatchingLogo).ToResult<GetUploadLogosUrlResponse>();

    private Task<Result<Unit>> FinalizeLogoAsync(UploadData request) =>
        this.vonageHttpClient.SendAsync<FinalizeLogoRequest>(new FinalizeLogoRequest(request.ThemeData.ThemeId,
            request.Request.Fields.Key));

    private async Task<Result<LogoUrlData>> GetLogoUrlAsync(UpdateThemeData themeData) =>
        await this.vonageHttpClient
            .SendWithResponseAsync<GetUploadLogosUrlRequest, GetUploadLogosUrlResponse[]>(GetUploadLogosUrlRequest
                .Default)
            .Bind(responses => FilterLogosOnType(responses, themeData.Type))
            .Map(response => new LogoUrlData(response, themeData)).ConfigureAwait(false);

    private Result<UpdateThemeData> LoadFile(UpdateThemeLogoRequest request) =>
        this.fileExistOperation(request.FilePath)
            ? new UpdateThemeData(this.readFileOperation(request.FilePath), request.ThemeId, request.Type)
            : ResultFailure.FromErrorMessage("The file cannot be found.").ToResult<UpdateThemeData>();

    private static UploadData ToUploadLogoRequest(LogoUrlData logoUrl) =>
        new(UploadLogoRequest.FromLogosUrl(logoUrl.Response, logoUrl.ThemeData.File), logoUrl.ThemeData);

    private async Task<Result<UploadData>> UploadLogoAsync(UploadData uploadLogoRequest) =>
        (await this.vonageHttpClient.SendWithoutHeadersAsync<UploadLogoRequest>(uploadLogoRequest.Request)
            .ConfigureAwait(false))
        .Match(_ => uploadLogoRequest, Result<UploadData>.FromFailure);

    private static Func<GetUploadLogosUrlResponse, bool> VerifyMatchingLogoType(ThemeLogoType logoType) =>
        response => response.MatchesLogoType(logoType);

    internal Task<Result<Unit>> UpdateThemeLogoAsync(Result<UpdateThemeLogoRequest> request) =>
        request.Bind(this.LoadFile)
            .BindAsync(this.GetLogoUrlAsync)
            .Map(ToUploadLogoRequest)
            .BindAsync(this.UploadLogoAsync)
            .BindAsync(this.FinalizeLogoAsync);

    private record UpdateThemeData(byte[] File, Guid ThemeId, ThemeLogoType Type);

    private record LogoUrlData(GetUploadLogosUrlResponse Response, UpdateThemeData ThemeData);

    private record UploadData(UploadLogoRequest Request, UpdateThemeData ThemeData);
}