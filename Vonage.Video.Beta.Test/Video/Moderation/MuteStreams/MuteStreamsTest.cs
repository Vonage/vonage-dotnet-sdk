using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Moderation;
using Vonage.Video.Beta.Video.Moderation.MuteStreams;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Moderation.MuteStreams
{
    public class MuteStreamsTest
    {
        private readonly ModerationClient client;
        private readonly Result<MuteStreamsRequest> request;
        private readonly UseCaseHelper helper;

        public MuteStreamsTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new ModerationClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetNonEmptyStrings(),
                (statusCode, jsonError) =>
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<MuteStreamsResponse>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.MuteStreamsAsync(requestValue));
            result.Should().BeSuccess(expectedResponse);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(MuteStreamsResponse)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.MuteStreamsAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value.Configuration))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }

        private static Result<MuteStreamsRequest> BuildRequest(ISpecimenBuilder fixture) =>
            MuteStreamsRequest.Parse(fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<MuteStreamsRequest.MuteStreamsConfiguration>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.MuteStreamsAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var expectedFailureMessage = $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(code, jsonError));
            var result = await this.request.BindAsync(requestValue => this.client.MuteStreamsAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }
    }
}