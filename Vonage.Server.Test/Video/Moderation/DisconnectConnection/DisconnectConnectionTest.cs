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
using Vonage.Server.Video.Moderation;
using Vonage.Server.Video.Moderation.DisconnectConnection;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.DisconnectConnection
{
    public class DisconnectConnectionTest
    {
        private Func<Task<Result<Unit>>> Operation => () => this.client.DisconnectConnectionAsync(this.request);
        private readonly ModerationClient client;
        private readonly Result<DisconnectConnectionRequest> request;
        private readonly UseCaseHelper helper;

        public DisconnectConnectionTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new ModerationClient(this.helper.Server.CreateClient(), () => this.helper.Token,
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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<DisconnectConnectionRequest, Unit>(this.client
                .DisconnectConnectionAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<DisconnectConnectionRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DisconnectConnectionRequest.Parse(fixture.Create<string>(), fixture.Create<string>(),
                fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request))
                .UsingDelete();
    }
}