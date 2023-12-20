using System;
using System.Text.Json.Serialization;

namespace Vonage.Conversations;

/// <summary>
///     Represents the Callback parameters.
/// </summary>
/// <param name="ApplicationId">The Vonage Application Id.</param>
/// <param name="NccoUrl">The NCCO Url.</param>
public record CallbackParameters(
    [property: JsonPropertyName("applicationId")]
    [property: JsonPropertyOrder(0)]
    string ApplicationId,
    [property: JsonPropertyName("ncco_url")]
    [property: JsonPropertyOrder(1)]
    Uri NccoUrl);