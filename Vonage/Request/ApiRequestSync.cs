#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

namespace Vonage.Request;

internal partial class ApiRequest
{
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

    internal VonageResponse DoDeleteRequestWithUrlContent(Uri uri, Dictionary<string, string> parameters,
        AuthType authType) =>
        ExecuteAsyncOperation(() => this.DoRequestWithUrlContentAsync(HttpMethod.Delete, uri, parameters, authType));

    internal HttpResponseMessage DoGetRequestWithJwt(Uri uri) =>
        ExecuteAsyncOperation(() => this.DoGetRequestWithJwtAsync(uri));

    internal T DoGetRequestWithQueryParameters<T>(Uri uri, AuthType authType, object parameters = null) =>
        ExecuteAsyncOperation(() => this.DoGetRequestWithQueryParametersAsync<T>(uri, authType, parameters));

    internal T DoPostRequestUrlContentFromObject<T>(Uri uri, object parameters,
        bool withCredentials = true) =>
        ExecuteAsyncOperation(() => this.DoPostRequestUrlContentFromObjectAsync<T>(uri, parameters, withCredentials));

    internal T DoRequestWithJsonContent<T>(HttpMethod method, Uri uri, object payload, AuthType authType) =>
        ExecuteAsyncOperation(() => this.DoRequestWithJsonContentAsync<T>(method, uri, payload, authType));
}