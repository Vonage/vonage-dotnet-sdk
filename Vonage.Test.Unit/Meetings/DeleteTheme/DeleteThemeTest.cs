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
using Vonage.Meetings.DeleteTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class DeleteThemeTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Common.Monads.Unit>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).DeleteThemeAsync(this.request);

        private readonly Result<DeleteThemeRequest> request;

        public DeleteThemeTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<DeleteThemeRequest, Common.Monads.Unit>(
                (configuration, failureRequest) =>
                    MeetingsClientFactory.Create(configuration).DeleteThemeAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<DeleteThemeRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteThemeRequest.Parse(fixture.Create<Guid>(), fixture.Create<bool>());
    }
}