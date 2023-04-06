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
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using Vonage.Server.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.StartBroadcast
{
    public class StartBroadcastTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Server.Video.Broadcast.Common.Broadcast>>> Operation =>
            configuration => new BroadcastClient(configuration).StartBroadcastsAsync(this.request);

        private readonly Result<StartBroadcastRequest> request;

        public StartBroadcastTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.Helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<GetBroadcastsRequest, GetBroadcastsResponse>(
                (configuration, failureRequest) =>
                    new BroadcastClient(configuration).GetBroadcastsAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<StartBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            StartBroadcastRequestBuilder.Build(fixture.Create<Guid>())
                .WithSessionId(fixture.Create<string>())
                .WithLayout(new Layout(null, null, LayoutType.HorizontalPresentation))
                .WithOutputs(fixture.Create<StartBroadcastRequest.BroadcastOutput>())
                .Create();
    }
}