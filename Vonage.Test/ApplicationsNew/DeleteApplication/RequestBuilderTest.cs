using Vonage.ApplicationsNew.DeleteApplication;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ApplicationsNew.DeleteApplication;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldSetApplicationId() =>
        DeleteApplicationRequest.Build()
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
        DeleteApplicationRequest.Build()
            .WithApplicationId(invalidId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be null or whitespace.");
}
