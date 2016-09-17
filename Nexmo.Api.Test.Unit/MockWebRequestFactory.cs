using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Nexmo.Api.Request;

namespace Nexmo.Api.Test.Unit
{
    public class MockWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest CreateHttp(Uri uri)
        {
            Debug.WriteLine(uri);
            return new LoggingHttpWebRequestAdapter((HttpWebRequest)WebRequest.Create(uri));
        }
    }

    public class LoggingHttpWebRequestAdapter : IHttpWebRequest
    {
        private HttpWebRequest _request;

        public LoggingHttpWebRequestAdapter(HttpWebRequest request)
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

        public void SetBearerToken(string token)
        {
            _request.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
