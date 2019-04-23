using Nexmo.Api.Request;
using System;
using System.Net.Http;

namespace Nexmo.Api.Voice
{
    public class Recording
    {
        public static object GetRecording(Uri recordingUrl, Credentials credentials = null)
        {
            if (recordingUrl == null)
            {
                throw new ArgumentNullException(nameof(recordingUrl));
            }

            var response = VersionedApiRequest.DoRequest("GET", recordingUrl), credentials);


            return JsonConvert.DeserializeObject<CallResponse>(response.JsonResponse);

            }

            return null; 
        }
    }
}
