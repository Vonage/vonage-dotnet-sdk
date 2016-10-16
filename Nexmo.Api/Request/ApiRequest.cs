using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace Nexmo.Api.Request
{
    internal static class ApiRequest
    {
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
                baseUri = new Uri(Configuration.Instance.Settings["Nexmo.Url.Api"]);
            }
            else
            {
                baseUri = new Uri(Configuration.Instance.Settings["Nexmo.Url.Rest"]);
            }
            return string.IsNullOrEmpty(url) ? baseUri : new Uri(baseUri, url);
        }

        public static string DoRequest(Uri uri, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            parameters.Add("api_key", Configuration.Instance.Settings["Nexmo.api_key"]);
            parameters.Add("api_secret", Configuration.Instance.Settings["Nexmo.api_secret"]);
            foreach (var key in parameters.Keys)
            {
                sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(key), WebUtility.UrlEncode(parameters[key]));
            }

            return DoRequest(new Uri(uri, "?" + sb));
        }

        public static StringBuilder GetQueryStringBuilderFor(object parameters)
        {
            var apiParams = GetParameters(parameters);

            apiParams.Add("api_key", Configuration.Instance.Settings["Nexmo.api_key"]);
            apiParams.Add("api_secret", Configuration.Instance.Settings["Nexmo.api_secret"]);
            var sb = new StringBuilder();
            foreach (var key in apiParams.Keys)
            {
                sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(key), WebUtility.UrlEncode(apiParams[key]));
            }
            return sb;
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
                Method = HttpMethod.Get,
            };

            var sendTask = Configuration.Instance.Client.SendAsync(req);
            sendTask.Wait();
            var readTask = sendTask.Result.Content.ReadAsStreamAsync();
            readTask.Wait();
            string json;
            using (var sr = new StreamReader(readTask.Result))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }

        public static NexmoResponse DoRequest(string method, Uri uri, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            // if parameters is null, assume that key and secret have been taken care of
            if (null != parameters)
            {
                parameters.Add("api_key", Configuration.Instance.Settings["Nexmo.api_key"]);
                parameters.Add("api_secret", Configuration.Instance.Settings["Nexmo.api_secret"]);
                foreach (var key in parameters.Keys)
                {
                    sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(key), WebUtility.UrlEncode(parameters[key]));
                }
            }

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };

            var data = Encoding.ASCII.GetBytes(sb.ToString());
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            //req.Content.Headers.ContentLength = data.Length;

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

        public static NexmoResponse DoPostRequest(Uri uri, object parameters)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequest(uri, apiParams);            
        }
        public static NexmoResponse DoPutRequest(Uri uri, object parameters)
        {
            var apiParams = GetParameters(parameters);
            return DoPutRequest(uri, apiParams);
        }
        public static NexmoResponse DoDeleteRequest(Uri uri, object parameters)
        {
            var apiParams = GetParameters(parameters);
            return DoDeleteRequest(uri, apiParams);
        }

        public static NexmoResponse DoPostRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("POST", uri, parameters);
        public static NexmoResponse DoPutRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("PUT", uri, parameters);
        public static NexmoResponse DoDeleteRequest(Uri uri, Dictionary<string, string> parameters) => DoRequest("DELETE", uri, parameters);
    }
}
