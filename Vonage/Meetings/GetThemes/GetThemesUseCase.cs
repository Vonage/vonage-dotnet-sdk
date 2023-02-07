using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetThemes;

internal class GetThemesUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal GetThemesUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    private static Result<Theme[]> WorkaroundForEmptyResult(IResultFailure failure) =>
        failure.GetFailureMessage() == "Unable to deserialize '' into 'Theme[]'."
            ? Result<Theme[]>.FromSuccess(Array.Empty<Theme>())
            : Result<Theme[]>.FromFailure(failure);

    internal async Task<Result<Theme[]>> GetThemesAsync() =>
        (await this.vonageHttpClient.SendWithResponseAsync<GetThemesRequest, Theme[]>(GetThemesRequest.Default))
        .Match(_ => _, WorkaroundForEmptyResult);
}