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
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.SilentAuth
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<StartVerificationResponse>>> Operation =>
            configuration => new VerifyV2Client(configuration).StartVerificationAsync(this.request);

        private readonly Result<StartSilentAuthVerificationRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper
                .VerifyReturnsFailureGivenRequestIsFailure<StartSilentAuthVerificationRequest,
                    StartVerificationResponse>(
                    (configuration, failureRequest) =>
                        new VerifyV2Client(configuration).StartVerificationAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request.GetStringContent().IfFailure(string.Empty),
            };

        private static Result<StartSilentAuthVerificationRequest> BuildRequest(ISpecimenBuilder fixture) =>
            StartVerificationRequestBuilder.ForSilentAuth()
                .WithBrand(fixture.Create<string>())
                .WithWorkflow(SilentAuthWorkflow.Parse("123456789"))
                .Create();
    }
}