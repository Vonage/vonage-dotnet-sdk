using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRooms
{
    public class RequestBuilderTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenEndIdIsNullOrWhitespace(string emptyValue) =>
            GetRoomsRequest.Build()
                .WithEndId(emptyValue)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("EndId cannot be null or whitespace."));

        [Property]
        public Property Build_ShouldReturnFailure_GivenPageSizeIsLowerThanZero() =>
            Prop.ForAll(
                Gen.Choose(-100, 0).ToArbitrary(),
                invalidPageSize => GetRoomsRequest.Build()
                    .WithPageSize(invalidPageSize)
                    .Create()
                    .Should()
                    .BeFailure(ResultFailure.FromErrorMessage("PageSize cannot be lower than 1.")));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenStartIdIsNullOrWhitespace(string emptyValue) =>
            GetRoomsRequest.Build()
                .WithStartId(emptyValue)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StartId cannot be null or whitespace."));

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
                .WithEndId("abc123")
                .Create()
                .Map(request => request.EndId)
                .Should()
                .BeSuccess("abc123");

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
                .WithStartId("abc123")
                .Create()
                .Map(request => request.StartId)
                .Should()
                .BeSuccess("abc123");
    }
}