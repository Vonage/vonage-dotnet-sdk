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
using Vonage.Video.Beta.Video.Moderation.DisconnectConnection;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Moderation.DisconnectConnection
{
    public class DisconnectConnectionTest
    {
        private readonly ModerationClient client;
        private readonly Result<DisconnectConnectionRequest> request;
        private readonly UseCaseHelper helper;

        public DisconnectConnectionTest()
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
                    this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(
                            this.CreateRequest(),
                            WireMockExtensions.CreateResponse(statusCode, jsonError),
                            jsonError,
                            () => this.client.DisconnectConnectionAsync(this.request))
                        .Wait());

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

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure()
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.helper.Fixture.Create<string>());
            var result =
                await this.client.DisconnectConnectionAsync(
                    Result<DisconnectConnectionRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        private static Result<DisconnectConnectionRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DisconnectConnectionRequest.Parse(fixture.Create<string>(), fixture.Create<string>(),
                fixture.Create<string>());

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

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request))
                .UsingDelete();
    }
}