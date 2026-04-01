#region
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.NumberInsightV2.FraudCheck;

/// <summary>
///     Represents a request to perform fraud detection checks on a phone number using the Number Insight V2 API.
/// </summary>
public readonly struct FraudCheckRequest : IVonageRequest
{
    /// <summary>
    ///     The insight types requested for this fraud check. Available values: "fraud_score" (deprecated) and "sim_swap".
    ///     At least one insight type must be specified.
    /// </summary>
    [JsonPropertyOrder(2)]
    public IEnumerable<string> Insights { get; internal init; }

    /// <summary>
    ///     The phone number to check for fraud risk, in E.164 format without a leading + or 00.
    ///     Start with the country code, e.g., 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber Phone { get; internal init; }

    /// <summary>
    ///     The type of identifier being checked. Always "phone" for phone number lookups.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "phone";

    /// <summary>
    ///     Creates a builder for constructing a <see cref="FraudCheckRequest"/>.
    /// </summary>
    /// <returns>A builder instance to configure the fraud check request.</returns>
    public static IBuilderForPhone Build() => new FraudCheckRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/v2/ni")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() => new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
        Encoding.UTF8, "application/json");
}