using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
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
using Vonage.SubAccounts.GetSubAccounts;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetSubAccounts
{
    public class UseCaseTest : BaseUseCase
    {
        private static Func<VonageHttpClientConfiguration, Task<Result<GetSubAccountsResponse>>> Operation =>
            configuration => new SubAccountsClient(configuration, ApiKey).GetSubAccounts();

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(BuildExpectedRequest())
                .WithResponseContent(body);
            var result = await Operation(this.BuildConfiguration(messageHandler));
            result.Should()
                .BeFailure(DeserializationFailure.From(typeof(EmbeddedResponse<GetSubAccountsResponse>), body));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<EmbeddedResponse<GetSubAccountsResponse>>();
            var messageHandler = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(BuildExpectedRequest())
                .WithResponseContent(this.helper.Serializer.SerializeObject(expectedResponse));
            var result = await Operation(this.BuildConfiguration(messageHandler));
            result.Should().BeSuccess(success =>
            {
                success.PrimaryAccount.Should().Be(expectedResponse.Content.PrimaryAccount);
                success.SubAccounts.Should().BeEquivalentTo(expectedResponse.Content.SubAccounts);
            });
        }

        private VonageHttpClientConfiguration BuildConfiguration(FakeHttpRequestHandler handler) =>
            new VonageHttpClientConfiguration(
                handler.ToHttpClient(),
                new AuthenticationHeaderValue("Basic", this.helper.Fixture.Create<string>()),
                this.helper.Fixture.Create<string>());

        private static ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetSubAccountsRequest.Build(ApiKey).GetEndpointPath(), UriKind.Relative),
            };
    }
}