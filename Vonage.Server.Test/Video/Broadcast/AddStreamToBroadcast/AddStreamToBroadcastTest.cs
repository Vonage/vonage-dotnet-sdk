using System;
using System.Net.Http;
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
        private Func<HttpClient, Task<Result<Unit>>> Operation =>
            httpClient => new BroadcastClient(
                    httpClient,
                    () => this.helper.Token,
                    this.helper.Fixture.Create<string>())
                .AddStreamToBroadcastAsync(this.request);

        private readonly Result<AddStreamToBroadcastRequest> request;
        private readonly UseCaseHelperNew helper;

        public AddStreamToBroadcastTest()
        {
            this.helper = new UseCaseHelperNew(JsonSerializerBuilder.Build());
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.GetExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.GetExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<AddStreamToBroadcastRequest, Unit>(
                (client, r) => new BroadcastClient(
                    client,
                    () => this.helper.Token,
                    this.helper.Fixture.Create<string>()).AddStreamToBroadcastAsync(r));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.GetExpectedRequest(), this.Operation);

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

        private ExpectedRequest GetExpectedRequest() =>
            new ExpectedRequest
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };
    }
}