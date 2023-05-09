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
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Lists.GetLists;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetLists
{
    public class UseCaseTest : BaseUseCase, IUseCaseWithResponse
    {
        private Func<VonageHttpClientConfiguration, Task<Result<PaginationResult<EmbeddedLists>>>> Operation =>
            configuration => new ProactiveConnectClient(configuration).GetListsAsync(this.request);

        private readonly Result<GetListsRequest> request;

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
                .VerifyReturnsFailureGivenRequestIsFailure<GetListsRequest, PaginationResult<EmbeddedLists>>(
                    (configuration, failureRequest) =>
                        new ProactiveConnectClient(configuration).GetListsAsync(failureRequest));

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
                Method = HttpMethod.Get,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<GetListsRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetListsRequest.Build().WithPage(fixture.Create<int>()).WithPageSize(fixture.Create<int>()).Create();
    }
}