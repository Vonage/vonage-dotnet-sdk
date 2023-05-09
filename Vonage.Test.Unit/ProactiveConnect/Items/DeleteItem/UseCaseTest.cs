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
using Vonage.ProactiveConnect.Items.DeleteItem;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.DeleteItem
{
    public class UseCaseTest : BaseUseCase, IUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Common.Monads.Unit>>> Operation =>
            configuration => new ProactiveConnectClient(configuration).DeleteItemAsync(this.request);

        private readonly Result<DeleteItemRequest> request;

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
                .VerifyReturnsFailureGivenRequestIsFailure<DeleteItemRequest, Common.Monads.Unit>(
                    (configuration, failureRequest) =>
                        new ProactiveConnectClient(configuration).DeleteItemAsync(failureRequest));

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

        private static Result<DeleteItemRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteItemRequest.Build().WithListId(fixture.Create<Guid>()).WithItemId(fixture.Create<Guid>()).Create();
    }
}