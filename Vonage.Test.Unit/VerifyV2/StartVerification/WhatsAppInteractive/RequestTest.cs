using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.WhatsAppInteractive
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/verify");
    }
}