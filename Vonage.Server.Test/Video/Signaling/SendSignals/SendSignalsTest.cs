using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.Common;
using Vonage.Server.Video.Signaling.SendSignals;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignals

{
    public class SendSignalsTest
    {
        private Func<Task<Result<Unit>>> Operation => () => this.client.SendSignalsAsync(this.request);
        private readonly Result<SendSignalsRequest> request;
        private readonly SignalingClient client;
        private readonly UseCaseHelper helper;

        public SendSignalsTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new SignalingClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<SendSignalsRequest, Unit>(this.client
                .SendSignalsAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<SendSignalsRequest> BuildRequest(ISpecimenBuilder fixture) =>
            SendSignalsRequest.Parse(fixture.Create<Guid>(),
                fixture.Create<string>(),
                fixture.Create<SignalContent>());

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value.Content))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }
    }
}