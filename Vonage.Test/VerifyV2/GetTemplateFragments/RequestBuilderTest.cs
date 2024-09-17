#region
using System;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplateFragments;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragments;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    internal static readonly Guid ValidTemplateId = new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRequestIsEmpty() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Build_ShouldHaveNoDefaultPageSize() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(ValidTemplateId)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultPage() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(ValidTemplateId)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldSetTemplateId() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(ValidTemplateId)
            .Create()
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(ValidTemplateId);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(ValidTemplateId)
            .WithPageSize(50)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(50);

    [Fact]
    public void Build_ShouldSetPage() =>
        GetTemplateFragmentsRequest
            .Build()
            .WithTemplateId(ValidTemplateId)
            .WithPage(50)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(50);
}