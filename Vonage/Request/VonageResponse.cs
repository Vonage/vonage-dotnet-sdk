using Newtonsoft.Json;
using System.Net;

namespace Vonage.Request;

public class VonageResponse
{
    public HttpStatusCode Status { get; set; }
    public string JsonResponse { get; set; }
}