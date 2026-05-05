using Vonage.Test.Common.Extensions;
using Xunit;
using GetApplicationRequest = Vonage.Applications.GetApplication.GetApplicationRequest;

namespace Vonage.Test.Applications.GetApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetApplicationId() =>
        GetApplicationRequest.Build()
            .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess("78d335fa-323d-0114-9c3d-d6f0d48968cf");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string invalidId) =>
        GetApplicationRequest.Build()
            .WithApplicationId(invalidId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be null or whitespace.");
}
