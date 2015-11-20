using System.IO;

namespace Nexmo.Api.Request
{
    public interface IWebResponse
    {
        Stream GetResponseStream();
    }
}