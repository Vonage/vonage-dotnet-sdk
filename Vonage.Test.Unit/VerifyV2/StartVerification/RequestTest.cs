using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequest.Build()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(SmsWorkflow.Parse("123456789"))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v2/verify");
    }
}