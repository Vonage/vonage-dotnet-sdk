using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
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
using Vonage.SubAccounts.GetSubAccounts;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccounts
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<GetSubAccountsResponse>>> Operation =>
            configuration => new SubAccountsClient(configuration, this.apiKey).GetSubaccounts();

        private readonly string apiKey;

        public UseCaseTest() => this.apiKey = "27ebS990";

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
                .BeFailure(DeserializationFailure.From(typeof(EmbeddedResponse<GetSubAccountsResponse>), body));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private VonageHttpClientConfiguration BuildConfiguration(FakeHttpRequestHandler handler) =>
            new VonageHttpClientConfiguration(
                handler.ToHttpClient(),
                new AuthenticationHeaderValue("Basic", this.helper.Fixture.Create<string>()),
                this.helper.Fixture.Create<string>());

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetSubAccountsRequest.Build(this.apiKey).GetEndpointPath(), UriKind.Relative),
            };
    }
}