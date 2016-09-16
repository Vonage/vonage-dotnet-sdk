using System.Net;

namespace Nexmo.Api.Request
{
    public class NexmoResponse
    {
        public HttpStatusCode Status { get; set; }
        public string JsonResponse { get; set; }
    }
}