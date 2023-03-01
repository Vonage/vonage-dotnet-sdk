using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class CreateRoomTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Room>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).CreateRoomAsync(this.request);

        private readonly Result<CreateRoomRequest> request;

        public CreateRoomTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<CreateRoomRequest, Room>(
                (configuration, failureRequest) =>
                    MeetingsClientFactory.Create(configuration).CreateRoomAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request.GetStringContent().IfFailure(string.Empty),
            };

        private static Result<CreateRoomRequest> BuildRequest(ISpecimenBuilder fixture) =>
            CreateRoomRequestBuilder.Build(fixture.Create<string>()).Create();
    }
}