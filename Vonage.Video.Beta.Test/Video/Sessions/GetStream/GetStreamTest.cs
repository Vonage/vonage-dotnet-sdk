using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Moq;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Voice;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamTest : IDisposable
    {
        private readonly SessionClient client;
        private readonly Fixture fixture;
        private readonly JsonSerializer jsonSerializer;
        private readonly string path;
        private readonly Result<GetStreamRequest> request;
        private readonly WireMockServer server;
        private readonly string token;

        public GetStreamTest()
        {
            this.server = WireMockServer.Start();
            this.jsonSerializer = new JsonSerializer();
            this.fixture = new Fixture();
            this.fixture.Create<string>();
            this.token = this.fixture.Create<string>();
            this.request = GetStreamRequest.Parse(this.fixture.Create<string>(), this.fixture.Create<string>(),
                this.fixture.Create<string>());
            this.path = this.GetPathFromRequest();
            var credentials = this.fixture.Create<Credentials>();
            var tokenGenerator = new Mock<ITokenGenerator>();
            tokenGenerator
                .Setup(generator =>
                    generator.GenerateToken(credentials.ApplicationId, credentials.ApplicationKey))
                .Returns(this.token);
            this.client = new SessionClient(credentials, this.server.CreateClient(), tokenGenerator.Object);
        }

        public void Dispose()
        {
            this.server.Stop();
            this.server.Reset();
            GC.SuppressFinalize(this);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = this.jsonSerializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.server
                .Given(this.CreateGetStreamRequest())
                .RespondWith(CreateGetStreamResponse(code,
                    expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().Be(HttpFailure.From(code, message));
        }

        private string GetPathFromRequest() =>
            this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value)),
                (statusCode, jsonError) =>
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            this.server
                .Given(this.CreateGetStreamRequest())
                .RespondWith(CreateGetStreamResponse(code,
                    jsonError));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should()
                .Be(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'."));
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.fixture.Create<GetStreamResponse>();
            this.server
                .Given(this.CreateGetStreamRequest())
                .RespondWith(CreateGetStreamResponse(HttpStatusCode.OK,
                    this.jsonSerializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().BeSuccess(response =>
            {
                response.Id.Should().Be(expectedResponse.Id);
                response.Name.Should().Be(expectedResponse.Name);
                response.VideoType.Should().Be(expectedResponse.VideoType);
                response.LayoutClassList.Should().BeEquivalentTo(expectedResponse.LayoutClassList);
            });
        }

        private IRequestBuilder CreateGetStreamRequest() =>
            WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                .WithPath(this.path)
                .UsingGet();

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.fixture.Create<string>();
            this.server
                .Given(this.CreateGetStreamRequest())
                .RespondWith(CreateGetStreamResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should()
                .Be(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{body}' into '{nameof(GetStreamResponse)}'."));
        }

        private static IResponseBuilder CreateGetStreamResponse(HttpStatusCode code, string body) =>
            Response.Create()
                .WithStatusCode(code)
                .WithBody(body);
    }
}