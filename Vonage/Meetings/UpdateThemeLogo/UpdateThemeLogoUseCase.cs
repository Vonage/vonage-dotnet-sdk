using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateThemeLogo;

internal class UpdateThemeLogoUseCase
{
    /// <summary>
    ///     Indicates no matching logo was found.
    /// </summary>
    public const string NoMatchingLogo = "No logo matches the requested type.";

    private readonly VonageHttpClient vonageHttpClient;

    internal UpdateThemeLogoUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    private static Result<GetUploadLogosUrlResponse> FilterOnLogoType(GetUploadLogosUrlResponse[] url,
        ThemeLogoType logoType) =>
        url.Any(VerifyMatchingLogoType(logoType))
            ? url.Where(VerifyMatchingLogoType(logoType)).ToArray()[0]
            : Result<GetUploadLogosUrlResponse>.FromFailure(ResultFailure.FromErrorMessage(NoMatchingLogo));

    private async Task<Result<GetUploadLogosUrlResponse>> GetLogoResponseAsync(ThemeLogoType logoType)
    {
        var responseResult = await this.vonageHttpClient
            .SendWithResponseAsync<GetUploadLogosUrlRequest, GetUploadLogosUrlResponse[]>(GetUploadLogosUrlRequest
                .Default);
        return responseResult.Bind(responses => FilterOnLogoType(responses, logoType));
    }

    private Task<Result<Unit>> SendFinalizeLogoRequestAsync(FinalizeLogoRequest request) =>
        this.vonageHttpClient.SendAsync<FinalizeLogoRequest>(request);

    private async Task<Result<UploadLogoRequest>> SendUploadLogoRequestAsync(UploadLogoRequest uploadLogoRequest) =>
        (await this.vonageHttpClient.SendAsync<UploadLogoRequest>(uploadLogoRequest))
        .Match(_ => uploadLogoRequest, Result<UploadLogoRequest>.FromFailure);

    private async Task<Result<Unit>> UpdateThemeLogoAsync(UpdateThemeLogoRequest request)
    {
        var logosUrlResponse = await this.GetLogoResponseAsync(request.Type);
        return await logosUrlResponse
            .Map(response => UploadLogoRequest.FromLogosUrl(response, request.FilePath))
            .BindAsync(this.SendUploadLogoRequestAsync)
            .BindAsync(uploadLogoRequest =>
                this.SendFinalizeLogoRequestAsync(
                    new FinalizeLogoRequest(request.ThemeId, uploadLogoRequest.Fields.Key)));
    }

    private static Func<GetUploadLogosUrlResponse, bool> VerifyMatchingLogoType(ThemeLogoType logoType) =>
        response => response.MatchesLogoType(logoType);

    internal Task<Result<Unit>> UpdateThemeLogoAsync(Result<UpdateThemeLogoRequest> request) =>
        request.BindAsync(this.UpdateThemeLogoAsync);
}