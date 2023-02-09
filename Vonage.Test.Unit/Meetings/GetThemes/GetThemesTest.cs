﻿using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetThemes;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class GetThemesTest
    {
        private Func<Task<Result<Theme[]>>> Operation =>
            () => this.client.GetThemesAsync();

        private readonly MeetingsClient client;
        private readonly UseCaseHelper helper;

        public GetThemesTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = MeetingsClientFactory.Create(this.helper);
        }

        [Fact]
        public async Task ShouldReturnEmptyArray_GivenResponseIsEmpty()
        {
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(HttpStatusCode.OK));
            var result = await this.Operation();
            result.Should().BeSuccess(success => success.Should().BeEmpty());
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, GetThemesRequest.Default.GetEndpointPath())
                .UsingGet();
    }
}