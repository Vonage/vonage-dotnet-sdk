﻿using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.DeleteRecording;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteRecording
{
    public class DeleteRecordingTest
    {
        private Func<Task<Result<Common.Monads.Unit>>> Operation =>
            () => this.client.DeleteRecordingAsync(this.request);

        private readonly MeetingsClient client;
        private readonly Result<DeleteRecordingRequest> request;
        private readonly UseCaseHelper helper;

        public DeleteRecordingTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = MeetingsClientFactory.Create(this.helper);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<DeleteRecordingRequest, Common.Monads.Unit>(this
                .client
                .DeleteRecordingAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<DeleteRecordingRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteRecordingRequest.Parse(fixture.Create<Guid>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request))
                .UsingDelete();
    }
}