using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vonage.Common.Client;
using Vonage.Common.Exceptions;
using Vonage.Cryptography;
using Vonage.Logger;
using Vonage.Serialization;

namespace Vonage.Request;

/// <summary>
///     Responsible for sending all Vonage API requests that do not make use of Application authentication.
///     For application authentication, see VersionedApiRequest.
/// </summary>
internal partial class ApiRequest
{
    private readonly Credentials credentials;
    private readonly ILogger logger;
    private readonly string userAgent;

    public ApiRequest(Credentials credentials)
    {
        this.logger = LogProvider.GetLogger("Vonage.Request.ApiRequest");
        this.credentials = credentials;
        this.userAgent = UserAgentProvider.GetFormattedUserAgent(GetUserAgent(credentials));
    }

    /// <summary>
    ///     Sends an HTTP DELETE
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="authType"></param>
    /// <returns></returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public async Task<VonageResponse> DoDeleteRequestWithUrlContentAsync(Uri uri,
        Dictionary<string, string> parameters, AuthType authType = AuthType.Query) =>
        await this.DoRequestWithUrlContentAsync("DELETE", uri, parameters, authType);

    /// <summary>
    ///     Sends a GET request to the Vonage API using a JWT and returns the full HTTP response message
    ///     this is primarily for pulling a raw stream off an API call -e.g. a recording
    /// </summary>
    /// <param name="uri"></param>
    /// <returns>HttpResponseMessage</returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public async Task<HttpResponseMessage> DoGetRequestWithJwtAsync(Uri uri)
    {
        var req = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = HttpMethod.Get,
        };
        SetUserAgent(ref req);
        req.Headers.Authorization =
            BuildBearerAuth(GetApplicationId(this.credentials), GetApplicationKey(this.credentials));
        this.logger.LogDebug("GET {Uri}", uri);
        var result = await Configuration.Instance.Client.SendAsync(req);
        try
        {
            result.EnsureSuccessStatusCode();
            return result;
        }
        catch (HttpRequestException ex)
        {
            this.logger.LogError("FAIL: {StatusCode}", result.StatusCode);
            throw new VonageHttpRequestException(ex) {HttpStatusCode = result.StatusCode};
        }
    }

    /// <summary>
    ///     SendAsync a GET request to the versioned Vonage API.
    ///     Do not include credentials in the parameters object. If you need to override credentials, use the optional
    ///     Credentials parameter.
    /// </summary>
    /// <param name="uri">The URI to GET</param>
    /// <param name="authType"></param>
    /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
    /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
    public async Task<T> DoGetRequestWithQueryParametersAsync<T>(Uri uri, AuthType authType,
        object parameters = null)
    {
        parameters ??= new Dictionary<string, string>();
        var sb = GetQueryStringBuilderFor(parameters, authType, this.credentials);
        var requestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
        return await this.SendGetRequestAsync<T>(requestUri, authType);
    }

    /// <summary>
    ///     Sends a Post request to the specified endpoint with the given parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="credentials"></param>
    /// <param name="withCredentials">Indicates whether credentials should be included in Query string.</param>
    /// <returns></returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public async Task<T> DoPostRequestUrlContentFromObjectAsync<T>(Uri uri, object parameters,
        bool withCredentials = true) =>
        await this.DoPostRequestWithUrlContentAsync<T>(uri, GetParameters(parameters), withCredentials);

    /// <summary>
    ///     SendAsync a request to the versioned Vonage API.
    ///     Do not include credentials in the parameters object. If you need to override credentials, use the optional
    ///     Credentials parameter.
    /// </summary>
    /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
    /// <param name="uri">The URI to communicate with</param>
    /// <param name="payload">Parameters required by the endpoint (do not include credentials)</param>
    /// <param name="authType">Authorization type used on the API</param>
    /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public Task<T> DoRequestWithJsonContentAsync<T>(string method, Uri uri, object payload,
        AuthType authType) =>
        this.DoRequestWithJsonContentAsync(method, uri, payload, authType,
            value => JsonConvert.SerializeObject(value, VonageSerialization.SerializerSettings),
            JsonConvert.DeserializeObject<T>);

    public static Uri GetBaseUri(UriType uriType, string url = null) =>
        string.IsNullOrEmpty(url) ? BuildBaseUri(uriType) : new Uri(BuildBaseUri(uriType), url);

    private static Uri BuildBaseUri(UriType uriType) =>
        uriType switch
        {
            UriType.Api => Configuration.Instance.NexmoApiUrl,
            UriType.Rest => Configuration.Instance.RestApiUrl,
            _ => throw new Exception("Unknown Uri Type Detected"),
        };

    private static AuthenticationHeaderValue BuildBasicAuth(string apikey, string apiSecret)
    {
        if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(apiSecret))
        {
            throw VonageAuthenticationException.FromMissingApiKeyOrSecret();
        }

        return new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apikey} : {apiSecret}")));
    }

    private static AuthenticationHeaderValue BuildBearerAuth(string applicationId, string applicationKeyPath) =>
        new("Bearer", Jwt.CreateToken(applicationId, applicationKeyPath));

    /// <summary>
    ///     Builds a query string for a get request - if there is a security secret a signature is built for the request and
    ///     added to the query string
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="credentials"></param>
    /// <param name="withCredentials">Indicates whether credentials should be included in Query string.</param>
    /// <returns></returns>
    private static StringBuilder BuildQueryString(IDictionary<string, string> parameters,
        Credentials credentials = null,
        bool withCredentials = true)
    {
        var securitySecret = credentials?.SecuritySecret ?? Configuration.Instance.SecuritySecret;
        SmsSignatureGenerator.Method method;
        if (credentials?.Method != null)
        {
            method = credentials.Method;
        }
        else if (Enum.TryParse(Configuration.Instance.SigningMethod, out method))
        {
            //left blank intentionally
        }
        else
        {
            method = SmsSignatureGenerator.Method.md5hash;
        }

        var sb = new StringBuilder();
        var signatureSb = new StringBuilder();

        void BuildStringFromParams(IDictionary<string, string> param, StringBuilder strings)
        {
            foreach (var kvp in param)
            {
                //Special Case for ids from MessagesSearch API which needs a series of ID's with unescaped &/=
                strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key),
                    kvp.Key == "ids" ? kvp.Value : WebUtility.UrlEncode(kvp.Value));
            }
        }

        void BuildSignatureStringFromParams(IDictionary<string, string> param, StringBuilder strings)
        {
            foreach (var kvp in param)
            {
                strings.AppendFormat("{0}={1}&", kvp.Key.Replace('=', '_').Replace('&', '_'),
                    kvp.Value.Replace('=', '_').Replace('&', '_'));
            }
        }

        if (withCredentials)
        {
            parameters.Add("api_key", GetApiKey(credentials));
        }

        if (string.IsNullOrEmpty(securitySecret))
        {
            // security secret not provided, do not sign
            if (withCredentials)
            {
                parameters.Add("api_secret", GetApiSecret(credentials));
            }

            BuildStringFromParams(parameters, sb);
            return sb;
        }

        parameters.Add("timestamp",
            ((int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(
                CultureInfo.InvariantCulture));
        var sortedParams = new SortedDictionary<string, string>(parameters);
        BuildStringFromParams(sortedParams, sb);
        BuildSignatureStringFromParams(sortedParams, signatureSb);
        var queryToSign = "&" + signatureSb;
        queryToSign = queryToSign.Remove(queryToSign.Length - 1);
        var signature = SmsSignatureGenerator.GenerateSignature(queryToSign, securitySecret, method);
        sb.AppendFormat("sig={0}", signature);
        return sb;
    }

    private async Task<T> DoPostRequestWithUrlContentAsync<T>(Uri uri, Dictionary<string, string> parameters,
        bool withCredentials = true)
    {
        var response =
            await this.DoRequestWithUrlContentAsync("POST", uri, parameters, withCredentials: withCredentials);
        return JsonConvert.DeserializeObject<T>(response.JsonResponse);
    }

    private async Task<VonageResponse> DoRequestWithUrlContentAsync(string method, Uri uri,
        Dictionary<string, string> parameters, AuthType authType = AuthType.Query,
        bool withCredentials = true)
    {
        var sb = new StringBuilder();

        // if parameters is null, assume that key and secret have been taken care of            
        if (null != parameters)
        {
            sb = GetQueryStringBuilderFor(parameters, authType, this.credentials, withCredentials);
        }

        var req = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = new HttpMethod(method),
        };
        if (authType == AuthType.Basic)
        {
            req.Headers.Authorization = BuildBasicAuth(GetApiKey(this.credentials), GetApiSecret(this.credentials));
        }

        SetUserAgent(ref req);
        var data = Encoding.ASCII.GetBytes(sb.ToString());
        req.Content = new ByteArrayContent(data);
        req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        this.logger.LogDebug("{Method} {Uri} {StringBuilder}", method, uri, sb);
        return await this.SendHttpRequestAsync(req);
    }

    private static string GetApiKey(Credentials credentials) => credentials?.ApiKey ?? Configuration.Instance.ApiKey;

    private static string GetApiSecret(Credentials credentials) =>
        credentials?.ApiSecret ?? Configuration.Instance.ApiSecret;

    private static string GetApplicationId(Credentials credentials) =>
        credentials?.ApplicationId ?? Configuration.Instance.ApplicationId;

    private static string GetApplicationKey(Credentials credentials) =>
        credentials?.ApplicationKey ?? Configuration.Instance.ApplicationKey;

    /// <summary>
    ///     extracts parameters from an object into a dictionary
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private static Dictionary<string, string> GetParameters(object parameters)
    {
        var json = JsonConvert.SerializeObject(parameters, VonageSerialization.SerializerSettings);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }

    private static StringBuilder GetQueryStringBuilderFor(object parameters, AuthType type,
        Credentials credentials = null, bool withCredentials = true)
    {
        var apiParams = parameters as Dictionary<string, string> ?? GetParameters(parameters);
        var sb = new StringBuilder();
        if (type == AuthType.Query)
        {
            sb = BuildQueryString(apiParams, credentials, withCredentials);
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

    private static string GetUserAgent(Credentials credentials) =>
        credentials?.AppUserAgent ?? Configuration.Instance.UserAgent;

    /// <summary>
    ///     Sends an HTTP GET request to the Vonage API without any additional parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri"></param>
    /// <param name="authType"></param>
    /// <param name="credentials"></param>
    /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
    private async Task<T> SendGetRequestAsync<T>(Uri uri, AuthType authType)
    {
        var req = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = HttpMethod.Get,
        };
        SetUserAgent(ref req);
        switch (authType)
        {
            case AuthType.Basic:
                req.Headers.Authorization = BuildBasicAuth(GetApiKey(this.credentials), GetApiSecret(this.credentials));
                break;
            case AuthType.Bearer:
                req.Headers.Authorization =
                    BuildBearerAuth(GetApplicationId(this.credentials), GetApplicationKey(this.credentials));
                break;
            case AuthType.Query:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(authType), authType, null);
        }

        this.logger.LogDebug("GET {Uri}", uri);
        var json = (await this.SendHttpRequestAsync(req)).JsonResponse;
        return JsonConvert.DeserializeObject<T>(json);
    }

    private async Task<VonageResponse> SendHttpRequestAsync(HttpRequestMessage req)
    {
        var response = await Configuration.Instance.Client.SendAsync(req).ConfigureAwait(false);
        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        string json;
        using (var sr = new StreamReader(stream))
        {
            json = await sr.ReadToEndAsync();
        }

        try
        {
            this.logger.LogDebug("{Json}", json);
            response.EnsureSuccessStatusCode();
            return new VonageResponse
            {
                Status = response.StatusCode,
                JsonResponse = json,
            };
        }
        catch (HttpRequestException exception)
        {
            this.logger.LogError("FAIL: {StatusCode}", response.StatusCode);
            throw new VonageHttpRequestException(exception.Message + " Json from error: " + json)
            {
                HttpStatusCode = response.StatusCode,
                Json = json,
            };
        }
    }

    /// <summary>
    ///     Sets the user agent for an HTTP request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="credentials"></param>
    private void SetUserAgent(ref HttpRequestMessage request) => request.Headers.UserAgent.ParseAdd(this.userAgent);

    /// <summary>
    ///     Type of the Uri.
    /// </summary>
    public enum UriType
    {
        /// <summary>
        ///     Api uri type.
        /// </summary>
        Api,

        /// <summary>
        ///     Rest uri type.
        /// </summary>
        Rest,
    }

    /// <summary>
    ///     Retrieves the Base URI for a given component and appends the given url to the end of it.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    internal static Uri GetBaseUriFor(string url = null) =>
        string.IsNullOrEmpty(url) ? Configuration.Instance.RestApiUrl : new Uri(Configuration.Instance.RestApiUrl, url);

    internal async Task<T> DoRequestWithJsonContentAsync<T>(string method, Uri uri, object payload,
        AuthType authType, Func<object, string> payloadSerialization,
        Func<string, T> payloadDeserialization)
    {
        var req = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = new HttpMethod(method),
        };
        SetUserAgent(ref req);
        switch (authType)
        {
            case AuthType.Basic:
                req.Headers.Authorization = BuildBasicAuth(GetApiKey(this.credentials), GetApiSecret(this.credentials));
                break;
            case AuthType.Bearer:
                req.Headers.Authorization =
                    BuildBearerAuth(GetApplicationId(this.credentials), GetApplicationKey(this.credentials));
                break;
            case AuthType.Query:
                var sb = BuildQueryString(new Dictionary<string, string>(), this.credentials);
                req.RequestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
                break;
            default:
                throw new ArgumentException("Unknown Auth Type set for function");
        }

        var json = payloadSerialization(payload);
        this.logger.LogDebug("Request URI: {Uri}", uri);
        this.logger.LogDebug("JSON Payload: {Json}", json);
        var data = Encoding.UTF8.GetBytes(json);
        req.Content = new ByteArrayContent(data);
        req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var jsonResponse = (await this.SendHttpRequestAsync(req)).JsonResponse;
        return payloadDeserialization(jsonResponse);
    }
}

/// <summary>
///     Defines the authentication type.
/// </summary>
public enum AuthType
{
    /// <summary>
    ///     Basic authentication.
    /// </summary>
    Basic,

    /// <summary>
    ///     Bearer authentication.
    /// </summary>
    Bearer,

    /// <summary>
    ///     Query authentication.
    /// </summary>
    Query,
}