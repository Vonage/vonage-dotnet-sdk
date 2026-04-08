#region
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
///     Represents the response from an emergency number API operation, containing the number details and its assigned address.
/// </summary>
/// <param name="Number">The phone number in E.164 format.</param>
/// <param name="ContactName">The contact name associated with this emergency number.</param>
/// <param name="Address">The emergency address assigned to this number.</param>
public record EmergencyNumberResponse(
    [property: JsonPropertyName("number")]
    [property: JsonConverter(typeof(PhoneNumberJsonConverter))]
    PhoneNumber Number,
    [property: JsonPropertyName("contact_name")]
    string ContactName,
    [property: JsonPropertyName("address")]
    Address Address);