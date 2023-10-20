using Newtonsoft.Json;

namespace Vonage.Conversions;

public class ConversionRequest
{
    /// <summary>
    /// Set to true if your user replied to the message you sent. Otherwise, set to false. Note: for curl, use 0 and 1.
    /// </summary>
    [JsonProperty("delivered", Order = 1)]
    public bool Delivered { get; set; }

    /// <summary>
    /// The ID you receive in the response to a request. * From the Verify API - use the event_id in the response to Verify Check. 
    /// * From the SMS API - use the message-id * From the Text-To-Speech API - use the call-id * From the Text-To-Speech-Prompt API - use the call-id
    /// </summary>
    [JsonProperty("message-id", Order = 0)]
    public string MessageId { get; set; }

    /// <summary>
    /// When the user completed your call-to-action (e.g. visited your website, installed your app) 
    /// in UTC±00:00 format: yyyy-MM-dd HH:mm:ss. If you do not set this parameter, Vonage uses the time it receives this request.
    /// </summary>
    [JsonProperty("timestamp", Order = 2)]
    public string TimeStamp { get; set; }
}