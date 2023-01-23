using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetTheme;

public readonly struct GetThemeRequest : IVonageRequest
{
    private GetThemeRequest(string themeId) => this.ThemeId = themeId;

    /// <summary>
    ///     The theme identifier.
    /// </summary>
    public string ThemeId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/themes/{this.ThemeId}";

    /// <summary>
    ///     Parses the input into a GetThemeRequest.
    /// </summary>
    /// <param name="themeId">The theme identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetThemeRequest> Parse(string themeId) =>
        Result<GetThemeRequest>
            .FromSuccess(new GetThemeRequest(themeId))
            .Bind(VerifyThemeId);

    private static Result<GetThemeRequest> VerifyThemeId(GetThemeRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ThemeId, nameof(ThemeId));
}