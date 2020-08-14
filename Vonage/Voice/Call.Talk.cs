using Newtonsoft.Json;
using Vonage.Request;

namespace Vonage.Voice
{
    public static partial class Call
    {
        public class TalkCommand
        {
            /// <summary>
            /// A UTF-8 and URL encoded string of up to 1500 characters containing
            /// the message to be synthesized in the Call or Conversation. Each
            /// comma in text adds a short pause to the synthesized speech.
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// The name of the voice used to deliver text. You use the voice_name
            /// that has the correct language, gender and accent for the message
            /// you are sending. For example, the default voice kimberley is a
            /// female who speaks English with an American accent (en-US).
            /// Possible values for voice_name are listed at https://developer.nexmo.com/api/voice#startTalk
            /// </summary>
            public string voice_name { get; set; }
            /// <summary>
            /// Set to 0 to replay the audio file at stream_url when the stream ends. The default value is 1.
            /// </summary>
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore)]
            public int? loop { get; set; }
        }

        public class DtmfCommand
        {
            /// <summary>
            /// The array of digits to send to the call
            /// </summary>
            public string digits { get; set; }
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallCommandResponse BeginTalk(string id, TalkCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/talk"), cmd, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallCommandResponse EndTalk(string id, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>("DELETE", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/talk"), new {}, ApiRequest.AuthType.Bearer, creds);            
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallCommandResponse SendDtmf(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/dtmf"), cmd, ApiRequest.AuthType.Bearer, creds);
        }
    }
}