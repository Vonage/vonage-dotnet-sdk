using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetTheme;

/// <summary>
///     Represents a request for retrieving a theme.
/// </summary>
public readonly struct GetThemeRequest : IVonageRequest
{
    private GetThemeRequest(Guid themeId) => this.ThemeId = themeId;

    /// <summary>
    ///     The theme identifier.
    /// </summary>
    public Guid ThemeId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/meetings/themes/{this.ThemeId}";

    /// <summary>
    ///     Parses the input into a GetThemeRequest.
    /// </summary>
    /// <param name="themeId">The theme identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetThemeRequest> Parse(Guid themeId) =>
        Result<GetThemeRequest>
            .FromSuccess(new GetThemeRequest(themeId))
            .Map(InputEvaluation<GetThemeRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyThemeId));

    private static Result<GetThemeRequest> VerifyThemeId(GetThemeRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ThemeId, nameof(ThemeId));
}