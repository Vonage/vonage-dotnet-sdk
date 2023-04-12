using Newtonsoft.Json;

namespace Vonage.Verify;

public class VerifyControlRequest
{

    /// <summary>
    /// The request_id you received in the response to the Verify request.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    /// The possible commands are cancel to request cancellation of the verification process,
    /// or trigger_next_event to advance to the next verification event (if any). 
    /// Cancellation is only possible 30 seconds after the start of the verification request and 
    /// before the second event (either TTS or SMS) has taken place.
    /// must be one of 'cancel' or 'trigger_next_event'
    /// </summary>
    [JsonProperty("cmd")]
    public string Cmd { get; set; }
}