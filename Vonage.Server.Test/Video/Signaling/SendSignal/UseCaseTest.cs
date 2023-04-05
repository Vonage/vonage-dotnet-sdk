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
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.Common;
using Vonage.Server.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignal
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new SignalingClient(configuration).SendSignalAsync(this.request);

        private readonly Result<SendSignalRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<SendSignalRequest, Unit>(
                (configuration, failureRequest) =>
                    new SignalingClient(configuration).SendSignalAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value.Content))
                    .IfFailure(string.Empty),
            };

        private static Result<SendSignalRequest> BuildRequest(ISpecimenBuilder fixture) =>
            SendSignalRequest.Parse(
                fixture.Create<Guid>(),
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<SignalContent>());
    }
}