#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;
#endregion

namespace Vonage.SimSwap.Check;

/// <summary>
///     Represents a request to check if a SIM swap has been performed.
/// </summary>
public readonly struct CheckRequest : IVonageRequest

{
    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    [JsonPropertyOrder(0)]
    [JsonPropertyName("phoneNumber")]
    public PhoneNumber PhoneNumber { get; internal init; }

    /// <summary>
    ///     Period in hours to be checked for SIM swap.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("maxAge")]
    public int Period { get; internal init; }

    private static string Scope => "dpv:FraudPreventionAndDetection#check-sim-swap";

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForPhoneNumber Build() => new CheckRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "camara/sim-swap/v040/check")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal Result<AuthenticateRequest> BuildAuthenticationRequest() =>
        AuthenticateRequest.Parse(this.PhoneNumber.NumberWithInternationalIndicator, Scope);
}