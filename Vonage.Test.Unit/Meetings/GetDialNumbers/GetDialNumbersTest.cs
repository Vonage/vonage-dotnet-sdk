using System;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetDialNumbers;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
{
    public class GetDialNumbersTest
    {
        private Func<Task<Result<GetDialNumbersResponse[]>>> Operation =>
            () => this.client.GetDialNumbersAsync();

        private readonly MeetingsClient client;
        private readonly UseCaseHelper helper;

        public GetDialNumbersTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new MeetingsClient(this.helper.Server.CreateClient(), () => this.helper.Token);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, GetDialNumbersRequest.Default.GetEndpointPath())
                .UsingGet();
    }
}