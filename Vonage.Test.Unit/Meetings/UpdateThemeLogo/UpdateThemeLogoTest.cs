using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateThemeLogo;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateThemeLogo
{
    public class UpdateThemeLogoTest
    {
        private Func<Task<Result<Common.Monads.Unit>>> Operation =>
            () => this.client.UpdateThemeLogoAsync(this.request);

        private readonly MeetingsClient client;
        private readonly UpdateThemeLogoRequest request;
        private readonly UseCaseHelper helper;

        public UpdateThemeLogoTest()
        {
            this.helper = new UseCaseHelper(JsonSerializer.BuildWithSnakeCase());
            this.client = new MeetingsClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
            this.request = UpdateThemeLogoRequest
                .Parse("ca242c86-25e5-46b1-ad75-97ffd67452ea", ThemeLogoType.White, "C:\\ThisIsATest.txt")
                .GetSuccessUnsafe();
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<UpdateThemeLogoRequest, Common.Monads.Unit>(
                this.client
                    .UpdateThemeLogoAsync);

        [Property]
        public Property ShouldReturnFailureWhenFinalizingLogo_GivenApiErrorCannotBeParsed()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            return this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.GetFinalizingRequestBuilder(),
                this.Operation);
        }

        [Property]
        public Property ShouldReturnFailureWhenFinalizingLogo_GivenStatusCodeIsFailure()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            return this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.GetFinalizingRequestBuilder(),
                this.Operation);
        }

        [Property]
        public Property ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.GetUrlRetrievalRequestBuilder(),
                this.Operation);

        [Fact]
        public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage =
                $"Unable to deserialize '{body}' into '{typeof(GetUploadLogosUrlResponse[]).Name}'.";
            this.helper.Server
                .Given(this.GetUrlRetrievalRequestBuilder())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, body));
            var result = await this.Operation();
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenResponseContainsMatchingLogo()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(Array.Empty<GetUploadLogosUrlResponse>());
            this.helper.Server
                .Given(this.GetUrlRetrievalRequestBuilder())
                .RespondWith(WireMockExtensions.CreateResponse(HttpStatusCode.OK, expectedResponse));
            var result = await this.Operation();
            result.Should().BeFailure(ResultFailure.FromErrorMessage(UpdateThemeLogoUseCase.NoMatchingLogo));
        }

        [Property]
        public Property ShouldReturnFailureWhenRetrievingUploadUrls_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.GetUrlRetrievalRequestBuilder(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailureWhenUploadingLogo_GivenApiErrorCannotBeParsed()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            return this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.GetUploadRequestBuilder(),
                this.Operation);
        }

        [Property]
        public Property ShouldReturnFailureWhenUploadingLogo_GivenStatusCodeIsFailure()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            return this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.GetUploadRequestBuilder(),
                this.Operation);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseAreAllSuccesses()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            this.FinalizingLogoReturnsValidResponse();
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.GetFinalizingRequestBuilder(),
                this.Operation);
        }

        private GetUploadLogosUrlResponse[] CreateLogosUrlResponse() => new[]
        {
            new GetUploadLogosUrlResponse
            {
                Url = new Uri($"{this.helper.Server.Url}/beta/upload"),
                Fields = new UploadDetails
                {
                    Bucket = "bucket",
                    Key = "key",
                    Policy = "policy",
                    AmazonAlgorithm = "algorithm",
                    AmazonCredential = "credential",
                    AmazonDate = "date",
                    AmazonSignature = "signature",
                    LogoType = ThemeLogoType.White,
                    ContentType = "content",
                    AmazonSecurityToken = "token",
                },
            },
            new GetUploadLogosUrlResponse
            {
                Url = new Uri("https://wrong-example.com"),
                Fields = new UploadDetails
                {
                    Bucket = "123",
                    Key = "456",
                    Policy = "789",
                    AmazonAlgorithm = "123",
                    AmazonCredential = "456",
                    AmazonDate = "789",
                    AmazonSignature = "123",
                    LogoType = ThemeLogoType.Favicon,
                    ContentType = "456",
                    AmazonSecurityToken = "789",
                },
            },
            new GetUploadLogosUrlResponse
            {
                Url = new Uri("https://wrong-example-2.com"),
                Fields = new UploadDetails
                {
                    Bucket = "bucket",
                    Key = "key",
                    Policy = "policy",
                    AmazonAlgorithm = "algorithm",
                    AmazonCredential = "credential",
                    AmazonDate = "date",
                    AmazonSignature = "signature",
                    LogoType = ThemeLogoType.White,
                    ContentType = "content",
                    AmazonSecurityToken = "token",
                },
            },
        };

        private void FinalizingLogoReturnsValidResponse() =>
            this.helper.Server
                .Given(this.GetFinalizingRequestBuilder())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(HttpStatusCode.OK));

        private IRequestBuilder GetFinalizingRequestBuilder() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token,
                    UseCaseHelper.GetPathFromRequest<FinalizeLogoRequest>(new FinalizeLogoRequest(this.request.ThemeId,
                        this.CreateLogosUrlResponse()[0].Fields.Key)))
                .UsingPut();

        private IRequestBuilder GetUploadRequestBuilder() =>
            WireMock.RequestBuilders.Request
                .Create()
                .WithHeader("Authorization", $"Bearer {this.helper.Token}")
                .WithUrl(this.CreateLogosUrlResponse()[0].Url.AbsoluteUri)
                .UsingPost();

        private IRequestBuilder GetUrlRetrievalRequestBuilder() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token,
                    UseCaseHelper.GetPathFromRequest<GetUploadLogosUrlRequest>(GetUploadLogosUrlRequest.Default))
                .UsingGet();

        private void RetrievingLogosUrlReturnsValidResponse() =>
            this.helper.Server
                .Given(this.GetUrlRetrievalRequestBuilder())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.helper.Serializer.SerializeObject(this.CreateLogosUrlResponse())));

        private void UploadingLogoReturnsValidResponse() =>
            this.helper.Server
                .Given(this.GetUploadRequestBuilder())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(HttpStatusCode.OK));
    }
}