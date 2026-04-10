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
    ///     Sets the Vonage Application UUID.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApplicationId(applicationId)
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     Sets the Vonage Video session ID.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
    /// ]]></code>
    /// </example>
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