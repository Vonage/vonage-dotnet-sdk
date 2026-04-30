using Vonage.Test.Common.Extensions;
using Xunit;
using CreateApplicationRequest = Vonage.Applications.CreateApplication.CreateApplicationRequest;

namespace Vonage.Test.Applications.CreateApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        CreateApplicationRequest.Build()
            .WithName("My App")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/applications");
}
