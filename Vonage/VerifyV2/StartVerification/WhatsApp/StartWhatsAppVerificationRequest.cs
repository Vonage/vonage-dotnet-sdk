using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <inheritdoc />
public readonly struct StartWhatsAppVerificationRequest : IStartVerificationRequest
{
    /// <summary>
    ///     Gets the brand that is sending the verification request.
    /// </summary>
    [JsonPropertyOrder(4)]
    public string Brand { get; internal init; }

    /// <summary>
    ///     Gets the wait time in seconds between attempts to delivery the verification code.
    /// </summary>
    [JsonPropertyOrder(1)]
    public int ChannelTimeout { get; internal init; }

    /// <summary>
    ///     Gets the client reference.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("client_ref")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    public Maybe<string> ClientReference { get; internal init; }

    /// <summary>
    ///     Gets the length of the code to send to the user
    /// </summary>
    [JsonPropertyOrder(3)]
    public int CodeLength { get; internal init; }

    /// <summary>
    ///     Gets the request language.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(LocaleJsonConverter))]
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     Gets verification workflows.
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonPropertyName("workflow")]
    public WhatsAppWorkflow[] Workflows { get; internal init; }

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

/// <summary>
///     Represents a verification workflow for WhatsApp.
/// </summary>
/// <param name="Channel">The channel name.</param>
/// <param name="To">
///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
/// <param name="From">
///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
public record WhatsAppWorkflow([property: JsonPropertyOrder(1)] string To,
    [property: JsonPropertyOrder(3)]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> From) : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp";

    /// <inheritdoc />
    public WhatsAppWorkflow(string to)
        : this(to, Maybe<string>.None)
    {
    }
}