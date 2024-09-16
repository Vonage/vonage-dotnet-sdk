#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplateFragment;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplateFragment;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    internal static readonly Guid ValidTemplateId = new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");
    internal static readonly Guid ValidTemplateFragmentId = new Guid("c41a9862-93d6-4c15-b5eb-d5ea6d574654");
    internal static readonly string ValidText = "The authentication code for your ${brand} is: ${code}";

    [Fact]
    public void Create_ShouldReturnFailure_GivenTemplateIdIsEmpty() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(Guid.Empty)
            .WithFragmentId(ValidTemplateFragmentId)
            .WithText(ValidText)
            .Create()
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenTemplateFragmentIdIsEmpty() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(ValidTemplateId)
            .WithFragmentId(Guid.Empty)
            .WithText(ValidText)
            .Create()
            .Should()
            .BeParsingFailure("TemplateFragmentId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ShouldReturnFailure_GivenTextIsNullOrWhitespace(string value) =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(ValidTemplateId)
            .WithFragmentId(ValidTemplateFragmentId)
            .WithText(value)
            .Create()
            .Should()
            .BeParsingFailure("Text cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldSetId() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(ValidTemplateId)
            .WithFragmentId(ValidTemplateFragmentId)
            .WithText(ValidText)
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(ValidTemplateId);

    [Fact]
    public void Create_ShouldSetFragmentId() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(ValidTemplateId)
            .WithFragmentId(ValidTemplateFragmentId)
            .WithText(ValidText)
            .Create()
            .Map(request => request.TemplateFragmentId)
            .Should()
            .BeSuccess(ValidTemplateFragmentId);

    [Fact]
    public void Create_ShouldSetText() =>
        UpdateTemplateFragmentRequest.Build()
            .WithId(ValidTemplateId)
            .WithFragmentId(ValidTemplateFragmentId)
            .WithText(ValidText)
            .Create()
            .Map(request => request.Text)
            .Should()
            .BeSuccess(ValidText);
}