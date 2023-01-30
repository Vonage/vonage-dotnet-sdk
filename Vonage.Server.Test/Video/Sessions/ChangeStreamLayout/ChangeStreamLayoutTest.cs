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
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.ChangeStreamLayout;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.ChangeStreamLayout
{
    public class ChangeStreamLayoutTest
    {
        private Func<Task<Result<Unit>>> Operation => () => this.client.ChangeStreamLayoutAsync(this.request);
        private readonly Result<ChangeStreamLayoutRequest> request;
        private readonly SessionClient client;
        private readonly UseCaseHelper helper;

        public ChangeStreamLayoutTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token,
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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<ChangeStreamLayoutRequest, Unit>(this.client
                .ChangeStreamLayoutAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<ChangeStreamLayoutRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ChangeStreamLayoutRequest.Parse(
                fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.CreateMany<ChangeStreamLayoutRequest.LayoutItem>());

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(new {value.Items}))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPut();
        }
    }
}