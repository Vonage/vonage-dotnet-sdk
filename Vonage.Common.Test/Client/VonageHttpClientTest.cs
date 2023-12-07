using System.Net;
using System.Net.Http.Headers;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;

namespace Vonage.Common.Test.Client
{
    public class VonageHttpClientTest
    {
        private readonly Fixture fixture;

        private readonly JsonSerializer serializer;
        private readonly Result<FakeRequest> request;

        public VonageHttpClientTest()
        {
            this.serializer = new JsonSerializer();
            this.fixture = new Fixture();
            this.fixture.Customize(new SupportMutableValueTypesCustomization());
            this.request = BuildRequest();
        }

        [Fact]
        public async Task SendAsync_ShouldThrowException_GivenOperationExceedsTimeout() =>
            await this.VerifyReturnsFailureGivenOperationExceedsTimeout(client => client.SendAsync(this.request));

        [Property]
        public Property SendAsync_VerifyReturnsFailureGivenApiResponseIsError() =>
            this.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(),
                configuration => new VonageHttpClient(configuration, this.serializer).SendAsync(this.request));

        [Property]
        public Property SendAsync_VerifyReturnsFailureGivenErrorCannotBeParsed() =>
            this.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(),
                configuration => new VonageHttpClient(configuration, this.serializer).SendAsync(this.request));

        [Fact]
        public async Task SendAsync_VerifyReturnsFailureGivenRequestIsFailure() =>
            await this.VerifyReturnsFailureGivenRequestIsFailure((configuration, failureRequest) =>
                new VonageHttpClient(configuration, this.serializer).SendAsync(failureRequest));

        [Fact]
        public async Task SendAsync_VerifyReturnsFailureGivenTokenGenerationFails() =>
            await this.VerifyReturnsFailureGivenTokenGenerationFails(configuration =>
                new VonageHttpClient(configuration, this.serializer).SendAsync(this.request));

        [Fact]
        public async Task SendAsync_VerifyReturnsUnitGivenApiResponseIsSuccess() =>
            await this.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(BuildExpectedRequest(),
                configuration => new VonageHttpClient(configuration, this.serializer).SendAsync(this.request));

        [Fact]
        public async Task SendWithoutHeaderAsync_ShouldThrowException_GivenOperationExceedsTimeout() =>
            await this.VerifyReturnsFailureGivenOperationExceedsTimeout(client =>
                client.SendWithoutHeadersAsync(this.request));

        [Property]
        public Property SendWithoutHeaderAsync_VerifyReturnsFailureGivenApiResponseIsError() =>
            this.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(),
                configuration => new VonageHttpClient(configuration, this.serializer).SendAsync(this.request));

        [Property]
        public Property SendWithoutHeaderAsync_VerifyReturnsFailureGivenErrorCannotBeParsed() =>
            this.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer).SendWithoutHeadersAsync(this.request));

        [Fact]
        public async Task SendWithoutHeaderAsync_VerifyReturnsFailureGivenRequestIsFailure() =>
            await this.VerifyReturnsFailureGivenRequestIsFailure((configuration, failureRequest) =>
                new VonageHttpClient(configuration, this.serializer).SendWithoutHeadersAsync(failureRequest));

        [Fact]
        public async Task SendWithoutHeaderAsync_VerifyReturnsUnitGivenApiResponseIsSuccess() =>
            await this.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer).SendWithoutHeadersAsync(this.request));

        [Fact]
        public async Task SendWithRawResponseAsync_ShouldThrowException_GivenOperationExceedsTimeout() =>
            await this.VerifyReturnsFailureGivenOperationExceedsTimeout(client =>
                client.SendWithRawResponseAsync(this.request));

        [Fact]
        public async Task SendWithRawResponseAsync_VerifyReturnsExpectedValueGivenApiResponseIsSuccess() =>
            await this.VerifyReturnsRawContentGivenApiResponseIsSuccess(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer).SendWithRawResponseAsync(this.request));

        [Property]
        public Property SendWithRawResponseAsync_VerifyReturnsFailureGivenApiResponseIsError() =>
            this.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer).SendWithRawResponseAsync(this.request));

        [Property]
        public Property SendWithRawResponseAsync_VerifyReturnsFailureGivenErrorCannotBeParsed() =>
            this.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer).SendWithRawResponseAsync(this.request));

        [Fact]
        public async Task SendWithRawResponseAsync_VerifyReturnsFailureGivenRequestIsFailure() =>
            await this.VerifyReturnsFailureGivenRequestIsFailure((configuration, failureRequest) =>
                new VonageHttpClient(configuration, this.serializer).SendWithRawResponseAsync(failureRequest));

        [Fact]
        public async Task SendWithRawResponseAsync_VerifyReturnsFailureGivenTokenGenerationFails() =>
            await this.VerifyReturnsFailureGivenTokenGenerationFails(configuration =>
                new VonageHttpClient(configuration, this.serializer).SendWithRawResponseAsync(
                    this.request));

        [Fact]
        public async Task SendWithResponseAsync_ShouldThrowException_GivenOperationExceedsTimeout() =>
            await this.VerifyReturnsFailureGivenOperationExceedsTimeout(client =>
                client.SendWithResponseAsync<FakeRequest, FakeResponse>(this.request));

        [Fact]
        public async Task SendWithResponseAsync_VerifyReturnsExpectedValueGivenApiResponseIsSuccess() =>
            await this.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer)
                        .SendWithResponseAsync<FakeRequest, FakeResponse>(
                            this.request));

        [Fact]
        public async Task SendWithResponseAsync_VerifyReturnsFailureGivenApiResponseCannotBeParsed() =>
            await this.VerifyReturnsFailureGivenApiResponseCannotBeParsed(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer)
                        .SendWithResponseAsync<FakeRequest, FakeResponse>(
                            this.request));

        [Property]
        public Property SendWithResponseAsync_VerifyReturnsFailureGivenApiResponseIsError() =>
            this.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer)
                        .SendWithResponseAsync<FakeRequest, FakeResponse>(
                            this.request));

        [Property]
        public Property SendWithResponseAsync_VerifyReturnsFailureGivenErrorCannotBeParsed() =>
            this.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(),
                configuration =>
                    new VonageHttpClient(configuration, this.serializer)
                        .SendWithResponseAsync<FakeRequest, FakeResponse>(
                            this.request));

        [Fact]
        public async Task SendWithResponseAsync_VerifyReturnsFailureGivenRequestIsFailure() =>
            await this.VerifyReturnsFailureGivenRequestIsFailure((configuration, failureRequest) =>
                new VonageHttpClient(configuration, this.serializer).SendWithResponseAsync<FakeRequest, FakeResponse>(
                    failureRequest));

        [Fact]
        public async Task SendWithResponseAsync_VerifyReturnsFailureGivenTokenGenerationFails() =>
            await this.VerifyReturnsFailureGivenTokenGenerationFails(configuration =>
                new VonageHttpClient(configuration, this.serializer).SendWithResponseAsync<FakeRequest, FakeResponse>(
                    this.request));

        private static ExpectedRequest BuildExpectedRequest() =>
            new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/my-fake-api/yolo", UriKind.Relative),
                Content = "{\"id\":\"foo bar\",\"name\":\"My fake request\"}",
            };

        private static Result<FakeRequest> BuildRequest() =>
            new FakeRequest {Id = Guid.Parse("ceb2b201-2143-48f5-8890-c58369394eba"), Name = "My fake request"};

        private VonageHttpClientConfiguration CreateConfiguration(FakeHttpRequestHandler handler) =>
            new(handler.ToHttpClient(), new AuthenticationHeaderValue("Anonymous"), this.fixture.Create<string>());

        private async Task VerifyReturnsExpectedValueGivenApiResponseIsSuccess<TResponse>(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var expectedResponse = this.fixture.Create<TResponse>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(this.serializer.SerializeObject(expectedResponse));
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeSuccess(expectedResponse);
        }

        private async Task VerifyReturnsFailureGivenApiResponseCannotBeParsed<TResponse>(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var body = this.fixture.Create<string>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(body);
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeFailure(DeserializationFailure.From(typeof(TResponse), body));
        }

        private Property VerifyReturnsFailureGivenApiResponseIsError<TResponse>(
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
                        expectedContent = this.serializer.SerializeObject(error);
                        messageHandler = messageHandler.WithResponseContent(expectedContent);
                    }

                    operation(this.CreateConfiguration(messageHandler)).Result.Should()
                        .BeFailure(HttpFailure.From(error.Code, error.Message ?? string.Empty, expectedContent));
                });

        private Property VerifyReturnsFailureGivenErrorCannotBeParsed<TResponse>(
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

        private async Task VerifyReturnsFailureGivenOperationExceedsTimeout<TResponse>(
            Func<VonageHttpClient, Task<Result<TResponse>>> operation)
        {
            var httpClient = FakeHttpRequestHandler.Build(HttpStatusCode.OK).WithDelay(TimeSpan.FromSeconds(5))
                .ToHttpClient();
            httpClient.Timeout = TimeSpan.FromMilliseconds(100);
            var client =
                new VonageHttpClient(
                    new VonageHttpClientConfiguration(httpClient, new AuthenticationHeaderValue("Anonymous"),
                        this.fixture.Create<string>()), this.serializer);
            await operation(client).Should().BeFailureAsync();
        }

        private async Task VerifyReturnsFailureGivenRequestIsFailure<TRes>(
            Func<VonageHttpClientConfiguration, Result<FakeRequest>, Task<Result<TRes>>> operation)
        {
            var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.OK);
            var expectedFailure = ResultFailure.FromErrorMessage(this.fixture.Create<string>());
            var result = await operation(this.CreateConfiguration(messageHandler),
                Result<FakeRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        private async Task VerifyReturnsFailureGivenTokenGenerationFails<TResponse>(
            Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
        {
            var configuration = new VonageHttpClientConfiguration(
                FakeHttpRequestHandler.Build(HttpStatusCode.OK).ToHttpClient(),
                new AuthenticationFailure().ToResult<AuthenticationHeaderValue>(),
                this.fixture.Create<string>());
            var result = await operation(configuration);
            result.Should().BeFailure(new AuthenticationFailure());
        }

        private async Task VerifyReturnsRawContentGivenApiResponseIsSuccess(ExpectedRequest expected,
            Func<VonageHttpClientConfiguration, Task<Result<string>>> operation)
        {
            var expectedResponse = this.fixture.Create<string>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(expected)
                .WithResponseContent(expectedResponse);
            var result = await operation(this.CreateConfiguration(messageHandler));
            result.Should().BeSuccess(expectedResponse);
        }

        private struct FakeRequest : IVonageRequest
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
                .Initialize(HttpMethod.Post, this.GetEndpointPath())
                .WithContent(new StringContent("{\"id\":\"foo bar\",\"name\":\"My fake request\"}"))
                .Build();

            public string GetEndpointPath() => "/my-fake-api/yolo";
        }

        private struct FakeResponse
        {
            public Guid Id { get; set; }
        }
    }
}