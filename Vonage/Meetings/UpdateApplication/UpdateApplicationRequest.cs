using System;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.UpdateApplication;

/// <summary>
///     Represents a request to update an existing application.
/// </summary>
public readonly struct UpdateApplicationRequest : IVonageRequest
{
    private UpdateApplicationRequest(Guid themeId) => this.DefaultThemeId = themeId;

    /// <summary>
    /// </summary>
    public Guid DefaultThemeId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/meetings/applications";

    /// <summary>
    ///     Parses the input into a UpdateApplicationRequest.
    /// </summary>
    /// <param name="themeId">The theme id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<UpdateApplicationRequest> Parse(Guid themeId) =>
        Result<UpdateApplicationRequest>
            .FromSuccess(new UpdateApplicationRequest(themeId))
            .Map(InputEvaluation<UpdateApplicationRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyDefaultThemeId));

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(new {UpdateDetails = this}),
            Encoding.UTF8,
            "application/json");

    private static Result<UpdateApplicationRequest> VerifyDefaultThemeId(UpdateApplicationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.DefaultThemeId, nameof(DefaultThemeId));
}