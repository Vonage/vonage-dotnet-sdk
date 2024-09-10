#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.CreateTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplate;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_GivenNameIsNullOrWhitespace(string value) =>
        CreateTemplateRequest.Build()
            .WithName(value)
            .Create()
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldSetName() =>
        CreateTemplateRequest.Build()
            .WithName("my-template")
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess("my-template");
}