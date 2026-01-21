#region
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
/// </summary>
/// <param name="Id">Vonage Address unique ID</param>
/// <param name="Name">A friendly name to identify the address</param>
/// <param name="Line1">First line of the full address</param>
/// <param name="Line2">Second line of the full address</param>
/// <param name="City">The city where this address is located</param>
/// <param name="Region">The state or region of the address</param>
/// <param name="Type">Type of address</param>
/// <param name="LocationType">Address location type as a business or domestic</param>
/// <param name="PostalCode">The Postal Code of this address</param>
/// <param name="Country">The two character country code in ISO 3166-1 alpha-2 format</param>
public record Address(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("address_name")]
    string Name,
    [property: JsonPropertyName("address_line1")]
    string Line1,
    [property: JsonPropertyName("address_line2")]
    string Line2,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("region")] string Region,
    [property: JsonPropertyName("type")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<Address.AddressType>))]
    Address.AddressType Type,
    [property: JsonPropertyName("address_location_type")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<Address.AddressLocationType>))]
    Address.AddressLocationType LocationType,
    [property: JsonPropertyName("postal_code")]
    string PostalCode,
    [property: JsonPropertyName("country")]
    string Country)
{
    /// <summary>
    /// </summary>
    public enum AddressLocationType
    {
        /// <summary>
        /// </summary>
        [Description("business")] Business,

        /// <summary>
        /// </summary>
        [Description("residential")] Residential,
    }

    /// <summary>
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// </summary>
        [Description("emergency")] Emergency,
    }
}