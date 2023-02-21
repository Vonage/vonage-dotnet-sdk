using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.GetBroadcast;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
{
    public class GetBroadcastTest
    {
        private readonly BroadcastClient client;

        private Func<Task<Result<Server.Video.Broadcast.Common.Broadcast>>> Operation =>
            () => this.client.GetBroadcastAsync(this.request);

        private readonly Result<GetBroadcastRequest> request;
        private readonly UseCaseHelper helper;

        public GetBroadcastTest()
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
            await this.helper
                .VerifyReturnsFailureGivenRequestIsFailure<GetBroadcastRequest,
                    Server.Video.Broadcast.Common.Broadcast>(
                    this
                        .client
                        .GetBroadcastAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<GetBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetBroadcastRequestBuilder.Build().WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<string>()).Create();

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();
    }
}