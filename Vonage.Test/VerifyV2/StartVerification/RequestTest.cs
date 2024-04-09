using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        StartVerificationRequest.Build()
            .WithBrand("MyBrand")
            .WithWorkflow(SmsWorkflow.Parse("123456789"))
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify");
}