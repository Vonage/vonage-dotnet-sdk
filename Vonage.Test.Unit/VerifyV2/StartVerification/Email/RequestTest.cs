using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.Email
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StartVerificationRequestBuilder.ForEmail()
                .WithBrand(this.fixture.Create<string>())
                .WithWorkflow(EmailWorkflow.Parse("alive@company.com"))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v2/verify");
    }
}