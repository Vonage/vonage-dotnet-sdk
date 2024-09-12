#region
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplates;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplates;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldHaveNoDefaultPageSize() =>
        GetTemplatesRequest
            .Build()
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldHaveNoDefaultPage() =>
        GetTemplatesRequest
            .Build()
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetTemplatesRequest
            .Build()
            .WithPageSize(50)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(50);

    [Fact]
    public void Build_ShouldSetPage() =>
        GetTemplatesRequest
            .Build()
            .WithPage(50)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(50);
}