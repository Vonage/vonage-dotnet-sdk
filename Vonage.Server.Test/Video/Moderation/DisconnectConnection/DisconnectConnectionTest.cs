using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Server.Common;
using Vonage.Server.Common.Monads;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Moderation;
using Vonage.Server.Video.Moderation.DisconnectConnection;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.DisconnectConnection
{
    public class DisconnectConnectionTest
    {
        private readonly ModerationClient client;
        private readonly UseCaseHelper helper;
        private readonly Result<DisconnectConnectionRequest> request;

        public DisconnectConnectionTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new ModerationClient(this.helper.Server.CreateClient(), () => this.helper.Token);
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
                            () => this.client.DisconnectConnectionAsync(this.request))
                        .Wait());

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<DisconnectConnectionRequest, Unit>(this.client
                .DisconnectConnectionAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.DisconnectConnectionAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        private static Result<DisconnectConnectionRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DisconnectConnectionRequest.Parse(fixture.Create<string>(), fixture.Create<string>(),
                fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request))
                .UsingDelete();

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result =
                await this.request.BindAsync(requestValue => this.client.DisconnectConnectionAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }
    }
}