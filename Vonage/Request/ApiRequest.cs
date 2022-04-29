using Newtonsoft.Json;
using Vonage.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vonage.Serialization;

namespace Vonage.Request
{
    /// <summary>
    /// Responsible for sending all Vonage API requests that do not make use of Application authentication.
    /// For application authentication, see VersionedApiRequest.
    /// </summary>
    internal class ApiRequest
    {

        public enum AuthType
        {
            Basic,
            Bearer,
            Query
        }

        public enum UriType
        {
            Api,
            Rest
        }

        const string LoggerCategory = "Vonage.Request.ApiRequest";

        private static string _userAgent;

        /// <summary>
        /// Sets the user agent for an HTTP request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        internal static void SetUserAgent(ref HttpRequestMessage request, Credentials creds)
        {
            if (string.IsNullOrEmpty(_userAgent))
            {
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1
                // TODO: watch the next core release; may have functionality to make this cleaner
                var languageVersion = (System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription)
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("_", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    ;
#else
                var languageVersion = System.Diagnostics.FileVersionInfo
                    .GetVersionInfo(typeof(int).Assembly.Location)
                    .ProductVersion;
#endif
                var libraryVersion = typeof(ApiRequest)
                    .GetTypeInfo()
                    .Assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;

                _userAgent = $"vonage-dotnet/{libraryVersion} dotnet/{languageVersion}";

                var appVersion = creds?.AppUserAgent ?? Configuration.Instance.Settings["appSettings:Vonage.UserAgent"];
                if (!string.IsNullOrWhiteSpace(appVersion))
                {
                    _userAgent += $" {appVersion}";
                }
            }

            request.Headers.UserAgent.ParseAdd(_userAgent);
        }

        /// <summary>
        /// Builds a query string for a get request - if there is a security secret a signature is built for the request and added to the query string
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        private static StringBuilder BuildQueryString(IDictionary<string, string> parameters, Credentials creds = null)
        {
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];
            var securitySecret = creds?.SecuritySecret ?? Configuration.Instance.Settings["appSettings:Vonage.security_secret"];
            SmsSignatureGenerator.Method method;
            if (creds?.Method != null)
            {
                method = creds.Method;
            }
            else if(Enum.TryParse(Configuration.Instance.Settings["appSettings:Vonage.signing_method"], out method))
            {
                //left blank intentionally
            }
            else
            {
                method = SmsSignatureGenerator.Method.md5hash;
            }

            var sb = new StringBuilder();
            var signatureSb = new StringBuilder();
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    //Special Case for ids from MessagesSearch API which needs a sereies of ID's with unescaped &/=
                    if(kvp.Key == "ids")
                    {
                        strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), kvp.Value);
                    }
                    else
                    {
                        strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                    }                    
                }
            };
            Action<IDictionary<string, string>, StringBuilder> buildSignatureStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    strings.AppendFormat("{0}={1}&", kvp.Key.Replace('=','_').Replace('&','_'), kvp.Value.Replace('=', '_').Replace('&', '_'));
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
            buildSignatureStringFromParams(sortedParams, signatureSb);
            var queryToSign = "&" + signatureSb.ToString();
            queryToSign = queryToSign.Remove(queryToSign.Length - 1);
            var signature = SmsSignatureGenerator.GenerateSignature(queryToSign, securitySecret, method);
            sb.AppendFormat("sig={0}", signature);
            return sb;
        }

        /// <summary>
        /// extracts parameters from an object into a dictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static Dictionary<string, string> GetParameters(object parameters)
        {
            var json = JsonConvert.SerializeObject(parameters, VonageSerialization.SerializerSettings);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);            
        }

        /// <summary>
        /// Retrieves the Base URI for a given component and appends the given url to the end of it.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static Uri GetBaseUriFor(string url = null)
        {
            Uri baseUri  = new Uri(Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"]);
            return string.IsNullOrEmpty(url) ? baseUri : new Uri(baseUri, url);
        }

        public static Uri GetBaseUri(UriType uriType, string url = null)
        {
            Uri baseUri;
            switch (uriType)
            {
                case UriType.Api:
                    baseUri = new Uri(Configuration.Instance.Settings["appSettings:Vonage.Url.Api"]);
                    break;
                case UriType.Rest:
                    baseUri = new Uri(Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"]);
                    break;
                default:
                    throw new Exception("Unknown Uri Type Detected");
            }
            return string.IsNullOrEmpty(url) ? baseUri : new Uri(baseUri, url);
        }

        /// <summary>
        /// Creates a query string for the given parameters - if the auth type is Query the credentials are appended to the end of the query string
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="type"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        internal static StringBuilder GetQueryStringBuilderFor(object parameters, AuthType type, Credentials creds = null)
        {
            Dictionary<string, string> apiParams;
            if(!(parameters is Dictionary<string,string>))
            {
                apiParams = GetParameters(parameters);
            }
            else
            {
                apiParams = (Dictionary<string, string>)parameters;
            }
            var sb = new StringBuilder();
            if (type == AuthType.Query)
            {
                sb = BuildQueryString(apiParams, creds);
            }
            else
            {
                foreach (var key in apiParams.Keys)
                {
                    sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(key), WebUtility.UrlEncode(apiParams[key]));
                }
            }
            
            return sb;
        }

        /// <summary>
        /// SendAsync a GET request to the versioned Vonage API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="uri">The URI to GET</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
        public static async Task<T> DoGetRequestWithQueryParametersAsync<T>(Uri uri, AuthType authType, object parameters = null, Credentials credentials = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            var sb = GetQueryStringBuilderFor(parameters, authType, credentials);
            var requestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
            return await SendGetRequestAsync<T>(requestUri, authType, credentials);
        }

        /// <summary>
        /// SendAsync a GET request to the versioned Vonage API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="uri">The URI to GET</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
        public static T DoGetRequestWithQueryParameters<T>(Uri uri, AuthType authType, object parameters = null, Credentials credentials = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            var sb = GetQueryStringBuilderFor(parameters, authType, credentials);
            var requestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
            return SendGetRequest<T>(requestUri, authType, credentials);
        }
        
        /// <summary>
        /// Sends an HTTP GET request to the Vonage API without any additional parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="authType"></param>
        /// <param name="creds"></param>
        /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
        private static T SendGetRequest<T>(Uri uri, AuthType authType, Credentials creds)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            SetUserAgent(ref req, creds);

            if (authType == AuthType.Basic)
            {
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                    throw new VonageAuthenticationException("API Key or API Secret missing.");

                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }
            else if (authType == AuthType.Bearer)
            {
                if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKeyPath))
                    throw new VonageAuthenticationException("AppId or Private Key Path missing.");

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    Jwt.CreateToken(appId, appKeyPath));
            }
            logger.LogDebug($"GET {uri}");
            var json = (SendHttpRequest(req)).JsonResponse;
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Sends an HTTP GET request to the Vonage API without any additional parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="authType"></param>
        /// <param name="creds"></param>
        /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
        private static async Task<T> SendGetRequestAsync<T>(Uri uri, AuthType authType, Credentials creds)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];            
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            SetUserAgent(ref req, creds);
            
            if (authType == AuthType.Basic)
            {
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                    throw new VonageAuthenticationException("API Key or API Secret missing.");

                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }
            else if (authType == AuthType.Bearer)
            {
                if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKeyPath))
                    throw new VonageAuthenticationException("AppId or Private Key Path missing.");

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    Jwt.CreateToken(appId, appKeyPath));
            }
            logger.LogDebug($"GET {uri}");
            var json = (await SendHttpRequestAsync(req)).JsonResponse;
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// SendAsync a request to the Vonage API using the specified HTTP method and the provided parameters.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns></returns>
        public static async Task<VonageResponse> DoRequestWithUrlContentAsync(string method, Uri uri, Dictionary<string, string> parameters, AuthType authType = AuthType.Query, Credentials creds = null)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var sb = new StringBuilder();
            // if parameters is null, assume that key and secret have been taken care of            
            if (null != parameters)
            {
                sb = GetQueryStringBuilderFor(parameters, authType, creds);
            }            

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method)
            };
            if (authType == AuthType.Basic)
            {
                var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
                var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];
                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }
            SetUserAgent(ref req, creds);
            
            var data = Encoding.ASCII.GetBytes(sb.ToString());
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            logger.LogDebug($"{method} {uri} {sb}");
            return await SendHttpRequestAsync(req);
        }

        /// <summary>
        /// SendAsync a request to the Vonage API using the specified HTTP method and the provided parameters.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns></returns>
        public static VonageResponse DoRequestWithUrlContent(string method, Uri uri, Dictionary<string, string> parameters, AuthType authType = AuthType.Query, Credentials creds = null)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var sb = new StringBuilder();
            // if parameters is null, assume that key and secret have been taken care of            
            if (null != parameters)
            {
                sb = GetQueryStringBuilderFor(parameters, authType, creds);
            }

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method)
            };
            if (authType == AuthType.Basic)
            {
                var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
                var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];
                var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authBytes));
            }
            SetUserAgent(ref req, creds);

            var data = Encoding.ASCII.GetBytes(sb.ToString());
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            logger.LogDebug($"{method} {uri} {sb}");
            return SendHttpRequest(req);
        }

        /// <summary>
        /// Sends an HttpRequest on to the Vonage API
        /// </summary>
        /// <param name="req"></param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns></returns>
        public static async Task<VonageResponse> SendHttpRequestAsync(HttpRequestMessage req)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var response = await Configuration.Instance.Client.SendAsync(req).ConfigureAwait(false);
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            string json;
            
            using (var sr = new StreamReader(stream))
            {
                json = await sr.ReadToEndAsync();
            }
            
            try
            {
                logger.LogDebug(json);
                response.EnsureSuccessStatusCode();

                return new VonageResponse
                {
                    Status = response.StatusCode,
                    JsonResponse = json
                };
            }
            catch (HttpRequestException exception)
            {
                logger.LogError($"FAIL: {response.StatusCode}");
                throw new VonageHttpRequestException(exception.Message + " Json from error: " + json)
                {
                    HttpStatusCode = response.StatusCode, 
                    Json = json
                };
            }
        }

        /// <summary>
        /// Sends an HttpRequest on to the Vonage API
        /// </summary>
        /// <param name="req"></param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns></returns>
        public static VonageResponse SendHttpRequest(HttpRequestMessage req)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var response = Configuration.Instance.Client.SendAsync(req).Result;
            var stream = response.Content.ReadAsStreamAsync().Result;
            string json;
            using (var sr = new StreamReader(stream))
            {
                json = sr.ReadToEnd();
            }
            try
            {
                logger.LogDebug(json);
                response.EnsureSuccessStatusCode();
                return new VonageResponse
                {
                    Status = response.StatusCode,
                    JsonResponse = json
                };
            }
            catch (HttpRequestException exception)
            {
                logger.LogError($"FAIL: {response.StatusCode}");
                throw new VonageHttpRequestException(exception.Message + " Json from error: " + json) { HttpStatusCode = response.StatusCode, Json = json };
            }
        }

        /// <summary>
        /// SendAsync a request to the versioned Vonage API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="payload">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="authType">Authorization type used on the API</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static async Task<T> DoRequestWithJsonContentAsync<T>(string method, Uri uri, object payload, AuthType authType, Credentials creds)
        {
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            SetUserAgent(ref req, creds);

            switch (authType)
            {
                case AuthType.Basic:
                    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                        throw new VonageAuthenticationException("API Key or API Secret missing.");

                    var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authBytes));
                    break;
                case AuthType.Bearer:
                    if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKeyPath))
                        throw new VonageAuthenticationException("AppId or Private Key Path missing.");

                    // attempt bearer token auth
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                        Jwt.CreateToken(appId, appKeyPath));
                    break;
                case AuthType.Query:
                    var sb = BuildQueryString(new Dictionary<string, string>(), creds);
                    req.RequestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
                    break;
                default:
                    throw new ArgumentException("Unkown Auth Type set for function");
            }
            
            var json = JsonConvert.SerializeObject(payload, VonageSerialization.SerializerSettings);
            logger.LogDebug($"Request URI: {uri}");
            logger.LogDebug($"JSON Payload: {json}");
            
            var data = Encoding.UTF8.GetBytes(json);
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            
            var jsonResponse = (await SendHttpRequestAsync(req)).JsonResponse;
            return JsonConvert.DeserializeObject<T>(jsonResponse);            
        }

        /// <summary>
        /// SendAsync a request to the versioned Vonage API.
        /// Do not include credentials in the parameters object. If you need to override credentials, use the optional Credentials parameter.
        /// </summary>
        /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
        /// <param name="uri">The URI to communicate with</param>
        /// <param name="payload">Parameters required by the endpoint (do not include credentials)</param>
        /// <param name="authType">Authorization type used on the API</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static T DoRequestWithJsonContent<T>(string method, Uri uri, object payload, AuthType authType, Credentials creds)
        {
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];
            var apiKey = (creds?.ApiKey ?? Configuration.Instance.Settings["appSettings:Vonage_key"])?.ToLower();
            var apiSecret = creds?.ApiSecret ?? Configuration.Instance.Settings["appSettings:Vonage_secret"];
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = new HttpMethod(method),
            };
            SetUserAgent(ref req, creds);

            switch (authType)
            {
                case AuthType.Basic:                    
                    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                        throw new VonageAuthenticationException("API Key or API Secret missing.");
                
                    var authBytes = Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authBytes));
                    break;

                case AuthType.Bearer:
                    if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKeyPath))
                        throw new VonageAuthenticationException("AppId or Private Key Path missing.");
                    
                    // attempt bearer token auth
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                        Jwt.CreateToken(appId, appKeyPath));
                    break;                    

                case AuthType.Query:
                    var sb = BuildQueryString(new Dictionary<string, string>(), creds);
                    req.RequestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
                    break;

                default:
                    throw new ArgumentException("Unkown Auth Type set for function");
            }

            var json = JsonConvert.SerializeObject(payload, VonageSerialization.SerializerSettings);

            logger.LogDebug($"Request URI: {uri}");
            logger.LogDebug($"JSON Payload: {json}");

            var data = Encoding.UTF8.GetBytes(json);
            req.Content = new ByteArrayContent(data);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            
            var jsonResponse = SendHttpRequest(req).JsonResponse;
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        /// <summary>
        /// Sends a GET request to the Vonage API using a JWT and returns the full HTTP resonse message 
        /// this is primarily for pulling a raw stream off an API call -e.g. a recording
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="creds"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static async Task<HttpResponseMessage> DoGetRequestWithJwtAsync(Uri uri, Credentials creds)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            SetUserAgent(ref req, creds);

            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));

            logger.LogDebug($"GET {uri}");

            var result = await Configuration.Instance.Client.SendAsync(req);

            try
            {
                result.EnsureSuccessStatusCode();
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"FAIL: {result.StatusCode}");
                throw new VonageHttpRequestException(ex.Message, ex) { HttpStatusCode = result.StatusCode };
            }
            
        }

        /// <summary>
        /// Sends a GET request to the Vonage API using a JWT and returns the full HTTP resonse message 
        /// this is primarily for pulling a raw stream off an API call -e.g. a recording
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="creds"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static HttpResponseMessage DoGetRequestWithJwt(Uri uri, Credentials creds)
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var appId = creds?.ApplicationId ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Id"];
            var appKeyPath = creds?.ApplicationKey ?? Configuration.Instance.Settings["appSettings:Vonage.Application.Key"];

            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            SetUserAgent(ref req, creds);

            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                Jwt.CreateToken(appId, appKeyPath));

            logger.LogDebug($"GET {uri}");

            var result = Configuration.Instance.Client.SendAsync(req).Result;

            try
            {
                result.EnsureSuccessStatusCode();
                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"FAIL: {result.StatusCode}");
                throw new VonageHttpRequestException(ex.Message, ex) { HttpStatusCode = result.StatusCode };
            }

        }

        /// <summary>
        /// Sends a Post request to the specified endpoint with the given parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static async Task<T> DoPostRequestUrlContentFromObjectAsync<T>(Uri uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return await DoPostRequestWithUrlContentAsync<T>(uri, apiParams, creds);
        }

        /// <summary>
        /// Sends a Post request to the specified endpoint with the given parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static T DoPostRequestUrlContentFromObject<T>(Uri uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequestWithUrlContent<T>(uri, apiParams, creds);
        }

        /// <summary>
        /// SendAsync a Post Request with Url Content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static async Task<T> DoPostRequestWithUrlContentAsync<T>(Uri uri, Dictionary<string, string> parameters, Credentials creds = null) 
        {
            var response = await DoRequestWithUrlContentAsync("POST", uri, parameters, creds:creds);
            return JsonConvert.DeserializeObject<T>(response.JsonResponse);
        }

        /// <summary>
        /// SendAsync a Post Request with Url Content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static T DoPostRequestWithUrlContent<T>(Uri uri, Dictionary<string, string> parameters, Credentials creds = null)
        {
            var response = DoRequestWithUrlContent("POST", uri, parameters, creds: creds);
            return JsonConvert.DeserializeObject<T>(response.JsonResponse);
        }

        /// <summary>
        /// Sends an HTTP DELETE 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static async Task<VonageResponse> DoDeleteRequestWithUrlContentAsync(Uri uri, Dictionary<string, string> parameters, AuthType authType = AuthType.Query, Credentials creds = null) => await DoRequestWithUrlContentAsync("DELETE", uri, parameters, authType, creds);

        /// <summary>
        /// Sends an HTTP DELETE 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static VonageResponse DoDeleteRequestWithUrlContent(Uri uri, Dictionary<string, string> parameters, AuthType authType = AuthType.Query, Credentials creds = null) => DoRequestWithUrlContent("DELETE", uri, parameters, authType, creds);
    }
}