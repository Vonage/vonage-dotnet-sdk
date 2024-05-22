using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;

namespace Vonage.SimSwap.GetSwapDate;

/// <summary>
///     Represents a request to retrieve a SIM swap date.
/// </summary>
public readonly struct GetSwapDateRequest : IVonageRequest
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
    public string GetEndpointPath() => "camara/sim-swap/v040/retrieve-date";
    
    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    [JsonPropertyName("phoneNumber")]
    public PhoneNumber PhoneNumber { get; internal init; }
    
    private static string Scope => "dpv:FraudPreventionAndDetection#retrieve-sim-swap-date";
    
    internal Result<AuthenticateRequest> BuildAuthenticationRequest() =>
        AuthenticateRequest.Parse(this.PhoneNumber.NumberWithInternationalIndicator, Scope);
    
    /// <summary>
    ///     Parses the input into an GetSwapDateRequest.
    /// </summary>
    /// <param name="number">The phone number.</param>
    /// <returns>Success if the input matches all requirements. Failure otherwise.</returns>
    public static Result<GetSwapDateRequest> Parse(string number) =>
        PhoneNumber.Parse(number).Map(phoneNumber => new GetSwapDateRequest
        {
            PhoneNumber = phoneNumber,
        });
}