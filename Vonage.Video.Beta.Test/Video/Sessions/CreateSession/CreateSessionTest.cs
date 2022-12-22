using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest : IDisposable
    {
        private readonly SessionClient client;
        private readonly Fixture fixture;
        private readonly JsonSerializer jsonSerializer;
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly WireMockServer server;
        private readonly CreateSessionResponse session;
        private readonly string token;
        private readonly string path;

        public CreateSessionTest()
        {
            this.server = WireMockServer.Start();
            this.jsonSerializer = new JsonSerializer();
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
            var expectedResponse = this.jsonSerializer.SerializeObject(new[] {this.session});
            this.server
                .Given(WireMockExtensions.CreateRequest(this.token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = this.jsonSerializer.SerializeObject(new[]
            {
                this.session, this.fixture.Create<CreateSessionResponse>(),
                this.fixture.Create<CreateSessionResponse>(),
            });
            this.server
                .Given(WireMockExtensions.CreateRequest(this.token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = this.jsonSerializer.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.server
                .Given(WireMockExtensions.CreateRequest(this.token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = message is null
                ? null
                : this.jsonSerializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.server
                .Given(WireMockExtensions.CreateRequest(this.token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(code, expectedBody));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(HttpFailure.From(code, message ?? string.Empty));
        }
    }
}