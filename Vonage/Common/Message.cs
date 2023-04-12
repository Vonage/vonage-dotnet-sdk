using Newtonsoft.Json;

namespace Vonage.Common;

public class Message
{
    [JsonProperty("type")]
    public string Type { get; set; }
        
    [JsonProperty("message-id")]
    public string MessageId { get; set; }
        
    [JsonProperty("account-id")]
    public string AccountId { get; set; }
        
    [JsonProperty("date-received")]
    public string DateReceived { get; set; }
        
    [JsonProperty("network")]
    public string Network { get; set; }
        
    [JsonProperty("from")]
    public string From { get; set; }
        
    [JsonProperty("to")]
    public string To { get; set; }
        
    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
        
    [JsonProperty("date-closed")]
    public string DateClosed { get; set; }
        
    [JsonProperty("latency")]
    public decimal Latency { get; set; }
        
    [JsonProperty("client-ref")]
    public string ClientRef { get; set; }
        
    [JsonProperty("status")]
    public string Status { get; set; }
        
    [JsonProperty("error-code")]
    public string ErrorCode { get; set; }
        
    [JsonProperty("error-code-label")]
    public string ErrorCodeLabel { get; set; }

    [JsonProperty("final-status")]
    public string FinalStatus { get; set; }
}