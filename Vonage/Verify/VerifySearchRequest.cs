using Newtonsoft.Json;

namespace Vonage.Verify;

public class VerifySearchRequest
{
    /// <summary>
    /// The request_id you received in the Verify Request Response.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }
}