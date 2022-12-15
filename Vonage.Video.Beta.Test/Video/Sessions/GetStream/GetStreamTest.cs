using System;
using System.Linq;
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
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamTest : IDisposable
    {
        private readonly string applicationId;
        private readonly SessionClient client;
        private readonly Fixture fixture;
        private readonly JsonSerializer jsonSerializer;
        private readonly Result<GetStreamRequest> request;
        private readonly WireMockServer server;
        private readonly string sessionId;
        private readonly string streamId;
        private readonly string token;

        public GetStreamTest()
        {
            this.server = WireMockServer.Start();
            this.jsonSerializer = new JsonSerializer();
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
            this.streamId = this.fixture.Create<string>();
            this.token = this.fixture.Create<string>();
            this.request = GetStreamRequest.Parse(this.applicationId, this.sessionId, this.streamId);
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
            var path = this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);
            var errorResponse = new ErrorResponse {Code = ((int) code).ToString(), Message = message};
            var expectedBody = this.jsonSerializer.SerializeObject(errorResponse);
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(path)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode((int) code)
                    .WithBody(expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().Be(HttpFailure.From(code, message));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value)),
                (statusCode, jsonError) =>
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var path = this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(path)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode((int) code)
                    .WithBody(jsonError));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should()
                .Be(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'."));
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var path = this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);
            var expectedResponse = new GetStreamResponse
            {
                Id = this.fixture.Create<string>(),
                Name = this.fixture.Create<string>(),
                VideoType = this.fixture.Create<string>(),
                LayoutClassList = this.fixture.CreateMany<string>().ToArray(),
            };
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(path)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.jsonSerializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().BeSuccess(response =>
            {
                response.Id.Should().Be(expectedResponse.Id);
                response.Name.Should().Be(expectedResponse.Name);
                response.VideoType.Should().Be(expectedResponse.VideoType);
                response.LayoutClassList.Should().BeEquivalentTo(expectedResponse.LayoutClassList);
            });
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.fixture.Create<string>();
            var path = this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(path)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(body));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should()
                .Be(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{body}' into '{nameof(GetStreamResponse)}'."));
        }
    }
}