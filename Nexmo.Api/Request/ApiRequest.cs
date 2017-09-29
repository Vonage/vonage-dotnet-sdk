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
using Microsoft.Extensions.Logging;

namespace Nexmo.Api.Request
{
    /// <summary>
    /// Responsible for sending all Nexmo API requests that do not make use of Application authentication.
    /// For application authentication, see VersionedApiRequest.
    /// </summary>
    public static class ApiRequest
    {
        private static StringBuilder BuildQueryString(IDictionary<string, string> parameters, Credentials creds = null)
        {
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"]).ToUpper();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Nexmo.api_secret"];
            var securitySecret = creds?.SecuritySecret ?? Configuration.Instance.Settings["appSettings:Nexmo.security_secret"];

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
            // security secret provided, sort and sign request
            parameters.Add("timestamp", ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.InvariantCulture));
            var sortedParams = new SortedDictionary<string, string>(parameters);
            buildStringFromParams(sortedParams, sb);
            var queryToSign = "&" + sb;
            queryToSign = queryToSign.Remove(queryToSign.Length - 1) + securitySecret.ToUpper();
            var hashgen = MD5.Create();
            var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(queryToSign));
            sb.AppendFormat("sig={0}", ByteArrayToHexHelper.ByteArrayToHex(hash).ToLower());
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
                || typeof(Application) == component
                || typeof(Voice.Call) == component)
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

            using (Configuration.Instance.ApiLogger.BeginScope("ApiRequest.DoRequest {0}",uri.GetHashCode()))
            {
                Configuration.Instance.ApiLogger.LogDebug($"GET {uri}");
                var sendTask = Configuration.Instance.Client.SendAsync(req);
                sendTask.Wait();
                var readTask = sendTask.Result.Content.ReadAsStreamAsync();
                readTask.Wait();
                string json;
                using (var sr = new StreamReader(readTask.Result))
                {
                    json = sr.ReadToEnd();
                }
                Configuration.Instance.ApiLogger.LogDebug(json);
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
                Method = new HttpMethod(method),
            };
            VersionedApiRequest.SetUserAgent(ref req, creds);
            
            var data = Encoding.ASCII.GetBytes(sb.ToString());
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using (Configuration.Instance.ApiLogger.BeginScope("ApiRequest.DoRequest {0}", uri.GetHashCode()))
            {
                Configuration.Instance.ApiLogger.LogDebug($"{method} {uri} {sb}");
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

        internal static NexmoResponse DoPostRequest(Uri uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequest(uri, apiParams, creds);            
        }

        internal static NexmoResponse DoPostRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("POST", uri, parameters, creds);
        internal static NexmoResponse DoPutRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("PUT", uri, parameters, creds);
        internal static NexmoResponse DoDeleteRequest(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) => DoRequest("DELETE", uri, parameters, creds);
    }
}