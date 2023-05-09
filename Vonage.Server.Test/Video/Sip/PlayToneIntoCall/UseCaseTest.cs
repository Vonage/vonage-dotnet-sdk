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
using Vonage.Server.Video.Sip;
using Vonage.Server.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoCall
{
    public class UseCaseTest : BaseUseCase, IUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new SipClient(configuration).PlayToneIntoCallAsync(this.request);

        private readonly Result<PlayToneIntoCallRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<PlayToneIntoCallRequest, Unit>(
                (configuration, failureRequest) =>
                    new SipClient(configuration).PlayToneIntoCallAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<PlayToneIntoCallRequest> BuildRequest(ISpecimenBuilder fixture) =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithSessionId(fixture.Create<string>())
                .WithDigits(fixture.Create<string>())
                .Create();
    }
}