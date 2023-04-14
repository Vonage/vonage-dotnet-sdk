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
using Vonage.Server.Common;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.ChangeBroadcastLayout
{
    public class ChangeBroadcastLayoutTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new BroadcastClient(configuration).ChangeBroadcastLayoutAsync(this.request);

        private readonly Result<ChangeBroadcastLayoutRequest> request;

        public ChangeBroadcastLayoutTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<ChangeBroadcastLayoutRequest, Unit>(
                (configuration, failureRequest) =>
                    new BroadcastClient(configuration).ChangeBroadcastLayoutAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value.Layout))
                    .IfFailure(string.Empty),
            };

        private static Result<ChangeBroadcastLayoutRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ChangeBroadcastLayoutRequestBuilder.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<Guid>())
                .WithLayout(fixture.Create<Layout>())
                .Create();
    }
}