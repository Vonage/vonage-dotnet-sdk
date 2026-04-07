using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents a request to control an in-progress verification by cancelling it or triggering the next event.
/// </summary>
public class VerifyControlRequest
{
    /// <summary>
    ///     The unique identifier of the verification request to control. This is the <see cref="VerifyResponse.RequestId"/> received from <see cref="IVerifyClient.VerifyRequestAsync"/>.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    ///     The control command to execute. Must be either "cancel" to abort the verification, or "trigger_next_event" to immediately advance to the next delivery attempt. Cancellation is only available 30 seconds after the request started and before the second event has occurred.
    /// </summary>
    [JsonProperty("cmd")]
    public string Cmd { get; set; }
}