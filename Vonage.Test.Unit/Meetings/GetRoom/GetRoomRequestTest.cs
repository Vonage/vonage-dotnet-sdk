using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoom
{
    public class GetRoomRequestTest
    {
        private readonly string roomId;

        public GetRoomRequestTest()
        {
            var fixture = new Fixture();
            this.roomId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRoomRequest.Parse(this.roomId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/rooms/{this.roomId}");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string value) =>
            GetRoomRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetRoomRequest.Parse(this.roomId)
                .Should()
                .BeSuccess(request => request.RoomId.Should().Be(this.roomId));
    }
}