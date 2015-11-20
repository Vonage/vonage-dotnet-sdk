using System.IO;

namespace Nexmo.Api.Request
{
    public interface IHttpWebRequest
    {
        string Method { get; set; }
        string ContentType { get; set; }
        long ContentLength { get; set; }
        IWebResponse GetResponse();
        Stream GetRequestStream();
    }
}