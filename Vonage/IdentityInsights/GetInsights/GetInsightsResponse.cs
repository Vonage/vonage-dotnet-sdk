#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.IdentityInsights.GetInsights;

/// <summary>
///     Represents the response from the Identity Insights API containing requested phone number insights.
/// </summary>
/// <param name="RequestId">The unique identifier for your request. This is an alphanumeric string up to 40 characters.</param>
/// <param name="Insights">
///     A map of objects representing the requested insight(s), where each key corresponds to the name
///     of the insight and the value contains the result and status of that insight.
/// </param>
public record GetInsightsResponse(
    [property: JsonPropertyName("request_id")]
    Guid RequestId,
    [property: JsonPropertyName("insights")]
    Insights Insights);

/// <summary>
///     A map of objects representing the requested insight(s), where each key corresponds to the name of the insight and
///     the value contains the result and status of that insight.
/// </summary>
/// <param name="Format">Validates the format of a phone number and provides information based on that format.</param>
/// <param name="SimSwap">
///     Information about any recent SIM pairing changes related to a mobile account. A recent SIM swap
///     may indicate a potential risk of account takeover.
/// </param>
/// <param name="OriginalCarrier">
///     Information about the network the number was initially assigned. Information based on the
///     numbering plan prefix.
/// </param>
/// <param name="CurrentCarrier">
///     Information about the network the number is currently assigned. This is applicable to
///     mobile numbers only.
/// </param>
/// <param name="LocationVerification">
///     Verify whether the location of a user device is within the area specified in the request.
///     Only present when requested.
/// </param>
/// <param name="SubscriberMatch">
///     Compare information associated with a mobile phone user against records verified by the
///     mobile operator in their KYC system. Only present when requested.
/// </param>
/// <param name="Roaming">
///     Check roaming status and country for a given device on a mobile network. Only present when
///     requested.
/// </param>
/// <param name="Reachability">
///     Check the connectivity status for a given device, including whether it is connected to the
///     network for data, SMS, or both. Only present when requested.
/// </param>
/// <param name="DeviceSwap">
///     Information about any recent physical device change for a mobile phone number. A recent
///     device swap may indicate a potential risk of account takeover. Only present when requested.
/// </param>
public record Insights(
    [property: JsonPropertyName("format")] FormatInsights Format,
    [property: JsonPropertyName("sim_swap")]
    SimSwapInsights SimSwap,
    [property: JsonPropertyName("original_carrier")]
    CarrierInsights OriginalCarrier,
    [property: JsonPropertyName("current_carrier")]
    CarrierInsights CurrentCarrier,
    [property: JsonPropertyName("location_verification")]
    LocationVerificationInsights LocationVerification = null,
    [property: JsonPropertyName("subscriber_match")]
    SubscriberMatchInsights SubscriberMatch = null,
    [property: JsonPropertyName("roaming")]
    RoamingInsights Roaming = null,
    [property: JsonPropertyName("reachability")]
    ReachabilityInsights Reachability = null,
    [property: JsonPropertyName("device_swap")]
    DeviceSwapInsights DeviceSwap = null);

/// <summary>
///     Validates the format of a phone number and provides information based on that format.
/// </summary>
/// <param name="CountryCode">Two character country code for phone_number. This is in ISO 3166-1 alpha-2 format.</param>
/// <param name="CountryName">The full name of the country where the phone_number is registered.</param>
/// <param name="CountryPrefix">The numeric prefix for the country where the phone_number is registered.</param>
/// <param name="OfflineLocation">
///     The location where the number was originally assigned, based on its prefix. This does not
///     represent the real-time location of the device. The value indicates the country of origin or, when available, the
///     specific geographical area associated with the number. Only landline and mobile numbers are eligible for offline
///     location data.
/// </param>
/// <param name="TimeZones">
///     List of time zones corresponding to the format.offline_location field, or a single-element list
///     with the default "unknown" time zone if no other time zone was found or if the number is invalid. Time zone values
///     follow the tz database identifiers.
/// </param>
/// <param name="NumberInternational">The phone_number from your request, formatted in international E.164 format.</param>
/// <param name="NumberNational">
///     The phone_number from your request, formatted according to the local convention of the
///     country it belongs to.
/// </param>
/// <param name="IsFormatValid">
///     Phone number format validation involves verifying the length and prefix details at various
///     levels to ensure accuracy and compliance with global numbering standards. A valid format means the number can be
///     legitimately assigned by carriers to users. However, it does not guarantee that the number is currently assigned to
///     a carrier or that it is reachable.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record FormatInsights(
    [property: JsonPropertyName("country_code")]
    string CountryCode,
    [property: JsonPropertyName("country_name")]
    string CountryName,
    [property: JsonPropertyName("country_prefix")]
    string CountryPrefix,
    [property: JsonPropertyName("offline_location")]
    string OfflineLocation,
    [property: JsonPropertyName("time_zones")]
    string[] TimeZones,
    [property: JsonPropertyName("number_international")]
    string NumberInternational,
    [property: JsonPropertyName("number_national")]
    string NumberNational,
    [property: JsonPropertyName("is_format_valid")]
    bool IsFormatValid,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Indicates the status of the information returned for the specified phone number.
/// </summary>
/// <param name="Code">Code indicating the status of the request. </param>
/// <param name="Message">More detailed status description.</param>
public record InsightStatus(
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("message")]
    string Message);

/// <summary>
///     Information about any recent SIM pairing changes related to a mobile account. A recent SIM swap may indicate a
///     potential risk of account takeover.
/// </summary>
/// <param name="LatestSimSwap">Date and time in UTC ISO 8601 of latest SIM swap performed.</param>
/// <param name="IsSwapped">Indicates whether the SIM card has been swapped during the period.</param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record SimSwapInsights(
    [property: JsonPropertyName("latest_sim_swap_at")]
    DateTimeOffset LatestSimSwap,
    [property: JsonPropertyName("is_swapped")]
    bool IsSwapped,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Information about the carrier associated with a phone number.
/// </summary>
/// <param name="Name">
///     The full name of the carrier associated with the phone number. This is applicable to mobile numbers
///     only.
/// </param>
/// <param name="NetworkType">
///     The type of network of the carrier associated with that phone_number. This enum is
///     extensible; clients must handle unknown values.
/// </param>
/// <param name="CountryCode">The country that phone_number is associated with. This is in ISO 3166-1 alpha-2 format.</param>
/// <param name="NetworkCode">
///     Mobile country codes (MCC) + Mobile network codes (MNC). E.212 International mobile
///     subscriber identity.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record CarrierInsights(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("network_type")]
    string NetworkType,
    [property: JsonPropertyName("country_code")]
    string CountryCode,
    [property: JsonPropertyName("network_code")]
    string NetworkCode,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Verifies whether the location of a user device is within the area specified in the request.
/// </summary>
/// <param name="IsVerified">
///     Result of the verification request:
///     <list type="bullet">
///         <item><description><c>TRUE</c>: the network locates the device within the requested area.</description></item>
///         <item><description><c>FALSE</c>: the requested area does not match where the network locates the device.</description></item>
///         <item><description><c>UNKNOWN</c>: the network cannot locate the device.</description></item>
///         <item>
///             <description>
///                 <c>PARTIAL</c>: the requested area partially matches where the network locates the device; a
///                 <see cref="MatchRate" /> may be included.
///             </description>
///         </item>
///     </list>
///     This enum is extensible; clients must handle unknown values.
/// </param>
/// <param name="LatestLocationAt">Date and time in UTC ISO 8601 of the last known location.</param>
/// <param name="MatchRate">
///     Estimation of the match rate between the requested area (R) and the area where the network
///     locates the device (N), expressed as a percentage: (R ∩ N) / N × 100. Included only when
///     <see cref="IsVerified" /> is <c>PARTIAL</c>. Value is between 1 and 99.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record LocationVerificationInsights(
    [property: JsonPropertyName("is_verified")]
    string IsVerified,
    [property: JsonPropertyName("latest_location_at")]
    DateTimeOffset LatestLocationAt,
    [property: JsonPropertyName("match_rate")]
    int? MatchRate,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Compares information associated with a mobile phone user against records verified by the mobile
///     operator in their KYC system. All match fields use extensible enum strings; clients must handle
///     unknown values.
/// </summary>
/// <param name="IdDocumentMatch">
///     Indicates whether the ID document number of the customer matches the one on the operator's
///     system. Possible values: <c>EXACT</c>, <c>HIGH</c>, <c>PARTIAL</c>, <c>LOW</c>, <c>NONE</c>,
///     <c>DATA_UNAVAILABLE</c>.
/// </param>
/// <param name="GivenNameMatch">
///     Indicates whether the first/given name of the customer matches the one on the operator's system.
/// </param>
/// <param name="FamilyNameMatch">
///     Indicates whether the last/family name of the customer matches the one on the operator's system.
/// </param>
/// <param name="AddressMatch">
///     Indicates whether the complete address of the customer matches the one on the operator's system.
/// </param>
/// <param name="StreetNameMatch">
///     Indicates whether the street name of the customer matches the one on the operator's system.
///     Possible values: <c>EXACT</c>, <c>HIGH</c>, <c>PARTIAL</c>, <c>LOW</c>, <c>NONE</c>,
///     <c>DATA_UNAVAILABLE</c>, <c>INCLUDED_WITH_ADDRESS_MATCH</c>.
/// </param>
/// <param name="StreetNumberMatch">
///     Indicates whether the street number of the customer matches the one on the operator's system.
/// </param>
/// <param name="PostalCodeMatch">
///     Indicates whether the postal/zip code of the customer matches the one on the operator's system.
/// </param>
/// <param name="LocalityMatch">
///     Indicates whether the locality of the customer's address matches the one on the operator's
///     system.
/// </param>
/// <param name="RegionMatch">
///     Indicates whether the region or prefecture of the customer matches the one on the operator's
///     system.
/// </param>
/// <param name="CountryMatch">
///     Indicates whether the country of the customer's address matches the one on the operator's system.
/// </param>
/// <param name="HouseNumberExtensionMatch">
///     Indicates whether the house number extension of the customer's address matches the one on the
///     operator's system.
/// </param>
/// <param name="BirthdateMatch">
///     Indicates whether the birthdate of the customer matches the one on the operator's system.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record SubscriberMatchInsights(
    [property: JsonPropertyName("id_document_match")]
    string IdDocumentMatch,
    [property: JsonPropertyName("given_name_match")]
    string GivenNameMatch,
    [property: JsonPropertyName("family_name_match")]
    string FamilyNameMatch,
    [property: JsonPropertyName("address_match")]
    string AddressMatch,
    [property: JsonPropertyName("street_name_match")]
    string StreetNameMatch,
    [property: JsonPropertyName("street_number_match")]
    string StreetNumberMatch,
    [property: JsonPropertyName("postal_code_match")]
    string PostalCodeMatch,
    [property: JsonPropertyName("locality_match")]
    string LocalityMatch,
    [property: JsonPropertyName("region_match")]
    string RegionMatch,
    [property: JsonPropertyName("country_match")]
    string CountryMatch,
    [property: JsonPropertyName("house_number_extension_match")]
    string HouseNumberExtensionMatch,
    [property: JsonPropertyName("birthdate_match")]
    string BirthdateMatch,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Checks roaming status and country for a given device on a mobile network.
/// </summary>
/// <param name="LatestStatusAt">
///     Date and time in UTC ISO 8601 of the last time the associated device roaming status was
///     updated.
/// </param>
/// <param name="IsRoaming">Roaming status. <c>true</c> if the device is roaming.</param>
/// <param name="CountryCodes">
///     An array of country codes in ISO 3166-1 alpha-2 format representing where the phone number is
///     roaming. Usually contains one element; in edge cases where the roaming network is associated with
///     multiple countries, additional country codes are included.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record RoamingInsights(
    [property: JsonPropertyName("latest_status_at")]
    DateTimeOffset LatestStatusAt,
    [property: JsonPropertyName("is_roaming")]
    bool IsRoaming,
    [property: JsonPropertyName("country_codes")]
    string[] CountryCodes,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Checks the connectivity status for a given device, including whether it is connected to the
///     network for data, SMS, or both.
/// </summary>
/// <param name="LatestStatusAt">
///     Date and time in UTC ISO 8601 of the last time the associated device connectivity status was
///     updated.
/// </param>
/// <param name="IsReachable">
///     Indicates overall device reachability. <c>true</c> if the device is connected to the network.
/// </param>
/// <param name="Connectivity">
///     Indicates if the device is connected to the network for <c>DATA</c> or <c>SMS</c> usage. Only
///     returned when <see cref="IsReachable" /> is <c>true</c>. This enum is extensible; clients must
///     handle unknown values.
/// </param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record ReachabilityInsights(
    [property: JsonPropertyName("latest_status_at")]
    DateTimeOffset LatestStatusAt,
    [property: JsonPropertyName("is_reachable")]
    bool IsReachable,
    [property: JsonPropertyName("connectivity")]
    string[] Connectivity,
    [property: JsonPropertyName("status")] InsightStatus Status);

/// <summary>
///     Information about any recent physical device change for a mobile phone number. A recent device
///     swap may indicate a potential risk of account takeover.
/// </summary>
/// <param name="LatestDeviceSwap">Date and time in UTC ISO 8601 of the latest device swap performed.</param>
/// <param name="IsSwapped">Indicates whether the device has been swapped during the period.</param>
/// <param name="Status">Indicates the status of the information returned for the specified phone number.</param>
public record DeviceSwapInsights(
    [property: JsonPropertyName("latest_device_swap_at")]
    DateTimeOffset LatestDeviceSwap,
    [property: JsonPropertyName("is_swapped")]
    bool IsSwapped,
    [property: JsonPropertyName("status")] InsightStatus Status);