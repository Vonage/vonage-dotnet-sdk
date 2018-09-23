using System.Net;

namespace Nexmo.Api.Request
{
    public class NexmoResponse
    {
        public HttpStatusCode Status { get; set; }
        public string JsonResponse { get; set; }
    }

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

    public class Response<T> where T : class
    {
        public HALLinks _links { get; set; }
        public T _embedded { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
    }
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