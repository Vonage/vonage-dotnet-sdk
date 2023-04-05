using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoom
{
    public class RequestTest
    {
        private readonly Guid roomId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.roomId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRoomRequest.Parse(this.roomId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/rooms/{this.roomId}");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenRoomIdIsEmpty() =>
            GetRoomRequest.Parse(Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RoomId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetRoomRequest.Parse(this.roomId)
                .Should()
                .BeSuccess(request => request.RoomId.Should().Be(this.roomId));
    }
}