using System.IO;
using System.Net;

namespace Nexmo.Api.Request
{
    public class WebRequestAdapter : IHttpWebRequest
    {
        private readonly WebRequest _request;

        public WebRequestAdapter(WebRequest request)
        {
            _request = request;
        }

        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }
        public string ContentType
        {
            get { return _request.ContentType; }
            set { _request.ContentType = value; }
        }
        public long ContentLength
        {
            get { return _request.ContentLength; }
            set { _request.ContentLength = value; }
        }

        public IWebResponse GetResponse()
        {
            return new WebResponseAdapter(_request.GetResponse());
        }

        public Stream GetRequestStream()
        {
            return _request.GetRequestStream();
        }
    }
}