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
using Vonage.Server.Video.Sessions.GetStream;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.GetStream
{
    public class GetStreamTest
    {
        private Func<Task<Result<GetStreamResponse>>> Operation => () => this.client.GetStreamAsync(this.request);
        private readonly Result<GetStreamRequest> request;
        private readonly SessionClient client;
        private readonly UseCaseHelper helper;

        public GetStreamTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new SessionClient(this.helper.Server.CreateClient(), () => this.helper.Token);
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<GetStreamRequest, GetStreamResponse>(this.client
                .GetStreamAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<GetStreamRequest> BuildRequest(ISpecimenBuilder fixture) =>
            GetStreamRequest.Parse(fixture.Create<string>(),
                fixture.Create<string>(),
                fixture.Create<string>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request)).UsingGet();
    }
}