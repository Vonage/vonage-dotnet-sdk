using System;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetThemes;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class GetThemesTest
    {
        private Func<Task<Result<Theme[]>>> Operation =>
            () => this.client.GetThemesAsync();

        private readonly MeetingsClient client;
        private readonly UseCaseHelper helper;

        public GetThemesTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = new MeetingsClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
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
                .CreateRequest(this.helper.Token, GetThemesRequest.Default.GetEndpointPath())
                .UsingGet();
    }
}