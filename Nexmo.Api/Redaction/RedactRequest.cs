using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Redaction
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedactionType
    {
        [EnumMember(Value = "inbound")]
        Inbound,
        [EnumMember(Value = "outbound")]
        Outbound
    }
    public class RedactRequest
    {
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("product")]
        public string Product { get; set; }
        
        [JsonProperty("type")]
        public RedactionType Type { get; set; }
    }
}