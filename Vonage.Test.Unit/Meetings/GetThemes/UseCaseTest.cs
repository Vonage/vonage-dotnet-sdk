using System;
using System.Net.Http;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class UseCaseTest : BaseUseCase
    {
        private static Func<VonageHttpClientConfiguration, Task<Result<Theme[]>>> Operation =>
            configuration =>
                MeetingsClientFactory.Create(configuration).GetThemesAsync();

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(BuildExpectedRequest(),
                Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(Operation);

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