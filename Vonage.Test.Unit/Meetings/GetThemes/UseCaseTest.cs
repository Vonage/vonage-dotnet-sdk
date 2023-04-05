using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class UseCaseTest : BaseUseCase
    {
        private static Func<VonageHttpClientConfiguration, Task<Result<Theme[]>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).GetThemesAsync();

        [Fact]
        public async Task ShouldReturnEmptyArray_GivenResponseIsEmpty()
        {
            var client = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(BuildExpectedRequest())
                .ToHttpClient();
            var configuration = new VonageHttpClientConfiguration(client, () => this.helper.Fixture.Create<string>(),
                this.helper.Fixture.Create<string>());
            var result = await Operation(configuration);
            result.Should().BeSuccess(success => success.Should().BeEmpty());
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(), Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(BuildExpectedRequest(),
                Operation);

        private static ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetThemesRequest.Default.GetEndpointPath(), UriKind.Relative),
            };
    }
}