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
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Archives.StopArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.StopArchive
{
    public class StopArchiveTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Archive>>> Operation =>
            configuration => new ArchiveClient(configuration).StopArchiveAsync(this.request);

        private readonly Result<StopArchiveRequest> request;

        public StopArchiveTest() => this.request = BuildRequest(this.Helper.Fixture);

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
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<StopArchiveRequest, Archive>(
                (configuration, failureRequest) =>
                    new ArchiveClient(configuration).StopArchiveAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<StopArchiveRequest> BuildRequest(ISpecimenBuilder fixture) =>
            StopArchiveRequest.Parse(fixture.Create<Guid>(), fixture.Create<Guid>());
    }
}