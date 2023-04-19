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
using Vonage.Server.Video.Sip.PlayToneIntoConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoConnection
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new SipClient(configuration).PlayToneIntoConnectionAsync(this.request);

        private readonly Result<PlayToneIntoConnectionRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<PlayToneIntoConnectionRequest, Unit>(
                (configuration, failureRequest) =>
                    new SipClient(configuration).PlayToneIntoConnectionAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
            await this.Helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(value))
                    .IfFailure(string.Empty),
            };

        private static Result<PlayToneIntoConnectionRequest> BuildRequest(ISpecimenBuilder fixture) =>
            PlayToneIntoConnectionRequest.Build()
                .WithApplicationId(fixture.Create<Guid>())
                .WithSessionId(fixture.Create<string>())
                .WithConnectionId(fixture.Create<string>())
                .WithDigits(fixture.Create<string>())
                .Create();
    }
}