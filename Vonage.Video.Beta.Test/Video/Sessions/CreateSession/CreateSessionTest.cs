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
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest
    {
        private readonly SessionClient client;
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly CreateSessionResponse session;
        private readonly UseCaseHelper helper;

        public CreateSessionTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.session = this.helper.Fixture.Create<CreateSessionResponse>();
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(new[] {this.session});
            this.helper.Server
                .Given(WireMockExtensions.CreateRequest(this.helper.Token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(new[]
            {
                this.session, this.helper.Fixture.Create<CreateSessionResponse>(),
                this.helper.Fixture.Create<CreateSessionResponse>(),
            });
            this.helper.Server
                .Given(WireMockExtensions.CreateRequest(this.helper.Token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.helper.Server
                .Given(WireMockExtensions.CreateRequest(this.helper.Token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(WireMockExtensions.CreateRequest(this.helper.Token, this.request.GetEndpointPath()))
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(error.ToHttpFailure());
        }
    }
}