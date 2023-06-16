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
using Vonage.Common.Test.TestHelpers;
using Vonage.SubAccounts;
using Vonage.SubAccounts.Transfer;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferCredit
{
    public class UseCaseTest : BaseUseCase, IUseCaseWithResponse
    {
        private Func<VonageHttpClientConfiguration, Task<Result<CreditTransfer>>> Operation =>
            configuration => new SubAccountsClient(configuration, ApiKey).TransferCreditAsync(this.request);

        private readonly Result<TransferRequest> request;

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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<TransferRequest, CreditTransfer>(
                (configuration, failureRequest) =>
                    new SubAccountsClient(configuration, ApiKey).TransferCreditAsync(failureRequest));

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
                Method = HttpMethod.Post,
                RequestUri =
                    new Uri(
                        UseCaseHelper.GetPathFromRequest(this.request
                            .Map(incompleteRequest => incompleteRequest.WithApiKey(ApiKey))
                            .Map(incompleteRequest => incompleteRequest.WithEndpoint(TransferRequest.CreditTransfer))),
                        UriKind.Relative),
                Content = this.request.GetStringContent().IfFailure(string.Empty),
            };

        private static Result<TransferRequest> BuildRequest(ISpecimenBuilder fixture) =>
            TransferRequest.Build()
                .WithFrom(fixture.Create<string>())
                .WithTo(fixture.Create<string>())
                .WithAmount(fixture.Create<decimal>())
                .Create();
    }
}