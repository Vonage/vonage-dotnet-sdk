#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.VerifyV2.StartVerification;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Represents a template fragment containing the message text for a specific channel and locale. Fragments define the actual content sent to users during verification.
/// </summary>
/// <param name="TemplateFragmentId">The unique identifier (UUID) of the template fragment.</param>
/// <param name="Channel">The verification channel this fragment applies to (SMS or Voice).</param>
/// <param name="Locale">The IETF BCP 47 locale code this fragment applies to (e.g., "en-us").</param>
/// <param name="Text">The message text with optional placeholders: ${code}, ${brand}, ${time-limit}, and ${time-limit-unit}.</param>
/// <param name="CreatedOn">The timestamp when this fragment was created.</param>
/// <param name="UpdatedOn">The timestamp when this fragment was last modified.</param>
public record TemplateFragment(
    [property: JsonPropertyName("template_fragment_id")]
    Guid TemplateFragmentId,
    [property: JsonPropertyName("channel")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<VerificationChannel>))]
    VerificationChannel Channel,
    [property: JsonPropertyName("locale")] Locale Locale,
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("date_created")]
    DateTimeOffset CreatedOn,
    [property: JsonPropertyName("date_updated")]
    DateTimeOffset UpdatedOn);