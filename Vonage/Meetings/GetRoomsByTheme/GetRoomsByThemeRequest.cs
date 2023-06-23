using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetRoomsByTheme;

/// <summary>
///     Represents a request to get all rooms associated with a theme.
/// </summary>
public readonly struct GetRoomsByThemeRequest : IVonageRequest
{
    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public Maybe<int> EndId { get; internal init; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public Maybe<int> StartId { get; internal init; }

    /// <summary>
    /// </summary>
    public Guid ThemeId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForThemeId Build() => new GetRoomsByThemeRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString($"/meetings/themes/{this.ThemeId}/rooms",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.StartId.Map(value => value.ToString()).IfSome(value => parameters.Add("start_id", value));
        this.EndId.Map(value => value.ToString()).IfSome(value => parameters.Add("end_id", value));
        return parameters;
    }
}