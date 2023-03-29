using AutoFixture;
using Vonage.Common;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.WhatsApp
{
    public class StartWhatsAppVerificationRequestTest
    {
        private readonly Fixture fixture;

        public StartWhatsAppVerificationRequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequestBuilder.ForWhatsApp()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppWorkflow.Parse(PhoneNumber.Parse("123456789")))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/verify");
    }
}