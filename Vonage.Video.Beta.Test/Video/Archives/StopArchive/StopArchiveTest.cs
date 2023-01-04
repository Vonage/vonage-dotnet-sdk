using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Archives;
using Vonage.Video.Beta.Video.Archives.StopArchive;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.StopArchive
{
    public class StopArchiveTest
    {
        private readonly ArchiveClient client;
        private readonly Result<StopArchiveRequest> request;
        private readonly UseCaseHelper helper;

        public StopArchiveTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new ArchiveClient(this.helper.Server.CreateClient(), () => this.helper.Token);
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
                            () => this.client.StopArchiveAsync(this.request))
                        .Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<Archive>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.StopArchiveAsync(requestValue));
            result.Should().BeSuccess(response =>
                this.helper.Serializer.SerializeObject(response).Should()
                    .Be(this.helper.Serializer.SerializeObject(expectedResponse)));
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(Archive)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.StopArchiveAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure()
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.helper.Fixture.Create<string>());
            var result = await this.client.StopArchiveAsync(Result<StopArchiveRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        private static Result<StopArchiveRequest> BuildRequest(ISpecimenBuilder fixture) =>
            StopArchiveRequest.Parse(fixture.Create<string>(), fixture.Create<string>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.StopArchiveAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingPost();
    }
}