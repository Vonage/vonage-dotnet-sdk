using System;
using System.Net.Http;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Meetings.GetDialNumbers;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
{
    public class UseCaseTest : BaseUseCase
    {
        private static Func<VonageHttpClientConfiguration, Task<Result<GetDialNumbersResponse[]>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).GetDialNumbersAsync();

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(), Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(BuildExpectedRequest(),
                Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
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
                RequestUri = new Uri(GetDialNumbersRequest.Default.GetEndpointPath(), UriKind.Relative),
            };
    }
}