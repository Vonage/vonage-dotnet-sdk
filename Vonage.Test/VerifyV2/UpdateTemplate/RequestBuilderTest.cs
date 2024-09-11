#region
using System;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplate;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    internal static readonly Guid ValidTemplateId = new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");

    [Fact]
    public void Create_ShouldReturnFailure_GivenIdIsEmpty() =>
        UpdateTemplateRequest.Build()
            .WithId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Create_ShouldSetId() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(ValidTemplateId);

    [Fact]
    public void Create_ShouldSetName() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .WithName("my-template")
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess("my-template");

    [Fact]
    public void Create_ShouldSetDefaultToTrue_GivenSetAsDefaultTemplate() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .SetAsDefaultTemplate()
            .Create()
            .Map(request => request.IsDefault)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Create_ShouldSetDefaultToFalse_GivenSetAsNonDefaultTemplate() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .SetAsNonDefaultTemplate()
            .Create()
            .Map(request => request.IsDefault)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Create_ShouldHaveNoName_GivenDefault() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldHaveNoDefault_GivenDefault() =>
        UpdateTemplateRequest.Build()
            .WithId(ValidTemplateId)
            .Create()
            .Map(request => request.IsDefault)
            .Should()
            .BeSuccess(Maybe<bool>.None);
}