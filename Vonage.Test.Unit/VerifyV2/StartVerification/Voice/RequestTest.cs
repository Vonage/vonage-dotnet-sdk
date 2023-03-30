using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Voice;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Voice
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequestBuilder.ForVoice()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(VoiceWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/verify");
    }
}