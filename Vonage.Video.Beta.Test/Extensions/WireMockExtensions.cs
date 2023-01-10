using System.Collections.Generic;
using System.Linq;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Vonage.Video.Beta.Test.Extensions
{
    /// <summary>
    ///     Extensions for WireMock.Net
    /// </summary>
    public static class WireMockExtensions
    {
        /// <summary>
        ///     Creates a response.
        /// </summary>
        /// <param name="code">The http status code</param>
        /// <param name="body">The response body.</param>
        /// <returns>The response.</returns>
        public static IResponseBuilder CreateResponse(HttpStatusCode code, string body) =>
            body is null ? CreateResponse(code) : CreateResponse(code).WithBody(body);

        /// <summary>
        ///     Creates a response without a body.
        /// </summary>
        /// <param name="code">The http status code</param>
        /// <returns>The response.</returns>
        public static IResponseBuilder CreateResponse(HttpStatusCode code) =>
            Response.Create().WithStatusCode(code);

        /// <summary>
        ///     Creates a request.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="path">The endpoint path.</param>
        /// <param name="body">The body.</param>
        /// <returns>The request.</returns>
        public static IRequestBuilder CreateRequest(string token, string path, string body) =>
            body is null ? CreateRequest(token, path) : CreateRequest(token, path).WithBody(body);

        /// <summary>
        ///     Creates a request without a body.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="path">The endpoint path.</param>
        /// <returns>The request.</returns>
        public static IRequestBuilder CreateRequest(string token, string path)
        {
            var builder = BuildRequestWithAuthenticationHeader(token);
            if (!ContainsQueryParameters(path))
            {
                return builder.WithPath(path);
            }

            var pathSeparation = path.Split('?');
            var newPath = pathSeparation[0];
            pathSeparation[1].Split('&')
                .Select(parameter => parameter.Split('='))
                .Select(parameters => new KeyValuePair<string, string>(parameters[0], parameters[1]))
                .ToList()
                .ForEach(parameter => builder = builder.WithParam(parameter.Key, parameter.Value));
            return builder.WithPath(newPath);
        }

        private static bool ContainsQueryParameters(string path) => path.Contains("?");

        private static IRequestBuilder BuildRequestWithAuthenticationHeader(string token) =>
            WireMock.RequestBuilders.Request
                .Create()
                .WithHeader("Authorization", $"Bearer {token}");
    }
}