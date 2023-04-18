using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sessions.ChangeStreamLayout;

/// <summary>
///     Represents a request to change a stream layout.
/// </summary>
public readonly struct ChangeStreamLayoutRequest : IVonageRequest, IHasApplicationId
{
    private ChangeStreamLayoutRequest(Guid applicationId, string sessionId, IEnumerable<LayoutItem> items)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Items = items;
    }

    /// <inheritdoc />
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The layout items.
    /// </summary>
    public IEnumerable<LayoutItem> Items { get; }

    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream";

    /// <summary>
    ///     Parses the input into a ChangeStreamLayoutRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="sessionId">The session Id.</param>
    /// <param name="items">The layout items.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<ChangeStreamLayoutRequest> Parse(Guid applicationId, string sessionId,
        IEnumerable<LayoutItem> items) =>
        Result<ChangeStreamLayoutRequest>
            .FromSuccess(new ChangeStreamLayoutRequest(applicationId, sessionId, items))
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyItems);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(new {this.Items}),
            Encoding.UTF8,
            "application/json");

    private static Result<ChangeStreamLayoutRequest> VerifyItems(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotNull(request, request.Items, nameof(Items));

    private static Result<ChangeStreamLayoutRequest> VerifySessionId(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    /// <summary>
    ///     Represents a request to change a stream with layout classes.
    /// </summary>
    public readonly struct LayoutItem
    {
        /// <summary>
        ///     The stream Id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     The layout classes.
        /// </summary>
        public string[] LayoutClassList { get; }

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
    }
}