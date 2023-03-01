using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class CreateThemeTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Theme>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).CreateThemeAsync(this.request);

        private readonly Result<CreateThemeRequest> request;

        public CreateThemeTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<CreateThemeRequest, Theme>(
                (configuration, failureRequest) =>
                    MeetingsClientFactory.Create(configuration).CreateThemeAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<CreateThemeRequest> BuildRequest(ISpecimenBuilder fixture) =>
            CreateThemeRequestBuilder.Build(fixture.Create<string>(), fixture.Create<Color>()).Create();
    }
}