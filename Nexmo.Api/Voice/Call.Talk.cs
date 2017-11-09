using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.Voice
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
            /// Possible values for voice_name are listed at https://docs.nexmo.com/voice/voice-api/api-reference#talk_put
            /// </summary>
            public string voice_name { get; set; }
            /// <summary>
            /// Set to 0 to replay the audio file at stream_url when the stream ends. The default value is 1.
            /// </summary>
            public int loop { get; set; }
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
        public static CallCommandResponse BeginTalk(string id, TalkCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/talk"), cmd);

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        public static CallCommandResponse EndTalk(string id)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/talk"), new {});

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        public static CallCommandResponse SendDtmf(string id, DtmfCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/dtmf"), cmd);

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }
    }
}