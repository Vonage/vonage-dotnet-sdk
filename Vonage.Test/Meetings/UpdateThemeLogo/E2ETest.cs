#region
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateThemeLogo;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.UpdateThemeLogo;

[Trait("Category", "E2E")]
public class E2ETest
{
    private readonly Fixture fixture;

    private readonly UpdateThemeLogoRequest request;
    private readonly JsonSerializer serializer;

    private ICustomHandlerExpectsRequest customHandler;

    public E2ETest()
    {
        this.fixture = new Fixture();
        this.serializer = new JsonSerializer();
        this.fixture.Customize(new SupportMutableValueTypesCustomization());
        this.request = UpdateThemeLogoRequest
            .Parse(new Guid("ca242c86-25e5-46b1-ad75-97ffd67452ea"), ThemeLogoType.White, @"C:\ThisIsATest.txt")
            .GetSuccessUnsafe();
        this.customHandler = CustomHttpMessageHandler.Build();
    }

    private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
        configuration => new MeetingsClient(configuration, InitializeFileSystem()).UpdateThemeLogoAsync(
            this.request);

    [Fact]
    public async Task ShouldReturnFailure_GivenFileDoesNotExist() =>
        (await new MeetingsClient(this.BuildConfiguration(), new MockFileSystem()).UpdateThemeLogoAsync(
            this.request))
        .Should()
        .BeFailure(ResultFailure.FromErrorMessage("The file cannot be found."));

    [Fact]
    public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
        await this.VerifyReturnsFailureGivenRequestIsFailure<UpdateThemeLogoRequest, Unit>(
            (_, failureRequest) =>
                new MeetingsClient(this.BuildConfiguration(), new MockFileSystem()).UpdateThemeLogoAsync(
                    failureRequest));

    [Fact]
    public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
        await this.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

    [Fact]
    public void ShouldReturnFailureWhenFinalizingLogo_GivenApiErrorCannotBeParsed()
    {
        this.RetrievingLogosUrlReturnsValidResponse();
        this.UploadingLogoReturnsValidResponse();
        var expectedContent = this.fixture.Create<string>();
        var statusCode = this.fixture.Create<HttpStatusCode>();
        this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForFinalizing())
            .RespondWith(new MappingResponse
            {
                Code = statusCode,
                Content = expectedContent,
            });
        this.Operation(this.BuildConfiguration())
            .Result
            .Should()
            .BeFailure(HttpFailure.From(statusCode,
                DeserializationFailure.From(typeof(StandardApiError), expectedContent).GetFailureMessage(),
                expectedContent));
    }

    [Fact]
    public void ShouldReturnFailureWhenFinalizingLogo_GivenStatusCodeIsFailure()
    {
        this.RetrievingLogosUrlReturnsValidResponse();
        this.UploadingLogoReturnsValidResponse();
        var error = new StandardApiError("type", "title", "detail", "instance");
        var errorContent = this.serializer.SerializeObject(error);
        var expectedResponse = new MappingResponse
        {
            Code = HttpStatusCode.BadRequest,
            Content = errorContent,
        };
        this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForFinalizing())
            .RespondWith(expectedResponse);
        this.Operation(this.BuildConfiguration())
            .Result
            .Should()
            .BeFailure(HttpFailure.From(HttpStatusCode.BadRequest, error.Title, errorContent));
    }

    [Fact]
    public void ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiErrorCannotBeParsed()
    {
        var expectedContent = this.fixture.Create<string>();
        var statusCode = this.fixture.Create<HttpStatusCode>();
        this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
            .RespondWith(new MappingResponse
            {
                Code = statusCode,
                Content = expectedContent,
            });
        this.Operation(this.BuildConfiguration())
            .Result
            .Should()
            .BeFailure(HttpFailure.From(statusCode,
                DeserializationFailure.From(typeof(StandardApiError), expectedContent).GetFailureMessage(),
                expectedContent));
    }

    [Fact]
    public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenApiResponseCannotBeParsed()
    {
        var body = this.fixture.Create<string>();
        this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
            .RespondWith(new MappingResponse
            {
                Code = HttpStatusCode.OK,
                Content = body,
            });
        var result = await this.Operation(this.BuildConfiguration());
        result.Should().BeFailure(DeserializationFailure.From(typeof(GetUploadLogosUrlResponse[]), body));
    }

    [Fact]
    public async Task ShouldReturnFailureWhenRetrievingUploadUrls_GivenResponseContainsMatchingLogo()
    {
        var expectedResponse = this.serializer.SerializeObject(Array.Empty<GetUploadLogosUrlResponse>());
        this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
            .RespondWith(new MappingResponse
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
        var error = new StandardApiError("type", "title", "detail", "instance");
        var errorContent = this.serializer.SerializeObject(error);
        var expectedResponse = new MappingResponse
        {
            Code = HttpStatusCode.BadRequest,
            Content = errorContent,
        };
        this.customHandler = this.customHandler.GivenRequest(BuildExpectedRequestForUrlRetrieval())
            .RespondWith(expectedResponse);
        this.Operation(this.BuildConfiguration()).Result.Should()
            .BeFailure(HttpFailure.From(HttpStatusCode.BadRequest, error.Title, errorContent));
    }

    [Fact]
    public void ShouldReturnFailureWhenUploadingLogo_GivenApiErrorCannotBeParsed()
    {
        this.RetrievingLogosUrlReturnsValidResponse();
        var expectedContent = this.fixture.Create<string>();
        var statusCode = this.fixture.Create<HttpStatusCode>();
        this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForUploading())
            .RespondWith(new MappingResponse
            {
                Code = statusCode,
                Content = expectedContent,
            });
        this.Operation(this.BuildConfiguration())
            .Result
            .Should()
            .BeFailure(HttpFailure.From(statusCode,
                DeserializationFailure.From(typeof(StandardApiError), expectedContent).GetFailureMessage(),
                expectedContent));
    }

    [Fact]
    public void ShouldReturnFailureWhenUploadingLogo_GivenStatusCodeIsFailure()
    {
        this.RetrievingLogosUrlReturnsValidResponse();
        var error = new StandardApiError("type", "title", "detail", "instance");
        var errorContent = this.serializer.SerializeObject(error);
        var expectedResponse = new MappingResponse
        {
            Code = HttpStatusCode.BadRequest,
            Content = errorContent,
        };
        this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForUploading())
            .RespondWith(expectedResponse);
        this.Operation(this.BuildConfiguration())
            .Result
            .Should()
            .BeFailure(HttpFailure.From(HttpStatusCode.BadRequest, error.Title, errorContent));
    }

    [Fact]
    public async Task ShouldReturnSuccess_GivenApiResponseAreAllSuccesses()
    {
        this.RetrievingLogosUrlReturnsValidResponse();
        this.UploadingLogoReturnsValidResponse();
        this.FinalizingLogoReturnsValidResponse();
        this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequestForFinalizing()).RespondWith(
            new MappingResponse
            {
                Code = HttpStatusCode.OK,
            });
        var result = await this.Operation(this.BuildConfiguration());
        result.Should().BeSuccess(_ => { });
    }

    private VonageHttpClientConfiguration BuildConfiguration() =>
        this.customHandler.ToConfiguration(this.fixture);

    private ExpectedRequest BuildExpectedRequestForFinalizing() =>
        new ExpectedRequest
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(GetPathFromRequest<FinalizeLogoRequest>(new FinalizeLogoRequest(
                this.request.ThemeId,
                this.CreateLogosUrlResponse()[0].Fields.Key)), UriKind.Relative),
        };

    private ExpectedRequest BuildExpectedRequestForUploading() =>
        new ExpectedRequest
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(this.CreateLogosUrlResponse()[0].Url.AbsoluteUri),
            Content = ReadRequestContent<UploadLogoRequest>(
                UploadLogoRequest.FromLogosUrl(this.CreateLogosUrlResponse()[0], Array.Empty<byte>())).Result,
        };

    private static ExpectedRequest BuildExpectedRequestForUrlRetrieval() =>
        new ExpectedRequest
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    GetPathFromRequest<GetUploadLogosUrlRequest>(GetUploadLogosUrlRequest.Default),
                    UriKind.Relative),
        };

    private VonageHttpClientConfiguration CreateConfiguration(FakeHttpRequestHandler handler) =>
        new VonageHttpClientConfiguration(handler.ToHttpClient(), new AuthenticationHeaderValue("Anonymous"),
            this.fixture.Create<string>());

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
            .RespondWith(new MappingResponse {Code = HttpStatusCode.OK});

    private static string GetPathFromRequest<T>(Result<T> request) where T : IVonageRequest =>
        request.Match(value => value.GetEndpointPath(), _ => string.Empty);

    private static MockFileSystem InitializeFileSystem() =>
        new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {@"C:\ThisIsATest.txt", new MockFileData("Bla bla bla.")},
        });

    private static async Task<string> ReadRequestContent<T>(Result<T> request) where T : IVonageRequest =>
        await request
            .Map(value => value.BuildRequestMessage())
            .MapAsync(value => value.Content.ReadAsStringAsync())
            .IfFailure(string.Empty);

    private void RetrievingLogosUrlReturnsValidResponse() =>
        this.customHandler = this.customHandler
            .GivenRequest(BuildExpectedRequestForUrlRetrieval())
            .RespondWith(new MappingResponse
            {
                Code = HttpStatusCode.OK,
                Content = this.serializer.SerializeObject(this.CreateLogosUrlResponse()),
            });

    private void UploadingLogoReturnsValidResponse() =>
        this.customHandler = this.customHandler
            .GivenRequest(this.BuildExpectedRequestForUploading())
            .RespondWith(new MappingResponse
            {
                Code = HttpStatusCode.OK,
            });

    private async Task VerifyReturnsFailureGivenRequestIsFailure<TRequest, TResponse>(
        Func<VonageHttpClientConfiguration, Result<TRequest>, Task<Result<TResponse>>> operation)
    {
        var messageHandler = FakeHttpRequestHandler.Build(HttpStatusCode.OK);
        var expectedFailure = ResultFailure.FromErrorMessage(this.fixture.Create<string>());
        var result = await operation(this.CreateConfiguration(messageHandler),
            Result<TRequest>.FromFailure(expectedFailure));
        result.Should().BeFailure(expectedFailure);
    }

    private async Task VerifyReturnsFailureGivenTokenGenerationFails<TResponse>(
        Func<VonageHttpClientConfiguration, Task<Result<TResponse>>> operation)
    {
        var configuration = new VonageHttpClientConfiguration(
            FakeHttpRequestHandler.Build(HttpStatusCode.OK).ToHttpClient(),
            new AuthenticationFailure().ToResult<AuthenticationHeaderValue>(),
            this.fixture.Create<string>());
        var result = await operation(configuration);
        result.Should().BeFailure(new AuthenticationFailure());
    }
}