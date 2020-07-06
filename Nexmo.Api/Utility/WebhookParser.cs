using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexmo.Api.Messaging;

namespace Nexmo.Api.Utility
{
    public sealed class WebhookParser
    {

        /// <summary>
        /// Parses the stream into the given type
        /// This is anticipated to be used by ASP.NET Core MVC/API requests where the content is in the Body of the inbound request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="contentType">The content type of the request, must be of the type application/json or application/x-www-form-urlencoded</param>
        /// <exception cref="ArgumentException">Thrown if Content type does not contain application/json or application/x-www-form-urlencoded</exception>
        /// <returns></returns>
        public static async Task<T> ParseWebhookAsync<T>(Stream content, string contentType)
        {
            if (contentType.Contains("application/json"))
            {
                using (var reader = new StreamReader(content))
                {
                    var json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            else if (contentType.Contains("application/x-www-form-urlencoded"))
            {
                using(var reader = new StreamReader(content))
                {
                    var contentString = await reader.ReadToEndAsync();
                    return ParseUrlFormString<T>(contentString);
                }
            }
            else
            {
                throw new ArgumentException("Invalid Content Type");
            }
        }                


        /// <summary>
        /// Parses the HttpRequestMessag's content into the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <exception cref="ArgumentException">Thrown if Content type does not contain application/json or application/x-www-form-urlencoded</exception>
        /// <returns></returns>
        public static async Task<T> ParseWebhookAsync<T>(HttpRequestMessage request)
        {
            if(request.Content.Headers.GetValues("Content-Type").First().Contains("application/json"))
            {
                var json = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else if (request.Content.Headers.GetValues("Content-Type").First().Contains("application/x-www-form-urlencoded"))
            {
                var contentString = await request.Content.ReadAsStringAsync();
                return ParseUrlFormString<T>(contentString);
            }
            throw new ArgumentException("Invalid Content Type");
        }

        /// <summary>
        /// Synchronous Implementation of ParseWebhook
        /// Meant to be called from ASP.NET Core MVC with only the Content of the body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ParseWebhook<T>(Stream content, string contentType)
        {
            return ParseWebhookAsync<T>(content,contentType).Result;
        }

        /// <summary>
        /// Synchronous implementation of the ParseWehbook method, meant to be called from 
        /// Legacy ASP.NET Web Api with an HttpRequestMessage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T ParseWebhook<T>(HttpRequestMessage request)
        {
            return ParseWebhookAsync<T>(request).Result;
        }

        /// <summary>
        /// Used to Parse Query parameters into a givent type
        /// This Method will convert the string pairs into a dictioanry and then use
        /// Newtonsoft to convert the pairs to JSON - finally resolving the object from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static T ParseQuery<T>(IEnumerable<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> requestData)
        {
            var dict = requestData.ToDictionary(x => x.Key, x => x.Value.ToString());
            var json = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(json);
        }


        /// <summary>
        /// Used to Parse Query parameters into a givent type
        /// This Method will convert the string pairs into a dictioanry and then use
        /// Newtonsoft to convert the pairs to JSON - finally resolving the object from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static T ParseQueryNameValuePairs<T>(IEnumerable<KeyValuePair<string, string>> requestData)
        {
            var dict = requestData.ToDictionary(x => x.Key, x => x.Value);
            var json = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Parses URL content into the given object type
        /// This uses Newtonsoft.Json - abnormally named fields should be decorated with the 'JsonPropertyAttribute'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentString"></param>
        /// <returns></returns>
        public static T ParseUrlFormString<T>(string contentString)
        {
            var split_parameters = contentString.Split(new[] { '&' });
            var content_dictonary = new Dictionary<string, string>();
            foreach (var param in split_parameters)
            {
                var split = param.Split('=');
                content_dictonary.Add(split[0], split[1]);
            }
            var json = JsonConvert.SerializeObject(content_dictonary);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
