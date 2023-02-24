﻿using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using Vonage.Server.Video.Broadcast.StartBroadcast;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.StartBroadcast
{
    public class StartBroadcastTest
    {
        private readonly BroadcastClient client;

        private Func<Task<Result<Server.Video.Broadcast.Common.Broadcast>>> Operation =>
            () => this.client.StartBroadcastsAsync(this.request);

        private readonly Result<StartBroadcastRequest> request;
        private readonly UseCaseHelper helper;

        public StartBroadcastTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new BroadcastClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetBroadcastsRequest, GetBroadcastsResponse>(
                this
                    .client
                    .GetBroadcastsAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<StartBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            StartBroadcastRequestBuilder.Build(fixture.Create<Guid>())
                .WithSessionId(fixture.Create<string>())
                .WithLayout(new Layout(null, null, LayoutType.HorizontalPresentation))
                .WithOutputs(fixture.Create<StartBroadcastRequest.BroadcastOutput>())
                .Create();

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }
    }
}