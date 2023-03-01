﻿using System.Net;
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
            this.Token = this.Fixture.Create<string>();
        }

        public Fixture Fixture { get; }
        public JsonSerializer Serializer { get; }

        public string Token { get; }

        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        /// <param name="serializer">A specific serializer.</param>
        public UseCaseHelper(JsonSerializer serializer)
            : this()
        {
            this.Serializer = serializer;
        }

        /// <summary>
        ///     Retrieves the path from a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <returns>The path.</returns>
        public static string GetPathFromRequest<T>(Result<T> request) where T : IVonageRequest =>
            request.Match(value => value.GetEndpointPath(), _ => string.Empty);

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<string> ReadRequestContent<T>(Result<T> request) where T : IVonageRequest
        {
            var content = await request
                .Map(value => value.BuildRequestMessage())
                .MapAsync(value => value.Content.ReadAsStringAsync());
            return content.IfFailure(string.Empty);
        }

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
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{typeof(TResponse).Name}'.";
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(body);
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
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
                    var messageHandler = FakeHttpRequestHandler
                        .Build(error.Code)
                        .WithExpectedRequest(expected);
                    if (error.Message != null)
                    {
                        messageHandler = messageHandler.WithResponseContent(this.Serializer.SerializeObject(error));
                    }

                    operation(this.CreateConfiguration(messageHandler)).Result.Should()
                        .BeFailure(error.ToHttpFailure());
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
                FsCheckExtensions.GetNonEmptyStrings(),
                (statusCode, jsonError) =>
                {
                    var messageHandler = FakeHttpRequestHandler.Build(statusCode)
                        .WithExpectedRequest(expected)
                        .WithResponseContent(jsonError);
                    operation(this.CreateConfiguration(messageHandler))
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
            Func<VonageHttpClientConfiguration, Result<TRequest>, Task<Result<TResponse>>> operation)
        {
            var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.OK);
            var expectedFailure = ResultFailure.FromErrorMessage(this.Fixture.Create<string>());
            var result = await operation(this.CreateConfiguration(messageHandler),
                Result<TRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
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