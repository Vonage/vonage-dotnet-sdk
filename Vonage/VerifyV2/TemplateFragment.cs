#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.VerifyV2.StartVerification;
#endregion

namespace Vonage.VerifyV2;

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