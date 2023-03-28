using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Sms
{
    public class StartSmsVerificationRequestTest
    {
        private readonly Fixture fixture;

        public StartSmsVerificationRequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequestBuilder.ForSms()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(new Workflow(this.fixture.Create<string>(), this.fixture.Create<string>()))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/verify");
    }
}