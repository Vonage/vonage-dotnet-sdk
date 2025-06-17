#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.NumberVerification.Authenticate;
using Vonage.Serialization;
#endregion

namespace Vonage.NumberVerification.Verify;

/// <summary>
///     Represents a request to verify if the specified phone number matches the one that the user is currently using.
/// </summary>
public readonly struct VerifyRequest : IVonageRequest
{
    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    [JsonPropertyOrder(0)]
    [JsonPropertyName("phoneNumber")]
    public PhoneNumber PhoneNumber { get; internal init; }

    private static string Scope => "dpv:FraudPreventionAndDetection#number-verification-verify-read";

    /// <summary>
    ///     Parses the input into an VerifyRequest.
    /// </summary>
    /// <param name="number">The phone number.</param>
    /// <returns>Success if the input matches all requirements. Failure otherwise.</returns>
    public static Result<VerifyRequest> Parse(string number) =>
        PhoneNumber.Parse(number).Map(phoneNumber => new VerifyRequest
        {
            PhoneNumber = phoneNumber,
        });

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "camara/number-verification/v031/verify")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal Result<AuthenticateRequest> BuildAuthenticationRequest() =>
        AuthenticateRequest.Parse(this.PhoneNumber.NumberWithInternationalIndicator, Scope);
}