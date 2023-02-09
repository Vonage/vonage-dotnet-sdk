using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.GetRoomsByTheme;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class GetRoomsByThemeTest
    {
        private Func<Task<Result<GetRoomsByThemeResponse>>> Operation =>
            () => this.client.GetRoomsByThemeAsync(this.request);

        private readonly MeetingsClient client;

        private readonly Result<GetRoomsByThemeRequest> request;
        private readonly UseCaseHelper helper;

        public GetRoomsByThemeTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = MeetingsClientFactory.Create(this.helper);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<GetRoomsByThemeRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetRoomsByThemeVonageRequestBuilder.Build(fixture.Create<Guid>()).Create();

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();
    }
}