#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.CreateTemplateFragment;
using Vonage.VerifyV2.StartVerification;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplateFragment;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateTemplateFragmentRequest.Build()
            .WithTemplateId(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .WithText("my-fragment")
            .WithLocale(Locale.EnUs)
            .WithChannel(VerificationChannel.Sms)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287/template_fragments");
}