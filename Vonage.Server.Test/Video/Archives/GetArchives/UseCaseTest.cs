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
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchives
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<GetArchivesResponse>>> Operation =>
            configuration => new ArchiveClient(configuration).GetArchivesAsync(this.request);

        private readonly Result<GetArchivesRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

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
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<GetArchivesRequest, GetArchivesResponse>(
                (configuration, failureRequest) =>
                    new ArchiveClient(configuration).GetArchivesAsync(failureRequest));

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

        private static Result<GetArchivesRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetArchivesRequest.Build().WithApplicationId(fixture.Create<Guid>()).Create();
    }
}