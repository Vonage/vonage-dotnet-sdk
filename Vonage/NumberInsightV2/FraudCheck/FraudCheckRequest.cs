using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.NumberInsightV2.FraudCheck;

/// <inheritdoc />
public readonly struct FraudCheckRequest : IVonageRequest
{
    /// <summary>
    ///     The insight(s) you need, at least one of: fraud_score and sim_swap.
    /// </summary>
    [JsonPropertyOrder(2)]
    public IEnumerable<string> Insights { get; internal init; }

    /// <summary>
    ///     A single phone number that you need insight about in the E.164 format. Don't use a leading + or 00 when entering a
    ///     phone number, start with the country code, e.g. 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber Phone { get; internal init; }

    /// <summary>
    ///     Accepted value is “phone” when a phone number is provided.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "phone";

    /// <summary>
    ///     Initializes a builder for FraudCheckRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForPhone Build() => new FraudCheckRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v2/ni";

    private StringContent GetRequestContent() => new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
        Encoding.UTF8, "application/json");
}