﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Archives.GetArchive;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchive
{
    public class GetArchiveTest
    {
        private readonly ArchiveClient client;

        private Func<Task<Result<Archive>>> Operation => () => this.client.GetArchiveAsync(this.request);
        private readonly Result<GetArchiveRequest> request;
        private readonly UseCaseHelper helper;

        public GetArchiveTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new ArchiveClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(Archive)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.Operation();
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetArchiveRequest, Archive>(this.client
                .GetArchiveAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<Archive>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.Operation();
            result.Should().BeSuccess(response =>
                this.helper.Serializer.SerializeObject(response).Should()
                    .Be(this.helper.Serializer.SerializeObject(expectedResponse)));
        }

        private static Result<GetArchiveRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetArchiveRequest.Parse(fixture.Create<string>(), fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();
    }
}