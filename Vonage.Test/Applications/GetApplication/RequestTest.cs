using Vonage.Test.Common.Extensions;
using Xunit;
using GetApplicationRequest = Vonage.Applications.GetApplication.GetApplicationRequest;

namespace Vonage.Test.Applications.GetApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetApplicationRequest.Build()
            .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/applications/78d335fa-323d-0114-9c3d-d6f0d48968cf");
}
