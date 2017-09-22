using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Nexmo.Api.Request
{
    /// <summary>
    /// Responsible for sending all Nexmo API requests that require Application authentication.
    /// For older forms of authentication, see ApiRequest.
    /// </summary>
    public static class VersionedApiRequest
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

        private static string DoRequest(Uri uri, Credentials creds = null)
        {
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            SetUserAgent(ref req);
            // attempt bearer token auth
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));

            using (Configuration.Instance.ApiLogger.BeginScope("VersionedApiRequest.DoRequest {0}", uri.GetHashCode()))
            {
                Configuration.Instance.ApiLogger.LogDebug($"GET {uri}");

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
                Configuration.Instance.ApiLogger.LogDebug(json);
                return json;
            }
        }

        private static string _userAgent;
        internal static void SetUserAgent(ref HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(_userAgent))
            {
#if NETSTANDARD2_0
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

        /// <summary>
        /// Send a request to the versioned Nexmo API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="payload">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NexmoResponse DoRequest(string method, Uri uri, object payload, Credentials creds = null)
        {
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            SetUserAgent(ref req);
            // attempt bearer token auth
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (Configuration.Instance.ApiLogger.BeginScope("ApiRequest.DoRequest {0}", uri.GetHashCode()))
            {
                Configuration.Instance.ApiLogger.LogDebug($"{method} {uri} {payload}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();

                if (!sendTask.Result.IsSuccessStatusCode)
                {
                    Configuration.Instance.ApiLogger.LogError($"FAIL: {sendTask.Result.StatusCode}");
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
                Configuration.Instance.ApiLogger.LogDebug(json);
                return new NexmoResponse
                {
                    Status = sendTask.Result.StatusCode,
                    JsonResponse = json
                };
            }
        }

        /// <summary>
        /// Send a GET request to the versioned Nexmo API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="uri">The URI to GET</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static string DoRequest(Uri uri, object parameters, Credentials creds = null)
        {
            var sb = GetQueryStringBuilderFor(parameters);

            return DoRequest(new Uri(uri, "?" + sb), creds);
        }
    }
}
