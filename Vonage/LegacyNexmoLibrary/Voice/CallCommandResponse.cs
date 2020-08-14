using Newtonsoft.Json;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.CallCommandResponse class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.CallCommandResponse class.")]
    public class CallCommandResponse
    {
        /// <summary>
        /// A string explaining the state of this request.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        
        /// <summary>
        /// The unique id for this request.
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}
