using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Server.Common;
using Vonage.Server.Common.Failures;
using Vonage.Server.Common.Monads;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Archives.GetArchive;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchive
{
    public class GetArchiveTest
    {
        private readonly ArchiveClient client;
        private readonly UseCaseHelper helper;
        private readonly Result<GetArchiveRequest> request;

        public GetArchiveTest()
        {
            this.helper = new UseCaseHelper();
            this.client = new ArchiveClient(this.helper.Server.CreateClient(), () => this.helper.Token);
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
                            () => this.client.GetArchiveAsync(this.request))
                        .Wait());

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(Archive)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.GetArchiveAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            Prop.ForAll(
                FsCheckExtensions.GetErrorResponses(),
                error => this.VerifyReturnsFailureGivenStatusCodeIsFailure(error).Wait());

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetArchiveRequest, Archive>(this.client
                .GetArchiveAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<Archive>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.GetArchiveAsync(requestValue));
            result.Should().BeSuccess(response =>
                this.helper.Serializer.SerializeObject(response).Should()
                    .Be(this.helper.Serializer.SerializeObject(expectedResponse)));
        }

        private static Result<GetArchiveRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetArchiveRequest.Parse(fixture.Create<string>(), fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.GetArchiveAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }
    }
}