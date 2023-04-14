using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Exceptions;

namespace Vonage.Request;

internal partial class ApiRequest
{
    /// <summary>
    ///     Sends an HTTP DELETE
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public static VonageResponse DoDeleteRequestWithUrlContent(Uri uri, Dictionary<string, string> parameters,
        AuthType authType = AuthType.Query, Credentials creds = null) =>
        ExecuteAsyncOperation(() => DoRequestWithUrlContentAsync("DELETE", uri, parameters, authType, creds));

    /// <summary>
    ///     Sends a GET request to the Vonage API using a JWT and returns the full HTTP resonse message
    ///     this is primarily for pulling a raw stream off an API call -e.g. a recording
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="creds"></param>
    /// <returns>HttpResponseMessage</returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public static HttpResponseMessage DoGetRequestWithJwt(Uri uri, Credentials creds) =>
        ExecuteAsyncOperation(() => DoGetRequestWithJwtAsync(uri, creds));

    /// <summary>
    ///     SendAsync a GET request to the versioned Vonage API.
    ///     Do not include credentials in the parameters object. If you need to override credentials, use the optional
    ///     Credentials parameter.
    /// </summary>
    /// <param name="uri">The URI to GET</param>
    /// <param name="parameters">Parameters required by the endpoint (do not include credentials)</param>
    /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
    public static T DoGetRequestWithQueryParameters<T>(Uri uri, AuthType authType, object parameters = null,
        Credentials credentials = null) =>
        ExecuteAsyncOperation(() => DoGetRequestWithQueryParametersAsync<T>(uri, authType, parameters, credentials));

    /// <summary>
    ///     Sends a Post request to the specified endpoint with the given parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="creds"></param>
    /// <param name="withCredentials">Indicates whether credentials should be included in Query string.</param>
    /// <returns></returns>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public static T DoPostRequestUrlContentFromObject<T>(Uri uri, object parameters, Credentials creds = null,
        bool withCredentials = true) =>
        ExecuteAsyncOperation(() => DoPostRequestUrlContentFromObjectAsync<T>(uri, parameters, creds, withCredentials));

    /// <summary>
    ///     SendAsync a request to the versioned Vonage API.
    ///     Do not include credentials in the parameters object. If you need to override credentials, use the optional
    ///     Credentials parameter.
    /// </summary>
    /// <param name="method">HTTP method (POST, PUT, DELETE, etc)</param>
    /// <param name="uri">The URI to communicate with</param>
    /// <param name="payload">Parameters required by the endpoint (do not include credentials)</param>
    /// <param name="authType">Authorization type used on the API</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    public static T DoRequestWithJsonContent<T>(string method, Uri uri, object payload, AuthType authType,
        Credentials creds) =>
        ExecuteAsyncOperation(() => DoRequestWithJsonContentAsync<T>(method, uri, payload, authType, creds));

    private static T ExecuteAsyncOperation<T>(Func<Task<T>> operation)
    {
        try
        {
            return operation().Result;
        }
        catch (AggregateException exception)
        {
            throw exception.InnerExceptions.First();
        }
    }

    /// <summary>
    ///     Sends an HTTP GET request to the Vonage API without any additional parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri"></param>
    /// <param name="authType"></param>
    /// <param name="creds"></param>
    /// <exception cref="VonageHttpRequestException">Thrown if the API encounters a non-zero result</exception>
    private static T SendGetRequest<T>(Uri uri, AuthType authType, Credentials creds) =>
        ExecuteAsyncOperation(() => SendGetRequestAsync<T>(uri, authType, creds));
}