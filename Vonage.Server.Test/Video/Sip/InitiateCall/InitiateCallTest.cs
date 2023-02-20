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
using Vonage.Server.Video.Sip.InitiateCall;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class InitiateCallTest
    {
        private Func<Task<Result<InitiateCallResponse>>> Operation => () => this.client.InitiateCallAsync(this.request);
        private readonly Result<InitiateCallRequest> request;
        private readonly SipClient client;
        private readonly UseCaseHelper helper;

        public InitiateCallTest()
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
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<InitiateCallRequest, InitiateCallResponse>(this
                .client
                .InitiateCallAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<InitiateCallRequest> BuildRequest(ISpecimenBuilder fixture) =>
            SipElementBuilder
                .Build(fixture.Create<string>())
                .Create()
                .Bind(sip => InitiateCallRequest.Parse(fixture.Create<Guid>(), fixture.Create<string>(),
                    fixture.Create<string>(), sip));

        private IRequestBuilder CreateRequest()
        {
            var serializedItems =
                this.request
                    .Map(value =>
                        this.helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty);
            return WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request), serializedItems)
                .UsingPost();
        }
    }
}