using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
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
