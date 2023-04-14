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
using Vonage.Server.Video.Sip;
using Vonage.Server.Video.Sip.InitiateCall;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.InitiateCall
{
    public class InitiateCallTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<InitiateCallResponse>>> Operation =>
            configuration => new SipClient(configuration).InitiateCallAsync(this.request);

        private readonly Result<InitiateCallRequest> request;

        public InitiateCallTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<InitiateCallRequest, InitiateCallResponse>(
                (configuration, failureRequest) => new SipClient(configuration).InitiateCallAsync(failureRequest));

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

        private static Result<InitiateCallRequest> BuildRequest(ISpecimenBuilder fixture) =>
            SipElementBuilder
                .Build(fixture.Create<string>())
                .Create()
                .Bind(sip => InitiateCallRequest.Parse(fixture.Create<Guid>(), fixture.Create<string>(),
                    fixture.Create<string>(), sip));
    }
}