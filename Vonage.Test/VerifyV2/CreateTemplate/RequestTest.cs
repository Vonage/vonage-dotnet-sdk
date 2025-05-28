#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.CreateTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        CreateTemplateRequest.Build()
            .WithName("MyBrand")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/verify/templates");
}