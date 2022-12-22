using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Signaling;
using Vonage.Video.Beta.Video.Signaling.SendSignal;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Signaling.SendSignal
{
    public class SendSignalTest
    {
        private readonly SignalingClient client;
        private readonly Result<SendSignalRequest> request;
        private readonly UseCaseHelper helper;

        public SendSignalTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new SignalingClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                FsCheckExtensions.GetAny<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

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
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.SendSignalAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        private static Result<SendSignalRequest> BuildRequest(ISpecimenBuilder fixture) =>
            SendSignalRequest.Parse(
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<SignalContent>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = message is null
                ? null
                : this.helper.Serializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.helper.Server.Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.SendSignalAsync(requestValue));
            result.Should().BeFailure(HttpFailure.From(code, message ?? string.Empty));
        }

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value.Content))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var expectedFailureMessage = $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'.";
            this.helper.Server.Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(code, jsonError));
            var result =
                await this.request.BindAsync(requestValue => this.client.SendSignalAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }
    }
}