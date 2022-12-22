using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.GetStreams;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStreams
{
    public class GetStreamsTest
    {
        private readonly SessionClient client;
        private readonly Result<GetStreamsRequest> request;
        private readonly UseCaseHelper helper;

        public GetStreamsTest()
        {
            this.helper = new UseCaseHelper();
            this.request = GetStreamsRequest.Parse(this.helper.Fixture.Create<string>(),
                this.helper.Fixture.Create<string>());
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value)),
                (statusCode, jsonError) =>
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<GetStreamsResponse>();
            this.helper.Server
                .Given(WireMockExtensions
                    .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamsAsync(requestValue));
            result.Should().BeSuccess(response =>
            {
                this.helper.Serializer.SerializeObject(response).Should()
                    .Be(this.helper.Serializer.SerializeObject(expectedResponse));
            });
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(GetStreamsResponse)}'.";
            this.helper.Server
                .Given(WireMockExtensions
                    .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamsAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = message is null
                ? null
                : this.helper.Serializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.helper.Server
                .Given(WireMockExtensions
                    .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet())
                .RespondWith(WireMockExtensions.CreateResponse(code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamsAsync(requestValue));
            result.Should().BeFailure(HttpFailure.From(code, message ?? string.Empty));
        }

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var expectedFailureMessage = $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'.";
            this.helper.Server
                .Given(WireMockExtensions
                    .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet())
                .RespondWith(WireMockExtensions.CreateResponse(code, jsonError));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamsAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }
    }
}