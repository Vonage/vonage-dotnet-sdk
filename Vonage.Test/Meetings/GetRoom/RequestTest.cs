#region
using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.GetRoom;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.GetRoom;

[Trait("Category", "Request")]
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
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/meetings/rooms/{this.roomId}");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRoomIdIsEmpty() =>
        GetRoomRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("RoomId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        GetRoomRequest.Parse(this.roomId)
            .Should()
            .BeSuccess(request => request.RoomId.Should().Be(this.roomId));
}