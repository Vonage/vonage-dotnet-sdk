using System.Net;

namespace Vonage.Request;

public class VonageResponse
{
    public string JsonResponse { get; set; }
    public HttpStatusCode Status { get; set; }
}