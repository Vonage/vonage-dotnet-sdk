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
using Vonage.Meetings.Common;
using Vonage.Meetings.GetRoom;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoom
{
    public class GetRoomTest
    {
        private Func<Task<Result<Room>>> Operation => () => this.client.GetRoomAsync(this.request);
        private readonly MeetingsClient client;
        private readonly Result<GetRoomRequest> request;
        private readonly UseCaseHelper helper;

        public GetRoomTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = new MeetingsClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
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
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetRoomRequest, Room>(this.client
                .GetRoomAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<GetRoomRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetRoomRequest.Parse(fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();
    }
}