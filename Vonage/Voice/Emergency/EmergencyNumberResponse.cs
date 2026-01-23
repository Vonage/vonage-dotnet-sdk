#region
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
/// </summary>
/// <param name="Number"></param>
/// <param name="ContactName"></param>
/// <param name="Address"></param>
public record EmergencyNumberResponse(
    [property: JsonPropertyName("number")]
    [property: JsonConverter(typeof(PhoneNumberJsonConverter))]
    PhoneNumber Number,
    [property: JsonPropertyName("contact_name")]
    string ContactName,
    [property: JsonPropertyName("address")]
    Address Address);