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
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.AddStreamToBroadcast
{
    public class AddStreamToBroadcastTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new BroadcastClient(configuration).AddStreamToBroadcastAsync(this.request);

        private readonly Result<AddStreamToBroadcastRequest> request;

        public AddStreamToBroadcastTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper
                .VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper
                .VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper
                .VerifyReturnsFailureGivenRequestIsFailure<AddStreamToBroadcastRequest, Unit>(
                    (configuration, failureRequest) =>
                        new BroadcastClient(configuration).AddStreamToBroadcastAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper
                .VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<AddStreamToBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<Guid>())
                .WithStreamId(fixture.Create<Guid>())
                .Create();
    }
}