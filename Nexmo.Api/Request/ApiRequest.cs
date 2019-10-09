using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using Nexmo.Api.Logging;

namespace Nexmo.Api.Request
{
    /// <summary>
    /// Responsible for sending all Nexmo API requests that do not make use of Application authentication.
    /// For application authentication, see VersionedApiRequest.
    /// </summary>
    public static class ApiRequest
    {
        private static readonly ILog Logger = LogProvider.GetLogger(typeof(ApiRequest), "Nexmo.Api.Request.ApiRequest");

        private static StringBuilder BuildQueryString(IDictionary<string, string> parameters, Credentials creds = null)
        {
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"]).ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Nexmo.api_secret"];
            var securitySecret = creds?.SecuritySecret ?? Configuration.Instance.Settings["appSettings:Nexmo.security_secret"];
            Credentials.SigningMethod method;
            if (creds?.Method != null)
            {
                method = creds.Method;
            }
            else if(Enum.TryParse(Configuration.Instance.Settings["appSettings:Nexmo.signing_method"], out method))
            {
                //left blank intentionally
            }
            else
            {
                method = Credentials.SigningMethod.md5hash;
            }

            var sb = new StringBuilder();
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                }
            };
            parameters.Add("api_key", apiKey);
            if (string.IsNullOrEmpty(securitySecret))
            {
                // security secret not provided, do not sign
                parameters.Add("api_secret", apiSecret);
                buildStringFromParams(parameters, sb);
                return sb;
            }
            parameters.Add("timestamp", ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.InvariantCulture));
            var sortedParams = new SortedDictionary<string, string>(parameters);
            buildStringFromParams(sortedParams, sb);
            var signature = Credentials.GenerateSignature(sb.ToString(), securitySecret, method);
            sb.AppendFormat("sig={0}", signature);
            return sb;
        }

        internal static Dictionary<string, string> GetParameters(object parameters)
        {
            var paramType = parameters.GetType().GetTypeInfo();
            var apiParams = new Dictionary<string, string>();
            foreach (var property in paramType.GetProperties())
            {
                string jsonPropertyName = null;

                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Any())
                {
                    jsonPropertyName =
                        ((JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).First())
                            .PropertyName;
                }

                if (null == paramType.GetProperty(property.Name).GetValue(parameters, null)) continue;

                apiParams.Add(string.IsNullOrEmpty(jsonPropertyName) ? property.Name : jsonPropertyName,
                    paramType.GetProperty(property.Name).GetValue(parameters, null).ToString());
            }
            return apiParams;
        }

        internal static Uri GetBaseUriFor(Type component, string url = null)
        {
            Uri baseUri;
            if (typeof(NumberVerify) == component
                || typeof(ApiSecret) == component
                || typeof(ApplicationV2) == component
                || typeof(Voice.Call) == component
                || typeof(Redact) == component)
            {
                baseUri = new Uri(Configuration.Instance.Settings["appSettings:Nexmo.Url.Api"]);
            }
            else
            {
                baseUri = new Uri(Configuration.Instance.Settings["appSettings:Nexmo.Url.Rest"]);
            }
            return string.IsNullOrEmpty(url) ? baseUri : new Uri(baseUri, url);
        }

        internal static StringBuilder GetQueryStringBuilderFor(object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            var sb = BuildQueryString(apiParams, creds);
            return sb;
        }

        /// <summary>
        /// Send a GET request to the Nexmo API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="uri">The URI to GET</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static string DoRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null)
        {
            var sb = BuildQueryString(parameters, creds);
            return DoRequest(new Uri(uri, "?" + sb), creds);
        }

        internal static string DoRequest(Uri uri, object parameters, Credentials creds = null)
        {
            var sb = GetQueryStringBuilderFor(parameters, creds);

            return DoRequest(new Uri(uri, "?" + sb), creds);
        }

        internal static string DoRequest(Uri uri, Credentials creds)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            VersionedApiRequest.SetUserAgent(ref req, creds);

            // do we need to use basic auth?
            // TODO / HACK: this is a newer auth method that needs to be incorporated better in the future
            if (uri.AbsolutePath.StartsWith("/accounts/") || uri.AbsolutePath.StartsWith("/v2/applications"))
            {
                var authBytes = Encoding.UTF8.GetBytes(creds.ApiKey + ":" + creds.ApiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }

            using (LogProvider.OpenMappedContext("ApiRequest.DoRequest",uri.GetHashCode()))
            {
                Logger.Debug($"GET {uri}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();

                if (!sendTask.Result.IsSuccessStatusCode)
                {
                    Logger.Error($"FAIL: {sendTask.Result.StatusCode}");

                    if (string.Compare(Configuration.Instance.Settings["appSettings:Nexmo.Api.EnsureSuccessStatusCode"],
                            "true", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        sendTask.Result.EnsureSuccessStatusCode();
                    }
                }

                var readTask = sendTask.Result.Content.ReadAsStreamAsync();
                readTask.Wait();

                string json;
                using (var sr = new StreamReader(readTask.Result))
                {
                    json = sr.ReadToEnd();
                }
                Logger.Debug(json);
                return json;
            }
        }

        /// <summary>
        /// Send a request to the Nexmo API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NexmoResponse DoRequest(string method, Uri uri, Dictionary<string, string> parameters, Credentials creds = null)
        {
            var sb = new StringBuilder();
            // if parameters is null, assume that key and secret have been taken care of
            if (null != parameters)
            {
                sb = BuildQueryString(parameters, creds);
            }

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method)
            };
            VersionedApiRequest.SetUserAgent(ref req, creds);
            
            var data = Encoding.ASCII.GetBytes(sb.ToString());
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using (LogProvider.OpenMappedContext("ApiRequest.DoRequest",uri.GetHashCode()))
            {
                Logger.Debug($"{method} {uri} {sb}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();

                if (!sendTask.Result.IsSuccessStatusCode)
                {
                    Logger.Error($"FAIL: {sendTask.Result.StatusCode}");

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
                Logger.Debug(json);
                return new NexmoResponse
                {
                    Status = sendTask.Result.StatusCode,
                    JsonResponse = json
                };
            }
        }

        public static NexmoResponse DoRequest(string method, Uri uri, object requestBody, Credentials creds = null)
        {
            var sb = new StringBuilder();
            var parameters = new Dictionary<string, string>();
            sb = BuildQueryString(parameters, creds);

            var requestContent = JsonConvert.SerializeObject(requestBody);

            var req = new HttpRequestMessage
            {
                RequestUri = new Uri((uri.OriginalString + $"?{sb}").ToLower()),
                Method = new HttpMethod(method),
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json"),
            };
            VersionedApiRequest.SetUserAgent(ref req, creds);

            using (LogProvider.OpenMappedContext("ApiRequest.DoRequest", uri.GetHashCode()))
            {
                Logger.Debug($"{method} {uri} {sb}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();
                
                if (!sendTask.Result.IsSuccessStatusCode)
                {
                    
                    Logger.Error($"FAIL: {sendTask.Result.StatusCode}");

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

                string jsonResult;
                var readTask = sendTask.Result.Content.ReadAsStreamAsync();
                readTask.Wait();
                using (var sr = new StreamReader(readTask.Result))
                {
                    jsonResult = sr.ReadToEnd();
                }
                Logger.Debug(jsonResult);
                return new NexmoResponse
                {
                    Status = sendTask.Result.StatusCode,
                    JsonResponse = jsonResult
                };
            }
        }

        internal static HttpResponseMessage DoRequestJwt(Uri uri, Credentials creds)
        {
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            VersionedApiRequest.SetUserAgent(ref req, creds);

            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));

            using (LogProvider.OpenMappedContext("ApiRequest.DoRequestJwt", uri.GetHashCode()))
            {
                Logger.Debug($"GET {uri}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();

                if (!sendTask.Result.IsSuccessStatusCode)
                {
                    Logger.Error($"FAIL: {sendTask.Result.StatusCode}");

                    if (string.Compare(Configuration.Instance.Settings["appSettings:Nexmo.Api.EnsureSuccessStatusCode"],
                            "true", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        sendTask.Result.EnsureSuccessStatusCode();
                    }
                }
                return sendTask.Result;
            }
        }

        internal static NexmoResponse DoPostRequest(Uri uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequest(uri, apiParams, creds);            
        }

        internal static NexmoResponse DoPostRequestWithContent(Uri uri, object parameters, Credentials creds = null)
        {
            return DoRequestWithContent(uri, parameters, creds);
        }

        internal static NexmoResponse DoPostRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("POST", uri, parameters, creds);
        internal static NexmoResponse DoRequestWithContent(Uri uri, object parameters, Credentials creds = null) => DoRequest("POST", uri, parameters, creds);
        internal static NexmoResponse DoPutRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("PUT", uri, parameters, creds);
        internal static NexmoResponse DoDeleteRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("DELETE", uri, parameters, creds);
    }
}