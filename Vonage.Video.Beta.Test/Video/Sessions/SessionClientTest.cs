using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Moq;
using Newtonsoft.Json;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Voice;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions
{
    public class SessionClientTest : IDisposable
    {
        private readonly SessionClient client;
        private readonly Credentials credentials;
        private readonly Fixture fixture;
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly WireMockServer server;
        private readonly CreateSessionResponse session;
        private readonly string token;

        public SessionClientTest()
        {
            this.server = WireMockServer.Start(VideoClient.ApiUrl);
            this.fixture = new Fixture();
            this.token = this.fixture.Create<string>();
            this.session = this.fixture.Create<CreateSessionResponse>();
            this.credentials = this.fixture.Create<Credentials>();
            var tokenGenerator = new Mock<ITokenGenerator>();
            tokenGenerator
                .Setup(generator =>
                    generator.GenerateToken(this.credentials.ApplicationId, this.credentials.ApplicationKey))
                .Returns(this.token);
            this.client = new SessionClient(this.credentials, this.server.CreateClient(), tokenGenerator.Object);
        }

        public void Dispose()
        {
            this.server.Stop();
            this.server.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task CreateSessionAsync_ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = JsonConvert.SerializeObject(new[] {this.session});
            this.server
                .Given(BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().Be(this.session);
        }

        [Fact]
        public async Task CreateSessionAsync_ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = JsonConvert.SerializeObject(new[]
            {
                this.session, this.fixture.Create<CreateSessionResponse>(),
                this.fixture.Create<CreateSessionResponse>(),
            });
            this.server
                .Given(BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().Be(this.session);
        }

        [Fact]
        public async Task CreateSessionAsync_ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = JsonConvert.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.server
                .Given(BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().Be(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        private async Task ShouldReturnFailureGivenStatusCodeIsFailure(HttpStatusCode statusCode)
        {
            const string expectedResponse = "Some reason session wasn't created.";
            this.server
                .Given(BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(statusCode)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().Be(ResultFailure.FromErrorMessage($"{(int) statusCode} - {expectedResponse}"));
        }

        private static Arbitrary<HttpStatusCode> GetInvalidStatusCodes() => Arb.From<HttpStatusCode>()
            .MapFilter(_ => _, code => (int) code >= 400 && (int) code < 600);

        [Property]
        private Property CreateSessionAsync_ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                GetInvalidStatusCodes(),
                statusCode => this.ShouldReturnFailureGivenStatusCodeIsFailure(statusCode).Wait());

        private static IRequestBuilder BuildRequestWithAuthenticationHeader(string token) =>
            WireMock.RequestBuilders.Request.Create()
                .WithHeader("Authorization", $"Bearer {token}");
    }
}