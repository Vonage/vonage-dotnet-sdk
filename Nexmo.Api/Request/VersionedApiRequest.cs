using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace Nexmo.Api.Request
{
    internal static class VersionedApiRequest
    {
        private static StringBuilder GetQueryStringBuilderFor(object parameters)
        {
            var apiParams = ApiRequest.GetParameters(parameters);

            var sb = new StringBuilder();
            foreach (var key in apiParams.Keys)
            {
                sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(key), WebUtility.UrlEncode(apiParams[key]));
            }
            return sb;
        }

        private static string DoRequest(Uri uri)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            // attempt bearer token auth
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"], Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"]));

            var sendTask = Configuration.Instance.Client.SendAsync(req);
            sendTask.Wait();

            //if (!sendTask.Result.IsSuccessStatusCode)
            //    throw new Exception("Error while retrieving resource.");

            string json;
            var readTask = sendTask.Result.Content.ReadAsStreamAsync();
            readTask.Wait();
            using (var sr = new StreamReader(readTask.Result))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }

        public static NexmoResponse DoRequest(string method, Uri uri, object payload)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            // attempt bearer token auth
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"], Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"]));

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var sendTask = Configuration.Instance.Client.SendAsync(req);
            sendTask.Wait();

            if (!sendTask.Result.IsSuccessStatusCode)
            {
                return new NexmoResponse
                {
                    Status = sendTask.Result.StatusCode
                };
            }

            string json;
            var readTask = sendTask.Result.Content.ReadAsStreamAsync();
            readTask.Wait();
            using (var sr = new StreamReader(readTask.Result))
            {
                json = sr.ReadToEnd();
            }
            return new NexmoResponse
            {
                Status = sendTask.Result.StatusCode,
                JsonResponse = json
            };
        }

        public static string DoRequest(Uri uri, object parameters)
        {
            var sb = GetQueryStringBuilderFor(parameters);

            return DoRequest(new Uri(uri, "?" + sb));
        }
    }
}
