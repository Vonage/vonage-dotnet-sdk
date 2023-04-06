using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
{
    public class GetBroadcastTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Server.Video.Broadcast.Common.Broadcast>>> Operation =>
            configuration => new BroadcastClient(configuration).GetBroadcastAsync(this.request);

        private readonly Result<GetBroadcastRequest> request;

        public GetBroadcastTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.Helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper
                .VerifyReturnsFailureGivenRequestIsFailure<GetBroadcastRequest,
                    Server.Video.Broadcast.Common.Broadcast>(
                    (configuration, failureRequest) =>
                        new BroadcastClient(configuration).GetBroadcastAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<GetBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetBroadcastRequestBuilder.Build().WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<Guid>()).Create();
    }
}