using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Nexmo.Api.Request
{
    internal static class ApiRequest
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

        internal static Dictionary<string, string> GetParameters(object parameters)
        {
            var apiParams = new Dictionary<string, string>();
            foreach (var property in parameters.GetType().GetProperties())
            {
                string jsonPropertyName = null;

                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Length > 0)
                {
                    jsonPropertyName =
                        ((JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0])
                            .PropertyName;
                }

                if (null == parameters.GetType().GetProperty(property.Name).GetValue(parameters, null)) continue;

                apiParams.Add(string.IsNullOrEmpty(jsonPropertyName) ? property.Name : jsonPropertyName,
                    parameters.GetType().GetProperty(property.Name).GetValue(parameters, null).ToString());
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
                baseUri = new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Api"]);
            }
            else
            {
                baseUri = new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Rest"]);
            }
            return string.IsNullOrEmpty(url) ? baseUri : new Uri(baseUri, url);
        }

        public static string DoRequest(Uri uri, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            parameters.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
            parameters.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);
            foreach (var key in parameters.Keys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]));
            }

            return DoRequest(new Uri(uri, "?" + sb));
        }

        public static StringBuilder GetQueryStringBuilderFor(object parameters)
        {
            var apiParams = GetParameters(parameters);

            apiParams.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
            apiParams.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);
            var sb = new StringBuilder();
            foreach (var key in apiParams.Keys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(apiParams[key]));
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
            var req = _webRequestFactory.CreateHttp(uri);
            var resp = req.GetResponse();
            string json;
            using (var sr = new StreamReader(resp.GetResponseStream()))
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
                parameters.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
                parameters.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);
                foreach (var key in parameters.Keys)
                {
                    sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]));
                }
            }

            var req = _webRequestFactory.CreateHttp(uri);

            req.Method = method;
            var data = Encoding.ASCII.GetBytes(sb.ToString());

            req.ContentType = "application/x-www-form-urlencoded";
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
