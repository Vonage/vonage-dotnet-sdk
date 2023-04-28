using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Items.ImportItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ImportItems
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<ImportItemsResponse>>> Operation =>
            configuration => new ProactiveConnectClient(configuration).ImportItemsAsync(this.request);

        private ICustomHandlerExpectsRequest customHandler;

        private readonly Result<ImportItemsRequest> request;

        public UseCaseTest()
        {
            this.customHandler = CustomHttpMessageHandler.Build();
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Fact]
        public void ShouldReturnFailure_GivenApiErrorCannotBeParsed()
        {
            var expectedContent = this.helper.Fixture.Create<string>();
            var statusCode = this.helper.Fixture.Create<HttpStatusCode>();
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequest())
                .RespondWith(new MappingResponse
                {
                    Code = statusCode,
                    Content = expectedContent,
                });
            this.Operation(this.BuildConfiguration())
                .Result
                .Should()
                .BeFailure(HttpFailure.From(statusCode,
                    DeserializationFailure.From(typeof(ErrorResponse), expectedContent).GetFailureMessage(),
                    expectedContent));
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed()
        {
            var body = this.helper.Fixture.Create<string>();
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequest())
                .RespondWith(new MappingResponse
                {
                    Code = HttpStatusCode.OK,
                    Content = body,
                });
            var result = await this.Operation(this.BuildConfiguration());
            result.Should().BeFailure(DeserializationFailure.From(typeof(ImportItemsResponse), body));
        }

        [Fact]
        public void ShouldReturnFailure_GivenApiResponseIsError()
        {
            var error = new ErrorResponse(HttpStatusCode.BadRequest, "Some content");
            var errorContent = this.helper.Serializer.SerializeObject(error);
            var expectedResponse = new MappingResponse
            {
                Code = error.Code,
                Content = errorContent,
            };
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequest())
                .RespondWith(expectedResponse);
            this.Operation(this.BuildConfiguration()).Result.Should()
                .BeFailure(HttpFailure.From(error.Code, error.Message, errorContent));
        }

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper
                .VerifyReturnsFailureGivenRequestIsFailure<ImportItemsRequest, ImportItemsResponse>(
                    (_, failureRequest) =>
                        new ProactiveConnectClient(this.BuildConfiguration()).ImportItemsAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess()
        {
            var response = this.helper.Fixture.Create<ImportItemsResponse>();
            this.customHandler = this.customHandler.GivenRequest(this.BuildExpectedRequest())
                .RespondWith(
                    new MappingResponse
                    {
                        Code = HttpStatusCode.OK,
                        Content = this.helper.Serializer.SerializeObject(response),
                    });
            var result = await this.Operation(this.BuildConfiguration());
            result.Should().BeSuccess(response);
        }

        private VonageHttpClientConfiguration BuildConfiguration() =>
            this.customHandler.ToConfiguration(this.helper.Fixture);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<ImportItemsRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ImportItemsRequest.Build().WithListId(fixture.Create<Guid>())
                .WithFileData(fixture.CreateMany<byte>().ToArray()).Create();
    }
}