using System.IO;
using System.Net;

namespace Nexmo.Api.Request
{
    public interface IWebResponse
    {
        Stream GetResponseStream();
        HttpStatusCode GetResponseStatusCode();
    }
}