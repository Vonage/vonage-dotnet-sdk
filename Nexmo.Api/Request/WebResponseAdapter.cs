using System.IO;
using System.Net;

namespace Nexmo.Api.Request
{
    public class WebResponseAdapter : IWebResponse
    {
        private readonly WebResponse _response;

        public WebResponseAdapter(WebResponse response)
        {
            _response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}