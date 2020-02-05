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
        private const string LOGGER_CATEGORY = "Nexmo.Api.VersionedApiRequest";
        public enum AuthType
        {
            Basic,
            Bearer
        }

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

        private static string DoRequest(Uri uri, AuthType authType, Credentials creds = null)
        {
            var logger = Api.Logger.LogProvider.GetLogger(LOGGER_CATEGORY);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"];
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Nexmo.api_secret"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            SetUserAgent(ref req, creds);
            // attempt bearer token auth
            if(authType == AuthType.Bearer)
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));
            }
            else if(authType == AuthType.Basic)
            {
                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }


            logger.LogDebug($"GET {uri}");

            var sendTask = Configuration.Instance.Client.SendAsync(req);
            sendTask.Wait();

            if (!sendTask.Result.IsSuccessStatusCode)
            {
                logger.LogError($"FAIL: {sendTask.Result.StatusCode}");

                if (string.Compare(Configuration.Instance.Settings["appSettings:Nexmo.Api.EnsureSuccessStatusCode"],
                        "true", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sendTask.Result.EnsureSuccessStatusCode();
                }
            }

            string json;
            var readTask = sendTask.Result.Content.ReadAsStreamAsync();
            readTask.Wait();
            using (var sr = new StreamReader(readTask.Result))
            {
                json = sr.ReadToEnd();
            }
            logger.LogDebug(json);

            return json;
        }

        private static string _userAgent;
        internal static void SetUserAgent(ref HttpRequestMessage request, Credentials creds)
        {
            if (string.IsNullOrEmpty(_userAgent))
            {
#if NETSTANDARD1_6 || NETSTANDARD2_0
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

                var appVersion = creds?.AppUserAgent ?? Configuration.Instance.Settings["appSettings:Nexmo.UserAgent"];
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
            var logger = Api.Logger.LogProvider.GetLogger(LOGGER_CATEGORY);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"];
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Nexmo.api_secret"];
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            SetUserAgent(ref req, creds);

            // do we need to use basic auth?
            // TODO / HACK: this is a newer auth method that needs to be incorporated better in the future
            if (uri.AbsolutePath.StartsWith("/accounts/") || uri.AbsolutePath.StartsWith("/v2/applications") || uri.AbsolutePath.StartsWith("/v1/redact/transaction"))
            {
                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }
            else
            {
                // attempt bearer token auth
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    Jwt.CreateToken(appId, appKeyPath));
            }

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            logger.LogDebug($"{method} {uri} {payload}");

            var sendTask = Configuration.Instance.Client.SendAsync(req);
            sendTask.Wait();

            if (!sendTask.Result.IsSuccessStatusCode)
            {
                logger.LogError($"FAIL: {sendTask.Result.StatusCode}");

                if (string.Compare(Configuration.Instance.Settings["appSettings:Nexmo.Api.EnsureSuccessStatusCode"],
                    "true", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sendTask.Result.EnsureSuccessStatusCode();
                }

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
            logger.LogDebug(json);

            return new NexmoResponse
            {
                Status = sendTask.Result.StatusCode,
                JsonResponse = json
            };
        }

        /// <summary>
        /// Send a GET request to the versioned Nexmo API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="uri">The URI to GET</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static string DoRequest(Uri uri, object parameters, AuthType authType, Credentials creds = null)
        {
            var sb = GetQueryStringBuilderFor(parameters);

            return DoRequest(new Uri(uri, "?" + sb), authType, creds);
        }
    }
}
