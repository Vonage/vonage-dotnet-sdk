using System;
using System.Threading.Tasks;
using AutoFixture;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video;
using WireMock.Matchers.Request;
using WireMock.ResponseProviders;
using WireMock.Server;

namespace Vonage.Video.Beta.Test.Video
{
    /// <summary>
    ///     Helper for use cases.
    /// </summary>
    public class UseCaseHelper : IDisposable
    {
        public JsonSerializer Serializer { get; }

        public WireMockServer Server { get; }

        public string Token { get; }

        public Fixture Fixture { get; }

        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        public UseCaseHelper()
        {
            this.Server = WireMockServer.Start();
            this.Serializer = new JsonSerializer();
            this.Fixture = new Fixture();
            this.Token = this.Fixture.Create<string>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Reset();
            this.Server.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Retrieves the path from a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <returns>The path.</returns>
        public static string GetPathFromRequest<T>(Result<T> request) where T : IVideoRequest =>
            request.Match(value => value.GetEndpointPath(), failure => string.Empty);

        /// <summary>
        ///     Verifies the call returns a failure given the error cannot be parsed.
        /// </summary>
        /// <param name="requestBuilder">Request builder for WireMock.</param>
        /// <param name="responseBuilder">Response builder for WireMock.</param>
        /// <param name="jsonError">The invalid json response.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="T">The type of the response.</typeparam>
        public async Task VerifyReturnsFailureGivenErrorCannotBeParsed<T>(
            IRequestMatcher requestBuilder,
            IResponseProvider responseBuilder,
            string jsonError,
            Func<Task<Result<T>>> operation)
        {
            this.Server.Given(requestBuilder).RespondWith(responseBuilder);
            var result = await operation();
            result.Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'."));
        }

        public async Task VerifyReturnsFailureGivenRequestIsFailure<TRequest, TResponse>(
            Func<Result<TRequest>, Task<Result<TResponse>>> operation)
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.Fixture.Create<string>());
            var result = await operation(Result<TRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }
    }
}