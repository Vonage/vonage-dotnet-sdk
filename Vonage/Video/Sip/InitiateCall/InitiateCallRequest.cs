#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Sip.InitiateCall;

/// <summary>
///     Represents a request to initiate an outbound Sip call.
/// </summary>
public readonly struct InitiateCallRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The sip element.
    /// </summary>
    [JsonPropertyOrder(1)]
    public SipElement Sip { get; internal init; }

    /// <summary>
    ///     The OpenTok token to be used for the participant being called. You can add token data to identify that the
    ///     participant is on a SIP endpoint or for other identifying data, such as phone numbers. (The OpenTok client
    ///     libraries include properties for inspecting the connection data for a client connected to a session.) See the Token
    ///     Creation developer guide.
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Token { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new InitiateCallRequestBuilder();

    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/dial")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}