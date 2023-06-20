using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.SubAccounts;
using Vonage.SubAccounts.GetTransfers;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetTransfers.Balance
{
    public class UseCaseTest : BaseUseCase, IUseCaseWithResponse
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Transfer[]>>> Operation =>
            configuration => new SubAccountsClient(configuration, ApiKey).GetBalanceTransfersAsync(this.request);

        private readonly Result<GetTransfersRequest> request;
        public UseCaseTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(body);
            var result = await this.Operation(this.BuildConfiguration(messageHandler));
            result.Should()
                .BeFailure(DeserializationFailure.From(typeof(EmbeddedResponse<GetTransfersResponse>), body));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetTransfersRequest, Transfer[]>(
                (configuration, failureRequest) =>
                    new SubAccountsClient(configuration, ApiKey).GetBalanceTransfersAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<EmbeddedResponse<GetTransfersResponse>>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(this.helper.Serializer.SerializeObject(expectedResponse));
            var result = await this.Operation(this.BuildConfiguration(messageHandler));
            result.Should().BeSuccess(success =>
                success.Should().BeEquivalentTo(expectedResponse.Content.BalanceTransfers));
        }

        private VonageHttpClientConfiguration BuildConfiguration(FakeHttpRequestHandler handler) =>
            new VonageHttpClientConfiguration(
                handler.ToHttpClient(),
                new AuthenticationHeaderValue("Basic", this.helper.Fixture.Create<string>()),
                this.helper.Fixture.Create<string>());

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri(
                        UseCaseHelper.GetPathFromRequest(this.request.Map(incompleteRequest =>
                            incompleteRequest.WithApiKey(ApiKey).WithEndpoint(GetTransfersRequest.BalanceTransfer))),
                        UriKind.Relative),
            };

        private static Result<GetTransfersRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetTransfersRequest.Build().WithStartDate(fixture.Create<DateTimeOffset>()).Create();
    }
}