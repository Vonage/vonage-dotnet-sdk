using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.ChangeStreamLayout
{
    public class ChangeStreamLayoutTest
    {
        private readonly SessionClient client;
        private readonly JsonSerializer jsonSerializer;
        private readonly string path;
        private readonly Result<ChangeStreamLayoutRequest> request;
        private readonly WireMockServer server;
        private readonly string token;

        public ChangeStreamLayoutTest()
        {
            this.server = WireMockServer.Start();
            this.jsonSerializer = new JsonSerializer();
            var fixture = new Fixture();
            this.token = fixture.Create<string>();
            this.request = ChangeStreamLayoutRequest.Parse(fixture.Create<string>(), fixture.Create<string>(),
                fixture.CreateMany<ChangeStreamLayoutRequest.LayoutItem>());
            this.path = this.GetPathFromRequest();
            this.client = new SessionClient(this.server.CreateClient(), () => this.token);
        }

        private string GetPathFromRequest() =>
            this.request.Match(value => value.GetEndpointPath(), failure => string.Empty);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetInvalidStatusCodes(),
                Arb.From<string>(),
                (statusCode, message) => this.VerifyReturnsFailureGivenStatusCodeIsFailure(statusCode, message).Wait());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(HttpStatusCode code, string message)
        {
            var expectedBody = this.jsonSerializer.SerializeObject(new ErrorResponse(((int) code).ToString(), message));
            this.server
                .Given(this.CreateChangeStreamLayoutRequest())
                .RespondWith(CreateChangeStreamLayoutResponse(code, expectedBody));
            var result =
                await this.request.BindAsync(requestValue => this.client.ChangeStreamLayoutAsync(requestValue));
            result.Should().BeFailure(HttpFailure.From(code, message));
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
            this.server
                .Given(this.CreateChangeStreamLayoutRequest())
                .RespondWith(CreateChangeStreamLayoutResponse(code,
                    jsonError));
            var result =
                await this.request.BindAsync(requestValue => this.client.ChangeStreamLayoutAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            this.server
                .Given(this.CreateChangeStreamLayoutRequest())
                .RespondWith(CreateChangeStreamLayoutResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.ChangeStreamLayoutAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        private IRequestBuilder CreateChangeStreamLayoutRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.jsonSerializer.SerializeObject(new {value.Items}))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions.BuildRequestWithAuthenticationHeader(this.token).WithPath(this.path)
                .WithBody(serializedItems).UsingPut();
        }

        private static IResponseBuilder CreateChangeStreamLayoutResponse(HttpStatusCode code, string body) =>
            CreateChangeStreamLayoutResponse(code).WithBody(body);

        private static IResponseBuilder CreateChangeStreamLayoutResponse(HttpStatusCode code) =>
            Response.Create().WithStatusCode(code);
    }
}