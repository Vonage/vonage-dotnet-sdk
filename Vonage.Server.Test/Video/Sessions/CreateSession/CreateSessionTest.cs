using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.CreateSession;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest
    {
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly CreateSessionResponse session;

        private Func<Task<Result<CreateSessionResponse>>> Operation =>
            () => this.client.CreateSessionAsync(this.request);

        private readonly SessionClient client;
        private readonly UseCaseHelper helper;

        public CreateSessionTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
            this.session = this.helper.Fixture.Create<CreateSessionResponse>();
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<CreateSessionRequest, CreateSessionResponse>(
                this.client
                    .CreateSessionAsync);

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.Operation();
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(new[]
            {
                this.session, this.helper.Fixture.Create<CreateSessionResponse>(),
                this.helper.Fixture.Create<CreateSessionResponse>(),
            });
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.Operation();
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(new[] {this.session});
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.Operation();
            result.Should().BeSuccess(this.session);
        }

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, this.request.GetEndpointPath(), this.request.GetUrlEncoded())
                .UsingPost();
    }
}