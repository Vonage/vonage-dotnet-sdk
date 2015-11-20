using System;

namespace Nexmo.Api.Request
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest CreateHttp(Uri uri);
    }
}
