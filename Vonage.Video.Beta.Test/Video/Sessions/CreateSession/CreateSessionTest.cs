using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Newtonsoft.Json;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest : IDisposable
    {
        private readonly SessionClient client;
        private readonly Fixture fixture;
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly WireMockServer server;
        private readonly CreateSessionResponse session;
        private readonly string token;

        public CreateSessionTest()
        {
            this.server = WireMockServer.Start();
            this.fixture = new Fixture();
            this.token = this.fixture.Create<string>();
            this.session = this.fixture.Create<CreateSessionResponse>();
            this.client = new SessionClient(this.server.CreateClient(), () => this.token);
        }

        public void Dispose()
        {
            this.server.Stop();
            this.server.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = JsonConvert.SerializeObject(new[] {this.session});
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = JsonConvert.SerializeObject(new[]
            {
                this.session, this.fixture.Create<CreateSessionResponse>(),
                this.fixture.Create<CreateSessionResponse>(),
            });
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = JsonConvert.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                statusCode => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode statusCode)
        {
            const string expectedResponse = "Some reason session wasn't created.";
            this.server
                .Given(WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token)
                    .WithPath(CreateSessionRequest.CreateSessionEndpoint)
                    .WithBody(this.request.GetUrlEncoded())
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(statusCode)
                    .WithBody(expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(HttpFailure.From(statusCode, expectedResponse));
        }
    }
}