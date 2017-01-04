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
    internal static class ApiRequest
    {
        private static StringBuilder BuildQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                }
            };
            parameters.Add("api_key", Configuration.Instance.Settings["appSettings:Nexmo.api_key"].ToUpper());
            if (string.IsNullOrEmpty(Configuration.Instance.Settings["appSettings:Nexmo.security_secret"]))
            {
                // security secret not provided, do not sign
                parameters.Add("api_secret", Configuration.Instance.Settings["appSettings:Nexmo.api_secret"]);
                buildStringFromParams(parameters, sb);
                return sb;
            }
            // security secret provided, sort and sign request
            parameters.Add("timestamp", ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.InvariantCulture));
            var sortedParams = new SortedDictionary<string, string>(parameters);
            buildStringFromParams(sortedParams, sb);
            var queryToSign = "&" + sb;
            queryToSign = queryToSign.Remove(queryToSign.Length - 1) + Configuration.Instance.Settings["appSettings:Nexmo.security_secret"].ToUpper();
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

        public static Uri GetBaseUriFor(Type component, string url = null)
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

        public static StringBuilder GetQueryStringBuilderFor(object parameters)
        {
            var apiParams = GetParameters(parameters);
            var sb = BuildQueryString(apiParams);
            return sb;
        }

        public static string DoRequest(Uri uri, Dictionary<string, string> parameters)
        {
            var sb = BuildQueryString(parameters);
            return DoRequest(new Uri(uri, "?" + sb));
        }

        public static string DoRequest(Uri uri, object parameters)
        {
            var sb = GetQueryStringBuilderFor(parameters);

            return DoRequest(new Uri(uri, "?" + sb));
        }

        public static string DoRequest(Uri uri)
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            VersionedApiRequest.SetUserAgent(ref req);

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

        private static NexmoResponse DoRequest(string method, Uri uri, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            // if parameters is null, assume that key and secret have been taken care of
            if (null != parameters)
            {
                sb = BuildQueryString(parameters);
            }

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            VersionedApiRequest.SetUserAgent(ref req);
            
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

        public static NexmoResponse DoPostRequest(Uri uri, object parameters)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequest(uri, apiParams);            
        }

        public static NexmoResponse DoPostRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("POST", uri, parameters);
        public static NexmoResponse DoPutRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("PUT", uri, parameters);
        public static NexmoResponse DoDeleteRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("DELETE", uri, parameters);
    }
}