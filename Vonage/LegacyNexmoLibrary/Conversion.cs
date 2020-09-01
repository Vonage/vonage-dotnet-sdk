using Newtonsoft.Json;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api
{
    [System.Obsolete("The Nexmo.Api.Conversion class is obsolete.")]
    public static class Conversion
    {
        /// <summary>
        /// possible values : "voice" or "sms"
        /// </summary>
        public static string ConversionType { get; set; }

        public class ConversionRequest
        {
            /// <summary>
            /// The ID you receive in the response to a request.
            /// possible values: message-id for SMS API, call-id for TTS and TTSP APIs, 
            /// event_id for Verify API.
            /// </summary>
            [JsonProperty("message-id")]
            public string MessageId { get; set; }
            /// <summary>
            /// Set to true if your user replied to the message you sent. 
            /// Otherwise, set to false. 
            /// </summary>
            [JsonProperty("delivered")]
            public bool Delivered { get; set; }
            /// <summary>
            /// When the user completed your call-to-action in UTC±00:00 
            /// format: yyyy-MM-dd HH:mm:ss.
            /// </summary>
            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }
        }
        public class ConversionResult
        {

        }
        /// <summary>
        /// Tells if a message or call was successful.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        public static void SubmitConversion (ConversionRequest request, Credentials creds = null)
        {
            ApiRequest.DoPostRequestUrlContentFromObject<ConversionResult>(new Uri($"https://api.nexmo.com/conversions/{ConversionType}"), request, creds);
        }
    }
}
