using FsCheck;
using FsCheck.Xunit;
using Vonage.Meetings.GetRooms;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.GetRooms;

public class RequestBuilderTest
{
    [Property]
    public Property Build_ShouldReturnFailure_GivenPageSizeIsLowerThanZero() =>
        Prop.ForAll(
            Gen.Choose(-100, 0).ToArbitrary(),
            invalidPageSize => GetRoomsRequest.Build()
                .WithPageSize(invalidPageSize)
                .Create()
                .Should()
                .BeParsingFailure("PageSize cannot be lower than 1."));

    [Fact]
    public void Build_ShouldReturnNoEndId_GivenDefault() =>
        GetRoomsRequest.Build()
            .Create()
            .Map(request => request.EndId)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Fact]
    public void Build_ShouldReturnNoPageSize_GivenDefault() =>
        GetRoomsRequest.Build()
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Fact]
    public void Build_ShouldReturnNoStartId_GivenDefault() =>
        GetRoomsRequest.Build()
            .Create()
            .Map(request => request.StartId)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Fact]
    public void Build_ShouldSetEndId() =>
        GetRoomsRequest.Build()
            .WithEndId(123)
            .Create()
            .Map(request => request.EndId)
            .Should()
            .BeSuccess(123);

    [Fact]
    public void Build_ShouldSetPageSize() =>
        GetRoomsRequest.Build()
            .WithPageSize(50)
            .Create()
            .Map(request => request.PageSize)
            .Should()
            .BeSuccess(50);

    [Fact]
    public void Build_ShouldSetStartId() =>
        GetRoomsRequest.Build()
            .WithStartId(123)
            .Create()
            .Map(request => request.StartId)
            .Should()
            .BeSuccess(123);
}