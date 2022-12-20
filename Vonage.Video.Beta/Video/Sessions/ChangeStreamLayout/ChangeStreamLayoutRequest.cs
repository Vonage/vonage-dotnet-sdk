using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

/// <summary>
///     Represents a request to change a stream layout.
/// </summary>
public readonly struct ChangeStreamLayoutRequest
{
    private const string CannotBeNull = "cannot be null.";
    private const string CannotBeNullOrWhitespace = "cannot be null or whitespace.";

    /// <summary>
    ///     Parses the input into a ChangeStreamLayoutRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <param name="items">The layout items.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<ChangeStreamLayoutRequest> Parse(string applicationId, string sessionId,
        IEnumerable<LayoutItem> items) =>
        Result<ChangeStreamLayoutRequest>
            .FromSuccess(new ChangeStreamLayoutRequest(applicationId, sessionId, items))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyItems);

    private static Result<ChangeStreamLayoutRequest> VerifyApplicationId(ChangeStreamLayoutRequest request) =>
        VerifyNotEmptyValue(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<ChangeStreamLayoutRequest>
        VerifyNotEmptyValue(ChangeStreamLayoutRequest request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<ChangeStreamLayoutRequest>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CannotBeNullOrWhitespace}"))
            : request;

    private static Result<ChangeStreamLayoutRequest> VerifySessionId(ChangeStreamLayoutRequest request) =>
        VerifyNotEmptyValue(request, request.SessionId, nameof(SessionId));

    private static Result<ChangeStreamLayoutRequest> VerifyItems(ChangeStreamLayoutRequest request) =>
        request.Items is null
            ? Result<ChangeStreamLayoutRequest>.FromFailure(
                ResultFailure.FromErrorMessage($"{nameof(Items)} {CannotBeNull}"))
            : request;

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The layout items.
    /// </summary>
    public IEnumerable<LayoutItem> Items { get; }

    private ChangeStreamLayoutRequest(string applicationId, string sessionId, IEnumerable<LayoutItem> items)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Items = items;
    }

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/session/{this.SessionId}/stream";

    /// <summary>
    ///     Creates a Http request for changing a stream's layout.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The Http request.</returns>
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(new JsonSerializer().SerializeObject(new {this.Items}), Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    /// <summary>
    ///     Represents a request to change a stream with layout classes.
    /// </summary>
    public readonly struct LayoutItem
    {
        /// <summary>
        ///     Creates a new layout item.
        /// </summary>
        /// <param name="id">The stream Id.</param>
        /// <param name="layoutClassList">The layout classes.</param>
        public LayoutItem(string id, string[] layoutClassList)
        {
            this.Id = id;
            this.LayoutClassList = layoutClassList;
        }

        /// <summary>
        ///     The stream Id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     The layout classes.
        /// </summary>
        public string[] LayoutClassList { get; }
    }
}