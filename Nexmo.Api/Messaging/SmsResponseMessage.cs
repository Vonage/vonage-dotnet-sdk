using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Messaging
{
    public class SmsResponseMessage
    {
        
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("message-id")]
        public string MessageId { get; set; }

        [JsonProperty("error-text")]
        public string ErrorText { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("remaining-balance")]
        public string RemainingBalance { get; set; }

        [JsonProperty("message-price")]
        public string MessagePrice { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("account-ref")]
        public string AccountRef { get; set; }

        [JsonIgnore]
        public SmsStatusCode StatusCode 
        { 
            get
            {
                return (SmsStatusCode)Enum.Parse(typeof(SmsStatusCode), Status);
            } 
        }
    }
}
