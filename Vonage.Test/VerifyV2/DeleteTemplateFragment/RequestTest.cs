#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.DeleteTemplateFragment;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.DeleteTemplateFragment;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        DeleteTemplateFragmentRequest.Build()
            .WithTemplateId(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .WithTemplateFragmentId(new Guid("7e4fea73-afe6-4c34-b3e9-8b5ce2e2253a"))
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287/template_fragments/7e4fea73-afe6-4c34-b3e9-8b5ce2e2253a");
}