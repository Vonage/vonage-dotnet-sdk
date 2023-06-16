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
using Vonage.SubAccounts.GetCreditTransfers;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetCreditTransfers
{
    public class UseCaseTest : BaseUseCase, IUseCaseWithResponse
    {
        private Func<VonageHttpClientConfiguration, Task<Result<CreditTransfer[]>>> Operation =>
            configuration => new SubAccountsClient(configuration, ApiKey).GetCreditTransfersAsync(this.request);

        private readonly Result<GetCreditTransfersRequest> request;
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
                .BeFailure(DeserializationFailure.From(typeof(EmbeddedResponse<GetCreditTransfersResponse>), body));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetCreditTransfersRequest, CreditTransfer[]>(
                (configuration, failureRequest) =>
                    new SubAccountsClient(configuration, ApiKey).GetCreditTransfersAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<EmbeddedResponse<GetCreditTransfersResponse>>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(this.helper.Serializer.SerializeObject(expectedResponse));
            var result = await this.Operation(this.BuildConfiguration(messageHandler));
            result.Should().BeSuccess(success =>
                success.Should().BeEquivalentTo(expectedResponse.Content.CreditTransfers));
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
                            incompleteRequest.WithApiKey(ApiKey))), UriKind.Relative),
            };

        private static Result<GetCreditTransfersRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetCreditTransfersRequest.Build().WithStartDate(fixture.Create<DateTimeOffset>()).Create();
    }
}