using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a request for StartVerification with SilentAuth.
/// </summary>
public readonly struct StartSilentAuthVerificationRequest : IStartVerificationRequest
{
    /// <summary>
    ///     Gets the brand that is sending the verification request.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Brand { get; internal init; }

    /// <summary>
    ///     Gets verification workflows.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("workflow")]
    public SilentAuthWorkflow[] Workflows { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/verify";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}