#region
using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Sessions.ListConnections;

/// <summary>
///     Represents a request to list connections of a session.
/// </summary>
[Builder]
public readonly partial struct ListConnectionsRequest : IVonageRequest
{
    /// <summary>
    ///     Vonage Application UUID
    /// </summary>
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     Video session ID
    /// </summary>
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<ListConnectionsRequest> VerifyApplicationId(ListConnectionsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<ListConnectionsRequest> VerifySessionId(ListConnectionsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}