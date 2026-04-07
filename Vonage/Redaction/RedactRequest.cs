using Newtonsoft.Json;

namespace Vonage.Redaction;

/// <summary>
///     Represents a request to redact personal data from a specific transaction.
/// </summary>
public class RedactRequest
{
    /// <summary>
    ///     Gets or sets the transaction ID to redact. This is the unique identifier of the record to be redacted (e.g., an
    ///     SMS message ID or call UUID).
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Gets or sets the product that the transaction ID relates to. See <see cref="RedactionProduct"/> for available
    ///     products.
    /// </summary>
    [JsonProperty("product")]
    public RedactionProduct? Product { get; set; }

    /// <summary>
    ///     Gets or sets the type of transaction. Required when redacting SMS data to specify whether it was an inbound or
    ///     outbound message. See <see cref="RedactionType"/>.
    /// </summary>
    [JsonProperty("type")]
    public RedactionType? Type { get; set; }
}