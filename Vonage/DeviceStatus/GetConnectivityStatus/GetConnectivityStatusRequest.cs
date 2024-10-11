#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.DeviceStatus.Authenticate;
using Vonage.Serialization;
#endregion

namespace Vonage.DeviceStatus.GetConnectivityStatus;

/// <inheritdoc />
public readonly struct GetConnectivityStatusRequest : IVonageRequest
{
    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    [JsonPropertyOrder(0)]
    [JsonPropertyName("phoneNumber")]
    public PhoneNumber PhoneNumber { get; internal init; }

    private static string Scope => "dpv:NotApplicable#device-status:connectivity:read";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "camara/device-status/v050/connectivity";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(new
            {
                Device = new
                {
                    PhoneNumber = this.PhoneNumber.Number,
                }
            }), Encoding.UTF8,
            "application/json");

    internal Result<AuthenticateRequest> BuildAuthenticationRequest() =>
        AuthenticateRequest.Parse(this.PhoneNumber.NumberWithInternationalIndicator, Scope);

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForPhoneNumber Build() => new GetConnectivityStatusRequestBuilder();
}