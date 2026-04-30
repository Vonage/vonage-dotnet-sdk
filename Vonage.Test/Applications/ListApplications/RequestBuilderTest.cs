using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
using ListApplicationsRequest = Vonage.Applications.ListApplications.ListApplicationsRequest;

namespace Vonage.Test.Applications.ListApplications;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldHaveNoPageSize_GivenDefault() =>
        ListApplicationsRequest.Build()
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldHaveNoPage_GivenDefault() =>
        ListApplicationsRequest.Build()
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        ListApplicationsRequest.Build()
            .WithPageSize(20)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(20);

    [Fact]
    public void Build_ShouldSetPage() =>
        ListApplicationsRequest.Build()
            .WithPage(3)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(3);
}
