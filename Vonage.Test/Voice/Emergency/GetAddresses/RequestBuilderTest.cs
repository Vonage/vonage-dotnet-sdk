#region
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddresses;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddresses;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldReturnFailure_GivenPageIsBelowMinimumValue() =>
        GetAddressesRequest.Build()
            .WithPage(0)
            .Create()
            .Should()
            .BeParsingFailure("Page cannot be lower than 1.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsBelowMinimumValue() =>
        GetAddressesRequest.Build()
            .WithPageSize(0)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be lower than 1.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenPageSizeIsAboveMaximumValue() =>
        GetAddressesRequest.Build()
            .WithPageSize(1001)
            .Create()
            .Should()
            .BeParsingFailure("PageSize cannot be higher than 1000.");

    [Fact]
    public void Build_ShouldSetPage() =>
        GetAddressesRequest.Build()
            .WithPage(Constants.ValidPage)
            .WithPageSize(Constants.ValidPageSize)
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(Constants.ValidPage);

    [Fact]
    public void Build_ShouldSetPage_WithDefaultValue() =>
        GetAddressesRequest.Build()
            .Create()
            .Map(request => request.Page)
            .Should()
            .BeSuccess(1);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetAddressesRequest.Build()
            .WithPage(Constants.ValidPage)
            .WithPageSize(Constants.ValidPageSize)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(Constants.ValidPageSize);

    [Fact]
    public void Build_ShouldSetPageSize_WithDefaultValue() =>
        GetAddressesRequest.Build()
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(100);
}