using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.ChangeStreamLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.ChangeStreamLayout
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new SessionClient(configuration).ChangeStreamLayoutAsync(this.request);

        private readonly Result<ChangeStreamLayoutRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<ChangeStreamLayoutRequest, Unit>(
                (configuration, failureRequest) =>
                    new SessionClient(configuration).ChangeStreamLayoutAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(new {value.Items}))
                    .IfFailure(string.Empty),
            };

        private static Result<ChangeStreamLayoutRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ChangeStreamLayoutRequest.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithSessionId(fixture.Create<string>())
                .WithItem(fixture.Create<ChangeStreamLayoutRequest.LayoutItem>())
                .Create();
    }
}