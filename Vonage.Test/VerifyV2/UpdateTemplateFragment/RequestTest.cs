#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplateFragment;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplateFragment;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(RequestBuilderTest.ValidTemplateId)
            .WithFragmentId(RequestBuilderTest.ValidTemplateFragmentId)
            .WithText(RequestBuilderTest.ValidText)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments/c41a9862-93d6-4c15-b5eb-d5ea6d574654");
}