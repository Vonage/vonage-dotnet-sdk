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
using Vonage.Server.Video.Sip;
using Vonage.Server.Video.Sip.PlayToneIntoConnection;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoConnection
{
    public class PlayToneIntoConnectionTest
    {
        private Func<Task<Result<Unit>>> Operation => () => this.client.PlayToneIntoConnectionAsync(this.request);
        private readonly Result<PlayToneIntoConnectionRequest> request;
        private readonly SipClient client;
        private readonly UseCaseHelper helper;

        public PlayToneIntoConnectionTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new SipClient(this.helper.Server.CreateClient(), () => this.helper.Token,
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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<PlayToneIntoConnectionRequest, Unit>(this.client
                .PlayToneIntoConnectionAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<PlayToneIntoConnectionRequest> BuildRequest(ISpecimenBuilder fixture) =>
            PlayToneIntoConnectionRequest.Parse(
                fixture.Create<Guid>(),
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<string>());

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }
    }
}