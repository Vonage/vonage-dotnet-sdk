using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyRequestBase
    {
        /// <summary>
        /// The mobile or landline phone number to verify. Unless you are setting country explicitly, this number must be in E.164 format.
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// If you do not provide number in international format or you are not sure if number is correctly formatted, specify the two-character country code in country. Verify will then format the number for you.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }                

        /// <summary>
        /// The length of the verification code. Must be 4 or 6
        /// </summary>
        [JsonProperty("code_length")]
        public int CodeLength { get; set; }

        /// <summary>
        /// By default, the SMS or text-to-speech (TTS) message is generated in the locale that matches the number. 
        /// For example, the text message or TTS message for a 33* number is sent in French. 
        /// Use this parameter to explicitly control the language used for the Verify request. 
        /// A list of languages is available: https://developer.nexmo.com/verify/guides/verify-languages
        /// </summary>
        [JsonProperty("lg")]
        public string Lg { get; set; }

        /// <summary>
        /// How long the generated verification code is valid for, in seconds. 
        /// When you specify both pin_expiry and next_event_wait then pin_expiry must be an integer 
        /// multiple of next_event_wait otherwise pin_expiry is defaulted to equal next_event_wait. 
        /// See changing the event timings: https://developer.nexmo.com/verify/guides/changing-default-timings.
        /// </summary>
        [JsonProperty("pin_expiry")]
        public int PinExpiry { get; set; }

        /// <summary>
        /// Specifies the wait time in seconds between attempts to deliver the verification code.
        /// Must be between 60 and 900
        /// </summary>
        [JsonProperty("next_event_wait")]
        public int NextEventWait { get; set; }        
    }
}
