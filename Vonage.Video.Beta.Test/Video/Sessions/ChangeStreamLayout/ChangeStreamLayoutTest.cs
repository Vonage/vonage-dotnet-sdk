using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.ChangeStreamLayout
{
    public class ChangeStreamLayoutTest
    {
        private readonly SessionClient client;
        private readonly Result<ChangeStreamLayoutRequest> request;
        private readonly UseCaseHelper helper;

        public ChangeStreamLayoutTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token);
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
                            () => this.client.ChangeStreamLayoutAsync(this.request))
                        .Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.ChangeStreamLayoutAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<ChangeStreamLayoutRequest, Unit>(this.client
                .ChangeStreamLayoutAsync);

        private static Result<ChangeStreamLayoutRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ChangeStreamLayoutRequest.Parse(
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.CreateMany<ChangeStreamLayoutRequest.LayoutItem>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result =
                await this.request.BindAsync(requestValue => this.client.ChangeStreamLayoutAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(new {value.Items}))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPut();
        }
    }
}