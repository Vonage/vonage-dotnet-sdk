using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Signaling;
using Vonage.Video.Beta.Video.Signaling.SendSignals;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Signaling

{
    public class SendSignalsTest
    {
        private readonly SignalingClient client;
        private readonly JsonSerializer jsonSerializer;
        private readonly string path;
        private readonly Result<SendSignalsRequest> request;
        private readonly WireMockServer server;
        private readonly string token;

        public SendSignalsTest()
        {
            this.server = WireMockServer.Start();
            this.jsonSerializer = new JsonSerializer();
            var fixture = new Fixture();
            this.token = fixture.Create<string>();
            this.request = SendSignalsRequest.Parse(fixture.Create<string>(), fixture.Create<string>(),
                fixture.Create<SendSignalsRequest.SignalContent>());
            this.path = this.request.Match(value => value.GetEndpointPath(), _ => string.Empty);
            this.client = new SignalingClient(this.server.CreateClient(), () => this.token);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = this.jsonSerializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.server.Given(this.CreateRequest()).RespondWith(CreateResponse(code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.SendSignalsAsync(requestValue));
            result.Should().BeFailure(HttpFailure.From(code, message));
        }

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.jsonSerializer.SerializeObject(value.Content))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token).WithPath(this.path)
                .WithBody(serializedItems).UsingPut();
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value)),
                (statusCode, jsonError) =>
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var expectedFailureMessage = $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'.";
            this.server.Given(this.CreateRequest()).RespondWith(CreateResponse(code, jsonError));
            var result =
                await this.request.BindAsync(requestValue => this.client.SendSignalsAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            this.server
                .Given(this.CreateRequest())
                .RespondWith(CreateResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.SendSignalsAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        private static IResponseBuilder CreateResponse(HttpStatusCode code, string body) =>
            CreateResponse(code).WithBody(body);

        private static IResponseBuilder CreateResponse(HttpStatusCode code) =>
            Response.Create().WithStatusCode(code);
    }
}