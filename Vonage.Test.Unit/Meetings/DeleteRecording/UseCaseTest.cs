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
using Vonage.Meetings.DeleteRecording;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteRecording
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Common.Monads.Unit>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).DeleteRecordingAsync(this.request);

        private readonly Result<DeleteRecordingRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<DeleteRecordingRequest, Common.Monads.Unit>(
                (configuration, failureRequest) =>
                    MeetingsClientFactory.Create(configuration).DeleteRecordingAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<DeleteRecordingRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteRecordingRequest.Parse(fixture.Create<Guid>());
    }
}