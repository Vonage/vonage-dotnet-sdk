using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.Voice
{
    public static partial class Call
    {
        public class StreamCommand
        {
            /// <summary>
            /// An array of URLs pointing to the .mp3 or .wav audio file to stream.
            /// </summary>
            public string[] stream_url { get; set; }
            /// <summary>
            /// Set to 0 to replay the audio file at stream_url when the stream ends.
            /// </summary>
            public int loop { get; set; }
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        public static CallCommandResponse BeginStream(string id, StreamCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/stream"), cmd);

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        public static CallCommandResponse EndStream(string id)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/stream"), new {});

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }
    }
}