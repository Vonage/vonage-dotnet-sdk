using Vonage.ApplicationsNew.UpdateApplication;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ApplicationsNew.UpdateApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        UpdateApplicationRequest.Build()
            .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
            .WithName("My App")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/applications/78d335fa-323d-0114-9c3d-d6f0d48968cf");
}
