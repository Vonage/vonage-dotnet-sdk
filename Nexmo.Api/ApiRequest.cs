using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Nexmo.Api
{
    public static class ApiRequest
    {
        public static Uri GetBaseUriFor(Type component, string url = null)
        {
            var baseUri = typeof (NumberVerify) == component ? new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Api"]) : new Uri(ConfigurationManager.AppSettings["Nexmo.Url.Rest"]);
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
            Dictionary<string, string> Dic = new Dictionary<string, string>();

            foreach (System.Reflection.PropertyInfo var in parameters.GetType().GetProperties())
            {
                string jsonPropertyName = null;

                if (var.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Length > 0)
                {
                    jsonPropertyName= ((JsonPropertyAttribute)var.GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]).PropertyName;
                }

                if (parameters.GetType().GetProperty(var.Name).GetValue(parameters, null) != null)
                {
                    if (string.IsNullOrEmpty(jsonPropertyName))
                    {
                        Dic.Add(var.Name, parameters.GetType().GetProperty(var.Name).GetValue(parameters, null).ToString());
                    }
                    else
                    {
                        Dic.Add(jsonPropertyName, parameters.GetType().GetProperty(var.Name).GetValue(parameters, null).ToString());
                    }
                }
            }

            Dic.Add("api_key", ConfigurationManager.AppSettings["Nexmo.api_key"]);
            Dic.Add("api_secret", ConfigurationManager.AppSettings["Nexmo.api_secret"]);

            foreach (var key in Dic.Keys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(Dic[key]));
            }

            return DoRequest(new Uri(uri, "?" + sb));
        }

        public static string DoRequest(Uri uri)
        {
            var req = WebRequest.CreateHttp(uri);
            
            var resp = req.GetResponseAsync().Result;
            string json;
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
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

            var req = WebRequest.CreateHttp(uri);

            req.Method = "POST";
            var data = Encoding.ASCII.GetBytes(sb.ToString());

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            var requestStream = req.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            var resp = req.GetResponseAsync().Result;
            string json;
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
