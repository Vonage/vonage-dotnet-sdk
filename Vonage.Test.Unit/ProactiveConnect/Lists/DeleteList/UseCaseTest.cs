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
using Vonage.ProactiveConnect.Lists.DeleteList;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.DeleteList
{
    public class UseCaseTest : BaseUseCase, IUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Common.Monads.Unit>>> Operation =>
            configuration => new ProactiveConnectClient(configuration).DeleteListAsync(this.request);

        private readonly Result<DeleteListRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper
                .VerifyReturnsFailureGivenRequestIsFailure<DeleteListRequest, Common.Monads.Unit>(
                    (configuration, failureRequest) =>
                        new ProactiveConnectClient(configuration).DeleteListAsync(failureRequest));

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

        private static Result<DeleteListRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteListRequest.Parse(fixture.Create<Guid>());
    }
}