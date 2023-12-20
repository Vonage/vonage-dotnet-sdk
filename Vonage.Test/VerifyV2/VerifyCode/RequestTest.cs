using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;

namespace Vonage.Test.VerifyV2.VerifyCode
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            VerifyCodeRequest.Build()
                .WithRequestId(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
                .WithCode(this.fixture.Create<string>())
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v2/verify/06547d61-7ac0-43bb-94bd-503b24b2a3a5");
    }
}