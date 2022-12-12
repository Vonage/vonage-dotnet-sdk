using System;
using System.Threading.Tasks;
using AutoFixture;
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
        private readonly Fixture fixture;
        private readonly Mock<ITokenGenerator> mockTokenGenerator;
        private readonly WireMockServer server;

        public SessionClientTest()
        {
            this.server = WireMockServer.Start(VideoClient.ApiUrl);
            this.fixture = new Fixture();
            this.mockTokenGenerator = new Mock<ITokenGenerator>();
        }

        public void Dispose()
        {
            this.server.Stop();
            this.server.Reset();
        }

        [Fact]
        public async Task CreateSessionAsync_()
        {
            const MediaMode mediaMode = MediaMode.Routed;
            const ArchiveMode archiveMode = ArchiveMode.Always;
            var location = IpAddress.Empty;
            var credentials = this.fixture.Create<Credentials>();
            var session = this.fixture.Create<CreateSessionResponse>();
            var expectedBody =
                $"location={location.Address}&archiveMode={archiveMode.ToString().ToLowerInvariant()}&p2p.preference=disabled";
            var expectedResponse = JsonConvert.SerializeObject(new[] {session});
            var token = this.fixture.Create<string>();
            this.server
                .Given(BuildRequestWithAuthenticationHeader(token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(expectedBody)
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            this.mockTokenGenerator
                .Setup(generator => generator.GenerateToken(credentials.ApplicationId, credentials.ApplicationKey))
                .Returns(token);
            var client = new SessionClient(credentials, this.server.CreateClient(), this.mockTokenGenerator.Object);
            var result = await CreateSessionRequest
                .Parse(location, mediaMode, archiveMode)
                .BindAsync(createSession => client.CreateSessionAsync(createSession));
            result.Should().Be(session);
        }

        private static IRequestBuilder BuildRequestWithAuthenticationHeader(string token) =>
            WireMock.RequestBuilders.Request.Create()
                .WithHeader("Authorization", $"Bearer {token}");
    }
}