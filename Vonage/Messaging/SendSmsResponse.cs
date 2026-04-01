using Newtonsoft.Json;

namespace Vonage.Messaging;

/// <summary>
///     Represents the response from sending an SMS message via the Vonage SMS API.
/// </summary>
public class SendSmsResponse
{
    /// <summary>
    ///     The number of messages generated from the request.
    ///     Long messages may be split into multiple parts.
    /// </summary>
    [JsonProperty("message-count")]
    public string MessageCount { get; set; }

    /// <summary>
    ///     The array of message results, one for each message part sent.
    ///     Check <see cref="SmsResponseMessage.Status"/> to verify delivery status.
    /// </summary>
    public SmsResponseMessage[] Messages { get; set; }
}