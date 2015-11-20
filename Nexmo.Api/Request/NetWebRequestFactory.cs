using System;
using System.Net;

namespace Nexmo.Api.Request
{
    public class NetWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest CreateHttp(Uri uri)
        {
            return new WebRequestAdapter((HttpWebRequest) WebRequest.Create(uri));
        }
    }
}