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
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Broadcast.AddStreamToBroadcast;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.AddStreamToBroadcast
{
    public class AddStreamToBroadcastTest
    {
        private readonly BroadcastClient client;

        private Func<Task<Result<Unit>>> Operation => () => this.client.AddStreamToBroadcastAsync(this.request);
        private readonly Result<AddStreamToBroadcastRequest> request;
        private readonly UseCaseHelper helper;

        public AddStreamToBroadcastTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new BroadcastClient(this.helper.Server.CreateClient(), () => this.helper.Token,
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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<AddStreamToBroadcastRequest, Unit>(this.client
                .AddStreamToBroadcastAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<AddStreamToBroadcastRequest> BuildRequest(ISpecimenBuilder fixture) =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithBroadcastId(fixture.Create<Guid>())
                .WithStreamId(fixture.Create<Guid>())
                .Create();

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPatch();
        }
    }
}