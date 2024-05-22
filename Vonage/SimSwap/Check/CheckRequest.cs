using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.SimSwap.Check;

/// <summary>
///     Represents a request to check if a SIM swap has been performed.
/// </summary>
public readonly struct CheckRequest : IVonageRequest

{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();
    
    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
    
    /// <inheritdoc />
    public string GetEndpointPath() => "camara/sim-swap/v040/check";
    
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
    
    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForPhoneNumber Build() => new CheckRequestBuilder();
}