using System;
using AutoFixture;
using Vonage.Meetings.UpdateRoom;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.UpdateRoom;

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
        UpdateRoomRequest
            .Build()
            .WithRoomId(this.roomId)
            .WithThemeId("Some value")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v1/meetings/rooms/{this.roomId}");
}