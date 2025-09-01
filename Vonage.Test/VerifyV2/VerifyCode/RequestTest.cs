#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.VerifyCode;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Fixture fixture;

    public RequestTest() => this.fixture = new Fixture();

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        VerifyCodeRequest.Build()
            .WithRequestId(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
            .WithCode(this.fixture.Create<string>())
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/verify/06547d61-7ac0-43bb-94bd-503b24b2a3a5");
}