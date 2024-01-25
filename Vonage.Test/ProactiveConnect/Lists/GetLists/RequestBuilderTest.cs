using AutoFixture;
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Lists.GetLists;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.GetLists;

public class RequestBuilderTest
{
    private readonly int page;
    private readonly int pageSize;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.page = fixture.Create<int>();
        this.pageSize = fixture.Create<int>();
    }

    [Fact]
    public void Build_ShouldSetOrderAscending_GivenDefault() =>
        GetListsRequest
            .Build()
            .WithPage(this.page)
            .WithPageSize(this.pageSize)
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Ascending);

    [Fact]
    public void Build_ShouldSetOrderDescending_GivenOrderByDescendingIsUsed() =>
        GetListsRequest
            .Build()
            .WithPage(this.page)
            .WithPageSize(this.pageSize)
            .OrderByDescending()
            .Create()
            .Map(request => request.Order)
            .Should()
            .BeSuccess(FetchOrder.Descending);

    [Fact]
    public void Build_ShouldSetPage() =>
        GetListsRequest
            .Build()
            .WithPage(this.page)
            .WithPageSize(this.pageSize)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(this.page);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetListsRequest
            .Build()
            .WithPage(this.page)
            .WithPageSize(this.pageSize)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(this.pageSize);
}