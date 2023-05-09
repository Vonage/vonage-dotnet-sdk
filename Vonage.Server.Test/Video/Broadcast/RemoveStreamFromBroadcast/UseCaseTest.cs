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
using Vonage.Common.Test.TestHelpers;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    public class UseCaseTest : BaseUseCase, IUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new BroadcastClient(configuration).RemoveStreamFromBroadcastAsync(this.request);

        private readonly Result<RemoveStreamFromBroadcastRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<RemoveStreamFromBroadcastRequest, Unit>(
                (configuration, failureRequest) =>
                    new BroadcastClient(configuration).RemoveStreamFromBroadcastAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<RemoveStreamFromBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<Guid>())
                .WithStreamId(fixture.Create<Guid>())
                .Create();
    }
}