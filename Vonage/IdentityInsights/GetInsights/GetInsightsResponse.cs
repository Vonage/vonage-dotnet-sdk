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
public record Insights(
    [property: JsonPropertyName("format")] FormatInsights Format,
    [property: JsonPropertyName("sim_swap")]
    SimSwapInsights SimSwap,
    [property: JsonPropertyName("original_carrier")]
    CarrierInsights OriginalCarrier,
    [property: JsonPropertyName("current_carrier")]
    CarrierInsights CurrentCarrier);

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