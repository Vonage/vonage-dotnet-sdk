using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class Call
    {
        public class Endpoint
        {
            public string type { get; set; }
            public string number { get; set; }
        }
        public class CallCommand
        {
            public Endpoint[] to { get; set; }
            public Endpoint from { get; set; }
            public string[] answer_url { get; set; }
        }

        public class CallResponse
        {
            
        }

        public static CallResponse Do(CallCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(Call), "/v1/calls"), cmd);

            return JsonConvert.DeserializeObject<CallResponse>(response.JsonResponse);
        }
    }
}