﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.CreateSession;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest
    {
        private readonly SessionClient client;
        private readonly UseCaseHelper helper;
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly CreateSessionResponse session;

        public CreateSessionTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.session = this.helper.Fixture.Create<CreateSessionResponse>();
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetNonEmptyStrings(),
                (statusCode, jsonError) =>
                    this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(
                            this.CreateRequest(),
                            WireMockExtensions.CreateResponse(statusCode, jsonError),
                            jsonError,
                            () => this.client.CreateSessionAsync(this.request))
                        .Wait());

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
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

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
        public async Task ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(new[] {this.session});
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.client.CreateSessionAsync(this.request);
            result.Should().BeSuccess(this.session);
        }

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, this.request.GetEndpointPath(), this.request.GetUrlEncoded())
                .UsingPost();

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
    }
}