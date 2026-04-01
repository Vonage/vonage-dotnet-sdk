using Newtonsoft.Json;

namespace Vonage.Conversions;

/// <summary>
///     Represents a request to report a conversion event to the Vonage Conversion API.
///     A conversion occurs when a user completes a desired action after receiving a message or call.
/// </summary>
public class ConversionRequest
{
    /// <summary>
    ///     Indicates whether the user completed the desired action (conversion).
    ///     Set to <c>true</c> if the user replied to the message, answered the call,
    ///     or completed your call-to-action. Set to <c>false</c> otherwise.
    /// </summary>
    [JsonProperty("delivered", Order = 1)]
    public bool Delivered { get; set; }

    /// <summary>
    ///     The unique identifier of the message or call being reported on.
    ///     Use the appropriate ID based on the API that sent the original communication:
    ///     <list type="bullet">
    ///         <item><description>SMS API: use the <c>message-id</c> from the send response</description></item>
    ///         <item><description>Voice API: use the <c>call-id</c> from the call response</description></item>
    ///         <item><description>Verify API: use the <c>event_id</c> from the Verify Check response</description></item>
    ///     </list>
    /// </summary>
    [JsonProperty("message-id", Order = 0)]
    public string MessageId { get; set; }

    /// <summary>
    ///     The timestamp when the user completed the conversion action, in UTC format: <c>yyyy-MM-dd HH:mm:ss</c>.
    ///     For example: <c>2024-01-15 14:30:00</c>.
    ///     If not specified, Vonage uses the time the request is received.
    /// </summary>
    [JsonProperty("timestamp", Order = 2)]
    public string TimeStamp { get; set; }
}