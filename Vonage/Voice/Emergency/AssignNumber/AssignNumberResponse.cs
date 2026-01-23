#region
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Voice.Emergency.AssignNumber;

public record AssignNumberResponse(
    [property: JsonPropertyName("number")]
    [property: JsonConverter(typeof(PhoneNumberJsonConverter))]
    PhoneNumber Number,
    [property: JsonPropertyName("contact_name")]
    string ContactName,
    [property: JsonPropertyName("address")]
    Address Address);