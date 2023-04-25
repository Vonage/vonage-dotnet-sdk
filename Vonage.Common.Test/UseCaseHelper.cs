using System.Net;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;

namespace Vonage.Common.Test
{
    /// <summary>
    ///     Helper for use cases.
    /// </summary>
    public class UseCaseHelper
    {
        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        private UseCaseHelper()
        {
            this.Serializer = new JsonSerializer();
            this.Fixture = new Fixture();
            this.Fixture.Customize(new SupportMutableValueTypesCustomization());
        }

        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        /// <param name="serializer">A specific serializer.</param>
        private UseCaseHelper(JsonSerializer serializer)
            : this() =>
            this.Serializer = serializer;

        public Fixture Fixture { get; }
        public JsonSerializer Serializer { get; }

        /// <summary>
        ///     Retrieves the path from a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <returns>The path.</returns>
        public static string GetPathFromRequest<T>(Result<T> request) where T : IVonageRequest =>
            request.Match(value => value.GetEndpointPath(), _ => string.Empty);

        /// <summary>
        /// Returns a request's content as a string.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The request type.</typeparam>
        /// <returns>The string content.</returns>
        public static async Task<string> ReadRequestContent<T>(Result<T> request) where T : IVonageRequest =>
            await request
                .Map(value => value.BuildRequestMessage())
                .MapAsync(value => value.Content.ReadAsStringAsync())
                .IfFailure(string.Empty);

        /// <summary>
        ///     Verifies the operation returns the expected value given the response is success.
        /// </summary>
        /// <param name="expected">Expected values for the incoming request.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        public async Task VerifyReturnsExpectedValueGivenApiResponseIsSuccess<TResponse>(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var expectedResponse = this.Fixture.Create<TResponse>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(this.Serializer.SerializeObject(expectedResponse));
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeSuccess(response =>
                this.Serializer.SerializeObject(response).Should()
                    .Be(this.Serializer.SerializeObject(expectedResponse)));
        }

        /// <summary>
        ///     Verifies the operation returns failure given the api response cannot be parsed.
        /// </summary>
        /// <param name="expected">Expected values for the incoming request.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        public async Task VerifyReturnsFailureGivenApiResponseCannotBeParsed<TResponse>(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var body = this.Fixture.Create<string>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(body);
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeFailure(DeserializationFailure.From(typeof(TResponse), body));
        }

        /// <summary>
        ///     Retrieves the property validating the operation returns failure given the api returns an error.
        /// </summary>
        /// <param name="expected">Expected values for the incoming request.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <returns>The property.</returns>
        public Property VerifyReturnsFailureGivenApiResponseIsError<TResponse>(
            ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation) =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error =>
                {
                    var expectedContent = string.Empty;
                    var messageHandler = FakeHttpRequestHandler
                        .Build(error.Code)
                        .WithExpectedRequest(expected);
                    if (error.Message != null)
                    {
                        expectedContent = this.Serializer.SerializeObject(error);
                        messageHandler = messageHandler.WithResponseContent(expectedContent);
                    }

                    operation(this.CreateConfiguration(messageHandler)).Result.Should()
                        .BeFailure(HttpFailure.From(error.Code, error.Message ?? string.Empty, expectedContent));
                });

        /// <summary>
        ///     Retrieves the property validating the operation returns failure given the error cannot be parsed.
        /// </summary>
        /// <param name="expected">Expected values for the incoming request.</param>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <returns>The property.</returns>
        public Property VerifyReturnsFailureGivenErrorCannotBeParsed<TResponse>(
            ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation) =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetNonDeserializableStrings(),
                (statusCode, jsonError) =>
                {
                    var messageHandler = FakeHttpRequestHandler.Build(statusCode)
                        .WithExpectedRequest(expected)
                        .WithResponseContent(jsonError);
                    operation(this.CreateConfiguration(messageHandler))
                        .Result
                        .Should()
                        .BeFailure(HttpFailure.From(statusCode,
                            DeserializationFailure.From(typeof(ErrorResponse), jsonError).GetFailureMessage(),
                            jsonError));
                });

        /// <summary>
        ///     Verifies the operation returns failure given the request is failure.
        /// </summary>
        /// <param name="operation">The call operation.</param>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        public async Task VerifyReturnsFailureGivenRequestIsFailure<TRequest, TResponse>(
            Func<VonageHttpClientConfiguration, Result<TRequest>, Task<Result<TResponse>>> operation)
        {
            var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.OK);
            var expectedFailure = ResultFailure.FromErrorMessage(this.Fixture.Create<string>());
            var result = await operation(this.CreateConfiguration(messageHandler),
                Result<TRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        /// <summary>
        ///     Verifies the operation returns a failure when the token generation fails.
        /// </summary>
        /// <param name="operation">The call operation.</param>
        public async Task VerifyReturnsFailureGivenTokenGenerationFails<TResponse>(
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var configuration = new VonageHttpClientConfiguration(
                FakeHttpRequestHandler.Build(HttpStatusCode.OK).ToHttpClient(),
                () => new AuthenticationFailure().ToResult<string>(),
                this.Fixture.Create<string>());
            var result = await operation(configuration);
            result.Should().BeFailure(new AuthenticationFailure());
        }

        /// <summary>
        ///     Verifies the operation returns the default unit value given the response is success.
        /// </summary>
        /// <param name="expected">Expected values for the incoming request.</param>
        /// <param name="operation">The call operation.</param>
        public async Task VerifyReturnsUnitGivenApiResponseIsSuccess(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<Unit>>> operation)
        {
            var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.OK).WithExpectedRequest(expected);
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeSuccess(Unit.Default);
        }

        /// <summary>
        ///     Initializes the helper with a specific serializer.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The helper.</returns>
        public static UseCaseHelper WithSerializer(JsonSerializer serializer) => new(serializer);

        private VonageHttpClientConfiguration CreateConfiguration(FakeHttpRequestHandler handler) =>
            new(handler.ToHttpClient(), () => this.Fixture.Create<string>(),
                this.Fixture.Create<string>());
    }

    public struct ExpectedRequest
    {
        public Maybe<string> Content { get; set; }
        public HttpMethod Method { get; set; }
        public Uri RequestUri { get; set; }
    }
}