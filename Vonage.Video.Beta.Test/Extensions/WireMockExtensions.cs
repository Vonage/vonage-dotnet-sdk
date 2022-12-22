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
        public static IRequestBuilder CreateRequest(string token, string path) =>
            BuildRequestWithAuthenticationHeader(token).WithPath(path);

        private static IRequestBuilder BuildRequestWithAuthenticationHeader(string token) =>
            WireMock.RequestBuilders.Request
                .Create()
                .WithHeader("Authorization", $"Bearer {token}");
    }
}