#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.DeleteTemplateFragment;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.DeleteTemplateFragment;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid validId = new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");

    [Fact]
    public void Create_ShouldReturnFailure_GivenTemplateIdIsEmpty() =>
        DeleteTemplateFragmentRequest.Build()
            .WithTemplateId(Guid.Empty)
            .WithTemplateFragmentId(Guid.NewGuid())
            .Create()
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Create_ShouldReturnFailure_GivenTemplateFragmentIdIsEmpty() =>
        DeleteTemplateFragmentRequest.Build()
            .WithTemplateId(Guid.NewGuid())
            .WithTemplateFragmentId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("TemplateFragmentId cannot be empty.");

    [Fact]
    public void Create_ShouldSetTemplateId() =>
        DeleteTemplateFragmentRequest.Build()
            .WithTemplateId(this.validId)
            .WithTemplateFragmentId(Guid.NewGuid())
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(this.validId);

    [Fact]
    public void Create_ShouldSetTemplateFragmentId() =>
        DeleteTemplateFragmentRequest.Build()
            .WithTemplateId(Guid.NewGuid())
            .WithTemplateFragmentId(this.validId)
            .Create()
            .Map(request => request.TemplateFragmentId)
            .Should()
            .BeSuccess(this.validId);
}