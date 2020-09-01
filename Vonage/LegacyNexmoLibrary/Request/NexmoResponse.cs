using System.Net;

namespace Nexmo.Api.Request
{
    [System.Obsolete("The Nexmo.Api.Request.NexmoResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Request.NexmoResponse class.")]
    public class NexmoResponse
    {
        public HttpStatusCode Status { get; set; }
        public string JsonResponse { get; set; }
    }

    [System.Obsolete("The Nexmo.Api.Request.Response class is obsolete.")]
    public class PaginatedResponse<T> where T : class
    {
        public int count { get; set; }
        public int page_size { get; set; }
        public int page_index { get; set; }
        public HALLinks _links { get; set; }
        public T _embedded { get; set; }
    }

    ////////
    // TODO: Handle HAL better
    [System.Obsolete("The Nexmo.Api.Request.Response class is obsolete.")]
    public class Response<T> where T : class
    {
        public HALLinks _links { get; set; }
        public T _embedded { get; set; }
    }

    [System.Obsolete("The Nexmo.Api.Request.Response class is obsolete.")]
    public class Link
    {
        public string href { get; set; }
    }
    [System.Obsolete("The Nexmo.Api.Request.Response class is obsolete.")]
    public class HALLinks
    {
        public Link self { get; set; }
        public Link next { get; set; }
        public Link prev { get; set; }
        public Link first { get; set; }
        public Link last { get; set; }
    }

    ////////
}