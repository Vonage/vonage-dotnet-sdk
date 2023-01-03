﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using WireMock.RequestBuilders;
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
                .Given(this.CreateRequest())
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
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(Array.Empty<CreateSessionResponse>());
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure()
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.helper.Fixture.Create<string>());
            var result =
                await this.client.CreateSessionAsync(Result<CreateSessionRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(error.ToHttpFailure());
        }

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, this.request.GetEndpointPath(), this.request.GetUrlEncoded())
                .UsingPost();
    }
}