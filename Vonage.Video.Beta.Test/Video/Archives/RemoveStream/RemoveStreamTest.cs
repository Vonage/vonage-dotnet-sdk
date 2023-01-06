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
using Vonage.Video.Beta.Video.Archives;
using Vonage.Video.Beta.Video.Archives.RemoveStream;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.RemoveStream
{
    public class RemoveStreamTest
    {
        private readonly ArchiveClient client;
        private readonly Result<RemoveStreamRequest> request;
        private readonly UseCaseHelper helper;

        public RemoveStreamTest()
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
                    this.VerifyReturnsFailureGivenErrorCannotBeParsed(statusCode, jsonError).Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK));
            var result =
                await this.request.BindAsync(requestValue => this.client.RemoveStreamAsync(requestValue));
            result.Should().BeSuccess(Unit.Default);
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure()
        {
            var expectedFailure = ResultFailure.FromErrorMessage(this.helper.Fixture.Create<string>());
            var result = await this.client.RemoveStreamAsync(Result<RemoveStreamRequest>.FromFailure(expectedFailure));
            result.Should().BeFailure(expectedFailure);
        }

        private static Result<RemoveStreamRequest> BuildRequest(ISpecimenBuilder fixture) =>
            RemoveStreamRequest.Parse(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server.Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.RemoveStreamAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(new {RemoveStream = value.StreamId}))
                    .Match(_ => _, _ => string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPatch();
        }

        private async Task VerifyReturnsFailureGivenErrorCannotBeParsed(HttpStatusCode code, string jsonError)
        {
            var expectedFailureMessage = $"Unable to deserialize '{jsonError}' into '{nameof(ErrorResponse)}'.";
            this.helper.Server.Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(code, jsonError));
            var result =
                await this.request.BindAsync(requestValue => this.client.RemoveStreamAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }
    }
}