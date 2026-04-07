using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents the response from sending a two-factor authentication message via short code.
/// </summary>
public class TwoFactorAuthResponse
{
    /// <summary>
    ///     Gets or sets the number of messages sent in response to the request.
    /// </summary>
    [JsonProperty("message-count")]
    public string MessageCount { get; set; }

    /// <summary>
    ///     Gets or sets the array of message results containing delivery status for each recipient.
    /// </summary>
    public Message[] Messages { get; set; }
}