using System.Net;
using AutoFixture;
using FsCheck;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using WireMock.Matchers.Request;
using WireMock.Server;

namespace Vonage.Common.Test
{
    /// <summary>
    ///     Helper for use cases.
    /// </summary>
    public class UseCaseHelper : IDisposable
    {
        public Fixture Fixture { get; }
        public JsonSerializer Serializer { get; }

        public WireMockServer Server { get; }

        public string Token { get; }

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

        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        /// <param name="serializer">A specific serializer.</param>
        public UseCaseHelper(JsonSerializer serializer)
            : this()
        {
            this.Serializer = serializer;
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
        public static string GetPathFromRequest<T>(Result<T> request) where T : IVonageRequest =>
            request.Match(value => value.GetEndpointPath(), failure => string.Empty);

        /// <summary>
        ///     Verifies the operation returns failure given the api response cannot be parsed.
        /// </summary>
        /// <param name="requestBuilder">Request builder for WireMock.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        public async Task VerifyReturnsFailureGivenApiResponseCannotBeParsed<TResponse>(IRequestMatcher requestBuilder,
            Func<Task<Result<TResponse>>> operation)
        {
            var body = this.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{typeof(TResponse).Name}'.";
            this.Server
                .Given(requestBuilder)
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await operation();
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        /// <summary>
        ///     Retrieves the property validating the operation returns failure given the api returns an error.
        /// </summary>
        /// <param name="requestBuilder">Request builder for WireMock.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <returns>The property.</returns>
        public Property VerifyReturnsFailureGivenApiResponseIsError<TResponse>(
            IRequestMatcher requestBuilder,
            Func<Task<Result<TResponse>>> operation) =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error =>
                {
                    var expectedBody = error.Message is null
                        ? null
                        : this.Serializer.SerializeObject(error);
                    this.Server
                        .Given(requestBuilder)
                        .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
                    operation().Result.Should().BeFailure(error.ToHttpFailure());
                });

        /// <summary>
        ///     Retrieves the property validating the operation returns failure given the error cannot be parsed.
        /// </summary>
        /// <param name="requestBuilder">Request builder for WireMock.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <returns>The property.</returns>
        public Property VerifyReturnsFailureGivenErrorCannotBeParsed<TResponse>(
            IRequestMatcher requestBuilder,
            Func<Task<Result<TResponse>>> operation) =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetNonEmptyStrings(),
                (statusCode, jsonError) =>
                {
                    this.Server
                        .Given(requestBuilder)
                        .RespondWith(WireMockExtensions.CreateResponse(statusCode, jsonError));
                    operation()
                        .Result
                        .Should()
                        .BeFailure(ResultFailure.FromErrorMessage(
                            $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'."));
                });

        /// <summary>
        ///     Verifies the operation returns failure given the request is failure.
        /// </summary>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        public async Task VerifyReturnsFailureGivenRequestIsFailure<TRequest, TResponse>(
            Func<Result<TRequest>, Task<Result<TResponse>>> operation)
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.Fixture.Create<string>());
            var result = await operation(Result<TRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }
    }
}