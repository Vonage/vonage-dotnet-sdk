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
    public class GetDialNumbersTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<GetDialNumbersResponse[]>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).GetDialNumbersAsync();

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(BuildExpectedRequest(),
                this.Operation);

        private static ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetDialNumbersRequest.Default.GetEndpointPath(), UriKind.Relative),
            };
    }
}