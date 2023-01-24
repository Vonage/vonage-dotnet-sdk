using AutoFixture;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class CreateRoomRequestTest
    {
        public CreateRoomRequestTest()
        {
            var fixture = new Fixture();
        }

        // [Fact]
        // public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        //     CreateRoomRequest.Parse()
        //         .Map(request => request.GetEndpointPath())
        //         .Should()
        //         .BeSuccess($"/beta/meetings/rooms");
        //
        // [Theory]
        // [InlineData("")]
        // [InlineData(" ")]
        // [InlineData(null)]
        // public void Parse_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string value) =>
        //     CreateRoomRequest.Parse()
        //         .Should()
        //         .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be null or whitespace."));
        //
        // [Fact]
        // public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        //     CreateRoomRequest.Parse()
        //         .Should()
        //         .BeSuccess(request => 0.Should().Be(0));
    }
}