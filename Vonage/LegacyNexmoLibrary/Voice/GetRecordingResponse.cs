using System.Net;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.GetRecordingResponse class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.GetRecordingResponse class.")]
    public class GetRecordingResponse
    {
        /// <summary>
        /// Response Status of the HTTP Request
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Stream of bytes containg the recording file's content
        /// </summary>
        public byte[] ResultStream { get; set; }
    }
}
