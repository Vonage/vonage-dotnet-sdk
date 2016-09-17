using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Nexmo.Api.Request
{
    internal static class VersionedApiRequest
    {
        private static IHttpWebRequestFactory _webRequestFactory = new NetWebRequestFactory();
        public static IHttpWebRequestFactory WebRequestFactory
        {
            get
            {
                return _webRequestFactory;
            }
            set { _webRequestFactory = value; }
        }

        public static NexmoResponse DoRequest(string method, Uri uri, object payload)
        {
            var req = _webRequestFactory.CreateHttp(uri);
            // attempt bearer token auth
            req.SetBearerToken(Jwt.CreateToken(ConfigurationManager.AppSettings["Nexmo.Application.Id"], ConfigurationManager.AppSettings["Nexmo.Application.Key"]));

            req.Method = method;
            req.ContentType = "application/json";
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            req.ContentLength = data.Length;
            var requestStream = req.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            try
            {
                var resp = req.GetResponse();
                string json;
                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }
                return new NexmoResponse
                {
                    Status = resp.GetResponseStatusCode(),
                    JsonResponse = json
                };
            }
            catch (WebException ex)
            {
                return new NexmoResponse
                {
                    Status = ((HttpWebResponse)ex.Response).StatusCode
                };
            }
        }
    }
}
