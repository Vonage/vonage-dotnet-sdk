#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Sessions.ChangeStreamLayout;

/// <summary>
///     Represents a request to change a stream layout.
/// </summary>
[Builder]
public readonly partial struct ChangeStreamLayoutRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The layout items.
    /// </summary>
    [Mandatory(2)]
    public IEnumerable<LayoutItem> Items { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(new {this.Items}), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<ChangeStreamLayoutRequest> VerifyApplicationId(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<ChangeStreamLayoutRequest> VerifySessionId(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

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