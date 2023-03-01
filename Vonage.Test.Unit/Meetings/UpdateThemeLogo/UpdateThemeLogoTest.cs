﻿using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateThemeLogo;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateThemeLogo
{
    public class UpdateThemeLogoTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Common.Monads.Unit>>> Operation =>
            _ => new MeetingsClient(this.BuildConfiguration(), InitializeFileSystem()).UpdateThemeLogoAsync(
                this.request);

        private UpdateThemeLogoMessageHandler.IExpectRequest customHandler;

        private readonly UpdateThemeLogoRequest request;

        public UpdateThemeLogoTest()
        {
            this.request = UpdateThemeLogoRequest
                .Parse(new Guid("ca242c86-25e5-46b1-ad75-97ffd67452ea"), ThemeLogoType.White, @"C:\ThisIsATest.txt")
                .GetSuccessUnsafe();
            this.customHandler = UpdateThemeLogoMessageHandler.Build();
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenFileDoesNotExist() =>
            (await new MeetingsClient(this.BuildConfiguration(), new MockFileSystem()).UpdateThemeLogoAsync(
                this.request))
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("The file cannot be found."));

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<UpdateThemeLogoRequest, Common.Monads.Unit>(
                (_, failureRequest) =>
                    new MeetingsClient(this.BuildConfiguration(), new MockFileSystem()).UpdateThemeLogoAsync(
                        failureRequest));

        [Fact]
        public void ShouldReturnFailureWhenFinalizingLogo_GivenApiErrorCannotBeParsed()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            var expectedContent = this.helper.Fixture.Create<string>();
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForFinalizing())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = this.helper.Fixture.Create<HttpStatusCode>(),
                    Content = expectedContent,
                });
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{expectedContent}' into '{nameof(ErrorResponse)}'."));
        }

        [Fact]
        public void ShouldReturnFailureWhenFinalizingLogo_GivenStatusCodeIsFailure()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            var error = new ErrorResponse(HttpStatusCode.Unauthorized, "Some content.");
            var expectedResponse = new UpdateThemeLogoMessageHandler.ExpectedResponse
            {
                Code = error.Code,
                Content = this.helper.Serializer.SerializeObject(error),
            };
            this.customHandler.GivenRequest(this.BuildExpectedRequestForFinalizing())
                .RespondWith(expectedResponse);
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(error.ToHttpFailure());
        }

        [Fact]
        public void ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiErrorCannotBeParsed()
        {
            var expectedContent = this.helper.Fixture.Create<string>();
            this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = this.helper.Fixture.Create<HttpStatusCode>(),
                    Content = expectedContent,
                });
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{expectedContent}' into '{nameof(ErrorResponse)}'."));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            var expectedFailureMessage =
                $"Unable to deserialize '{body}' into '{typeof(GetUploadLogosUrlResponse[]).Name}'.";
            this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = HttpStatusCode.OK,
                    Content = body,
                });
            var result = await this.Operation(this.BuildConfiguration());
            result.Should().BeFailure(ResultFailure.FromErrorMessage(expectedFailureMessage));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenResponseContainsMatchingLogo()
        {
            var expectedResponse = this.helper.Serializer.SerializeObject(Array.Empty<GetUploadLogosUrlResponse>());
            this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = HttpStatusCode.OK,
                    Content = expectedResponse,
                });
            var result = await this.Operation(this.BuildConfiguration());
            result.Should().BeFailure(ResultFailure.FromErrorMessage(UpdateThemeLogoUseCase.NoMatchingLogo));
        }

        [Fact]
        public void ShouldReturnFailureWhenRetrievingUploadUrls_GivenStatusCodeIsFailure()
        {
            var error = new ErrorResponse(HttpStatusCode.BadRequest, "Some content");
            var expectedResponse = new UpdateThemeLogoMessageHandler.ExpectedResponse
            {
                Code = error.Code,
                Content = this.helper.Serializer.SerializeObject(error),
            };
            this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval()).RespondWith(expectedResponse);
            this.Operation(this.BuildConfiguration()).Result.Should()
                .BeFailure(error.ToHttpFailure());
        }

        [Fact]
        public void ShouldReturnFailureWhenUploadingLogo_GivenApiErrorCannotBeParsed()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            var expectedContent = this.helper.Fixture.Create<string>();
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForUploading())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = this.helper.Fixture.Create<HttpStatusCode>(),
                    Content = expectedContent,
                });
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    $"Unable to deserialize '{expectedContent}' into '{nameof(ErrorResponse)}'."));
        }

        [Fact]
        public void ShouldReturnFailureWhenUploadingLogo_GivenStatusCodeIsFailure()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            var error = new ErrorResponse(HttpStatusCode.Unauthorized, "Some content.");
            var expectedResponse = new UpdateThemeLogoMessageHandler.ExpectedResponse
            {
                Code = error.Code,
                Content = this.helper.Serializer.SerializeObject(error),
            };
            this.customHandler.GivenRequest(this.BuildExpectedRequestForUploading())
                .RespondWith(expectedResponse);
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(error.ToHttpFailure());
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseAreAllSuccesses()
        {
            this.RetrievingLogosUrlReturnsValidResponse();
            this.UploadingLogoReturnsValidResponse();
            this.FinalizingLogoReturnsValidResponse();
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(
                this.BuildExpectedRequestForFinalizing(),
                this.Operation);
        }

        private VonageHttpClientConfiguration BuildConfiguration() =>
            new VonageHttpClientConfiguration(this.customHandler.ToHttpClient(),
                () => this.helper.Fixture.Create<string>(), this.helper.Fixture.Create<string>());

        private ExpectedRequest BuildExpectedRequestForFinalizing() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest<FinalizeLogoRequest>(new FinalizeLogoRequest(
                    this.request.ThemeId,
                    this.CreateLogosUrlResponse()[0].Fields.Key)), UriKind.Relative),
            };

        private ExpectedRequest BuildExpectedRequestForUploading() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(this.CreateLogosUrlResponse()[0].Url.AbsoluteUri),
                Content = UseCaseHelper.ReadRequestContent<UploadLogoRequest>(
                    UploadLogoRequest.FromLogosUrl(this.CreateLogosUrlResponse()[0], Array.Empty<byte>())).Result,
            };

        private static ExpectedRequest BuildExpectedRequestForUrlRetrieval() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri(
                        UseCaseHelper.GetPathFromRequest<GetUploadLogosUrlRequest>(GetUploadLogosUrlRequest.Default),
                        UriKind.Relative),
            };

        private GetUploadLogosUrlResponse[] CreateLogosUrlResponse() => new[]
        {
            new GetUploadLogosUrlResponse
            {
                Url = new Uri($"{this.customHandler.BaseUri}/beta/upload"),
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
            this.customHandler = this.customHandler
                .GivenRequest(this.BuildExpectedRequestForFinalizing())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse {Code = HttpStatusCode.OK});

        private static MockFileSystem InitializeFileSystem() =>
            new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"C:\ThisIsATest.txt", new MockFileData("Bla bla bla.")},
            });

        private void RetrievingLogosUrlReturnsValidResponse() =>
            this.customHandler = this.customHandler
                .GivenRequest(BuildExpectedRequestForUrlRetrieval())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = HttpStatusCode.OK,
                    Content = this.helper.Serializer.SerializeObject(this.CreateLogosUrlResponse()),
                });

        private void UploadingLogoReturnsValidResponse() =>
            this.customHandler = this.customHandler
                .GivenRequest(this.BuildExpectedRequestForUploading())
                .RespondWith(new UpdateThemeLogoMessageHandler.ExpectedResponse
                {
                    Code = HttpStatusCode.OK,
                });
    }
}