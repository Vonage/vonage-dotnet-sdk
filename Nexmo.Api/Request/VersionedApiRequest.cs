using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;

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
            SetUserAgent(ref req);
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

        private static string _userAgent;
        public static void SetUserAgent(ref HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(_userAgent))
            {
#if NETSTANDARD1_6
                // TODO: watch the next core release; may have functionality to make this cleaner
                var runtimeVersion = (System.Runtime.InteropServices.RuntimeInformation.OSDescription + System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription)
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("_", "")
                    ;
#else
                var runtimeVersion = System.Diagnostics.FileVersionInfo
                    .GetVersionInfo(typeof(int).Assembly.Location)
                    .ProductVersion;
#endif
                var libraryVersion = typeof(VersionedApiRequest)
                    .GetTypeInfo()
                    .Assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;

                _userAgent = $"nexmo-dotnet/{libraryVersion} dotnet/{runtimeVersion}";

                var appVersion = Configuration.Instance.Settings["appSettings:Nexmo.UserAgent"];
                if (!string.IsNullOrWhiteSpace(appVersion))
                {
                    _userAgent += $" {appVersion}";
                }
            }

            request.Headers.UserAgent.ParseAdd(_userAgent);
        }

        public static NexmoResponse DoRequest(string method, Uri uri, object payload)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            SetUserAgent(ref req);
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
