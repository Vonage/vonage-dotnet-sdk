using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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

        public static Uri GetBaseUriFor(Type component, string url = null)
        {
            var baseUri = typeof(NumberVerify) == component ? new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Api"]) : new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Rest"]);
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

        public static string DoRequest(Uri uri, object parameters)
        {
            var sb = new StringBuilder();
            var apiParams = new Dictionary<string, string>();

            foreach (var property in parameters.GetType().GetProperties())
            {
                string jsonPropertyName = null;

                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Length > 0)
                {
                    jsonPropertyName = ((JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName;
                }

                if (null == parameters.GetType().GetProperty(property.Name).GetValue(parameters, null)) continue;

                apiParams.Add(string.IsNullOrEmpty(jsonPropertyName) ? property.Name : jsonPropertyName,
                    parameters.GetType().GetProperty(property.Name).GetValue(parameters, null).ToString());
            }

            apiParams.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
            apiParams.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);

            foreach (var key in apiParams.Keys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(apiParams[key]));
            }

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
            Debug.WriteLine(json);
            return json;
        }

        public static string DoPostRequest(Uri uri, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            parameters.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
            parameters.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);
            foreach (var key in parameters.Keys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]));
            }

            var req = _webRequestFactory.CreateHttp(uri);

            req.Method = "POST";
            var data = Encoding.ASCII.GetBytes(sb.ToString());

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            var requestStream = req.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            var resp = req.GetResponse();
            string json;
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
