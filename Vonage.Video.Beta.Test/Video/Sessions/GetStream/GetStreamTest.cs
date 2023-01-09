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
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamTest
    {
        private readonly SessionClient client;
        private readonly Result<GetStreamRequest> request;
        private readonly UseCaseHelper helper;

        public GetStreamTest()
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
                            () => this.client.GetStreamAsync(this.request))
                        .Wait());

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var expectedResponse = this.helper.Fixture.Create<GetStreamResponse>();
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK,
                    this.helper.Serializer.SerializeObject(expectedResponse)));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().BeSuccess(response =>
            {
                response.Id.Should().Be(expectedResponse.Id);
                response.Name.Should().Be(expectedResponse.Name);
                response.VideoType.Should().Be(expectedResponse.VideoType);
                response.LayoutClassList.Should().BeEquivalentTo(expectedResponse.LayoutClassList);
            });
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage = $"Unable to deserialize '{body}' into '{nameof(GetStreamResponse)}'.";
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetStreamRequest, GetStreamResponse>(this.client
                .GetStreamAsync);

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();

        private static Result<GetStreamRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetStreamRequest.Parse(fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<string>());

        private async Task VerifyReturnsFailureGivenStatusCodeIsFailure(ErrorResponse error)
        {
            var expectedBody = error.Message is null
                ? null
                : this.helper.Serializer.SerializeObject(error);
            this.helper.Server
                .Given(this.CreateRequest())
                .RespondWith(WireMockExtensions.CreateResponse(error.Code, expectedBody));
            var result = await this.request.BindAsync(requestValue => this.client.GetStreamAsync(requestValue));
            result.Should().BeFailure(error.ToHttpFailure());
        }
    }
}