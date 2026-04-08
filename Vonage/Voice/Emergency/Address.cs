#region
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
///     Represents a physical address registered for emergency calling services.
/// </summary>
/// <param name="Id">The unique identifier assigned to this address by Vonage.</param>
/// <param name="Name">A friendly name to identify the address (e.g., "Office HQ").</param>
/// <param name="Line1">The first line of the street address.</param>
/// <param name="Line2">The second line of the street address (e.g., suite or apartment number).</param>
/// <param name="City">The city where this address is located.</param>
/// <param name="Region">The state, province, or region of the address.</param>
/// <param name="Type">The type of address (currently only "emergency").</param>
/// <param name="LocationType">Whether the address is a business or residential location.</param>
/// <param name="PostalCode">The postal or ZIP code of the address.</param>
/// <param name="Country">The two-character country code in ISO 3166-1 alpha-2 format (e.g., "US", "GB").</param>
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
    ///     Defines whether the address is a business or residential location.
    /// </summary>
    public enum AddressLocationType
    {
        /// <summary>
        ///     A business or commercial address.
        /// </summary>
        [Description("business")] Business,

        /// <summary>
        ///     A residential or domestic address.
        /// </summary>
        [Description("residential")] Residential,
    }

    /// <summary>
    ///     Defines the type of address registered with Vonage.
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        ///     An address registered for emergency calling services.
        /// </summary>
        [Description("emergency")] Emergency,
    }
}