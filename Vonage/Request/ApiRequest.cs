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
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
using Vonage.Cryptography;
using Vonage.Logger;
using Vonage.Serialization;

namespace Vonage.Request;

internal record VonageResponse(string JsonResponse);

internal partial class ApiRequest
{
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

    private readonly Maybe<Configuration> configuration;
    private readonly Maybe<Credentials> credentials;
    private readonly ILogger logger;
    private readonly ITimeProvider timeProvider;
    private readonly string userAgent;

    private ApiRequest()
    {
        this.logger = LogProvider.GetLogger("Vonage.Request.ApiRequest");
        this.userAgent = UserAgentProvider.GetFormattedUserAgent(this.GetConfiguration().UserAgent);
        this.timeProvider = new TimeProvider();
    }

    private ApiRequest(Credentials credentials) : this()
    {
        this.credentials = credentials;
        this.userAgent = UserAgentProvider.GetFormattedUserAgent(this.GetUserAgent());
    }

    private ApiRequest(Credentials credentials, Configuration configuration, ITimeProvider provider) : this(credentials)
    {
        this.configuration = configuration;
        this.timeProvider = provider;
    }

    private static Uri BuildBaseUri(UriType uriType, Configuration configuration) =>
        uriType switch
        {
            UriType.Api => configuration.NexmoApiUrl,
            UriType.Rest => configuration.RestApiUrl,
            _ => throw new Exception("Unknown Uri Type Detected"),
        };

    private AuthenticationHeaderValue BuildBasicAuth()
    {
        if (string.IsNullOrEmpty(this.GetApiKey()) || string.IsNullOrEmpty(this.GetApiSecret()))
        {
            throw VonageAuthenticationException.FromMissingApiKeyOrSecret();
        }

        return new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.GetApiKey()}:{this.GetApiSecret()}")));
    }

    private AuthenticationHeaderValue BuildBearerAuth() =>
        new("Bearer", Jwt.CreateToken(this.GetApplicationId(), this.GetApplicationKey()));

    private HttpRequestMessage BuildMessage(Uri uri, HttpMethod method)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = method,
        };
        request.Headers.UserAgent.ParseAdd(this.userAgent);
        return request;
    }

    private StringBuilder BuildQueryString(IDictionary<string, string> parameters, bool withCredentials = true)
    {
        var method = this.credentials.Map(value => value.Method).IfNone(
            Enum.TryParse(this.GetConfiguration().SigningMethod, out SmsSignatureGenerator.Method output)
                ? output
                : SmsSignatureGenerator.Method.md5hash);
        var sb = new StringBuilder();
        var signatureSb = new StringBuilder();
        if (withCredentials)
        {
            parameters.Add("api_key", this.GetApiKey());
        }

        if (string.IsNullOrEmpty(this.GetSecuritySecret()))
        {
            // security secret not provided, do not sign
            if (withCredentials)
            {
                parameters.Add("api_secret", this.GetApiSecret());
            }

            BuildStringFromParams(parameters, sb);
            return sb;
        }

        parameters.Add("timestamp", this.timeProvider.Epoch.ToString(CultureInfo.InvariantCulture));
        var sortedParams = new SortedDictionary<string, string>(parameters);
        BuildStringFromParams(sortedParams, sb);
        BuildSignatureStringFromParams(sortedParams, signatureSb);
        var queryToSign = "&" + signatureSb;
        queryToSign = queryToSign.Remove(queryToSign.Length - 1);
        var signature = SmsSignatureGenerator.GenerateSignature(queryToSign, this.GetSecuritySecret(), method);
        sb.AppendFormat("sig={0}", signature);
        return sb;

        void BuildSignatureStringFromParams(IDictionary<string, string> param, StringBuilder strings)
        {
            foreach (var kvp in param)
            {
                strings.AppendFormat("{0}={1}&", kvp.Key.Replace('=', '_').Replace('&', '_'),
                    kvp.Value.Replace('=', '_').Replace('&', '_'));
            }
        }

        void BuildStringFromParams(IDictionary<string, string> param, StringBuilder strings)
        {
            foreach (var kvp in param)
            {
                //Special Case for ids from MessagesSearch API which needs a series of ID's with unescaped &/=
                strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key),
                    kvp.Key == "ids" ? kvp.Value : WebUtility.UrlEncode(kvp.Value));
            }
        }
    }

    private async Task<T> DoPostRequestWithUrlContentAsync<T>(Uri uri, Dictionary<string, string> parameters,
        bool withCredentials = true)
    {
        var response =
            await this.DoRequestWithUrlContentAsync(HttpMethod.Post, uri, parameters, withCredentials: withCredentials);
        return JsonConvert.DeserializeObject<T>(response.JsonResponse);
    }

    private async Task<VonageResponse> DoRequestWithUrlContentAsync(HttpMethod method, Uri uri,
        Dictionary<string, string> parameters, AuthType authType = AuthType.Query,
        bool withCredentials = true)
    {
        var sb = new StringBuilder();

        // if parameters is null, assume that key and secret have been taken care of            
        if (null != parameters)
        {
            sb = this.GetQueryStringBuilderFor(parameters, authType, withCredentials);
        }

        var req = this.BuildMessage(uri, method);
        if (authType == AuthType.Basic)
        {
            req.Headers.Authorization = this.BuildBasicAuth();
        }

        var data = Encoding.ASCII.GetBytes(sb.ToString());
        req.Content = new ByteArrayContent(data);
        req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        this.logger.LogDebug("{Method} {Uri} {StringBuilder}", method, uri, sb);
        return await this.SendHttpRequestAsync(req);
    }

    private string GetApiKey() => this.credentials.Bind(value => value.ApiKey ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().ApiKey);

    private string GetApiSecret() => this.credentials.Bind(value => value.ApiSecret ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().ApiSecret);

    private string GetApplicationId() => this.credentials.Bind(value => value.ApplicationId ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().ApplicationId);

    private string GetApplicationKey() => this.credentials.Bind(value => value.ApplicationKey ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().ApplicationKey);

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private static Dictionary<string, string> GetParameters(object parameters)
    {
        var json = JsonConvert.SerializeObject(parameters, VonageSerialization.SerializerSettings);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }

    private StringBuilder GetQueryStringBuilderFor(object parameters, AuthType type, bool withCredentials = true)
    {
        var apiParams = parameters as Dictionary<string, string> ?? GetParameters(parameters);
        var sb = new StringBuilder();
        if (type == AuthType.Query)
        {
            sb = this.BuildQueryString(apiParams, withCredentials);
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

    private string GetSecuritySecret() => this.credentials.Bind(value => value.SecuritySecret ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().SecuritySecret);

    private string GetUserAgent() => this.credentials.Bind(value => value.AppUserAgent ?? Maybe<string>.None)
        .IfNone(this.GetConfiguration().UserAgent);

    private async Task<T> SendGetRequestAsync<T>(Uri uri, AuthType authType)
    {
        var req = this.BuildMessage(uri, HttpMethod.Get);
        switch (authType)
        {
            case AuthType.Basic:
                req.Headers.Authorization = this.BuildBasicAuth();
                break;
            case AuthType.Bearer:
                req.Headers.Authorization = this.BuildBearerAuth();
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
        var response = await this.GetConfiguration().Client.SendAsync(req).ConfigureAwait(false);
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
            return new VonageResponse(json);
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

    internal async Task<VonageResponse> DoDeleteRequestWithUrlContentAsync(Uri uri,
        Dictionary<string, string> parameters, AuthType authType = AuthType.Query) =>
        await this.DoRequestWithUrlContentAsync(HttpMethod.Delete, uri, parameters, authType);

    internal async Task<HttpResponseMessage> DoGetRequestWithJwtAsync(Uri uri)
    {
        var req = this.BuildMessage(uri, HttpMethod.Get);
        req.Headers.Authorization = this.BuildBearerAuth();
        this.logger.LogDebug("GET {Uri}", uri);
        var result = await this.GetConfiguration().Client.SendAsync(req);
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

    internal async Task<T> DoGetRequestWithQueryParametersAsync<T>(Uri uri, AuthType authType,
        object parameters = null)
    {
        parameters ??= new Dictionary<string, string>();
        var sb = this.GetQueryStringBuilderFor(parameters, authType);
        var requestUri = new Uri(uri + (sb.Length != 0 ? "?" + sb : ""));
        return await this.SendGetRequestAsync<T>(requestUri, authType);
    }

    internal async Task<T> DoPostRequestUrlContentFromObjectAsync<T>(Uri uri, object parameters,
        bool withCredentials = true) =>
        await this.DoPostRequestWithUrlContentAsync<T>(uri, GetParameters(parameters), withCredentials);

    internal Task<T> DoRequestWithJsonContentAsync<T>(HttpMethod method, Uri uri, object payload,
        AuthType authType) =>
        this.DoRequestWithJsonContentAsync(method, uri, payload, authType,
            value => JsonConvert.SerializeObject(value, VonageSerialization.SerializerSettings),
            JsonConvert.DeserializeObject<T>);

    internal static Uri GetBaseUri(UriType uriType, Configuration configuration, string url = null) =>
        string.IsNullOrEmpty(url)
            ? BuildBaseUri(uriType, configuration)
            : new Uri(BuildBaseUri(uriType, configuration), url);

    internal static ApiRequest Build(Credentials credentials, Configuration configuration, ITimeProvider provider) =>
        new(credentials, configuration, provider);

    internal static Uri GetBaseUriFor(Configuration configuration, string url = null) =>
        string.IsNullOrEmpty(url) ? configuration.RestApiUrl : new Uri(configuration.RestApiUrl, url);

    internal async Task<T> DoRequestWithJsonContentAsync<T>(HttpMethod method, Uri uri, object payload,
        AuthType authType, Func<object, string> payloadSerialization,
        Func<string, T> payloadDeserialization)
    {
        var req = this.BuildMessage(uri, method);
        switch (authType)
        {
            case AuthType.Basic:
                req.Headers.Authorization = this.BuildBasicAuth();
                break;
            case AuthType.Bearer:
                req.Headers.Authorization = this.BuildBearerAuth();
                break;
            case AuthType.Query:
                var sb = this.BuildQueryString(new Dictionary<string, string>());
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