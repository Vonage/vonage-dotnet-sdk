using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetAvailableRooms;
using Vonage.Server.Test.Video;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetAvailableRooms
{
    public class GetAvailableRoomsTest
    {
        private readonly MeetingsClient client;
        private readonly UseCaseHelper helper;
        private readonly GetAvailableRoomsRequest request;

        public GetAvailableRoomsTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new MeetingsClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetNonEmptyStrings(),
                (statusCode, jsonError) =>
                    this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(
                            this.CreateRequest(),
                            WireMockExtensions.CreateResponse(statusCode, jsonError),
                            jsonError,
                            () => this.client.GetAvailableRoomsAsync(this.request))
                        .Wait());

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(GetAvailableRoomsResponse)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.client.GetAvailableRoomsAsync(this.request);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<GetAvailableRoomsResponse>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.client.GetAvailableRoomsAsync(this.request);
            result.Should().BeSuccess(response =>
            {
                this.helper.Serializer.SerializeObject(response).Should()
                    .Be(this.helper.Serializer.SerializeObject(expectedResponse));
            });
        }

        private static GetAvailableRoomsRequest BuildRequest(ISpecimenBuilder fixture) =>
            GetAvailableRoomsRequest.Build(fixture.Create<string>(), fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, this.request.GetEndpointPath()).UsingGet();

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.client.GetAvailableRoomsAsync(this.request);
            result.Should().BeFailure(error.ToHttpFailure());
        }
    }
}