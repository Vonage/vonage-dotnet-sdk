#region
using AutoFixture;
using Vonage.Meetings.CreateRoom;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.CreateRoom;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly string displayName;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.displayName = fixture.Create<string>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateRoomRequest
            .Build()
            .WithDisplayName(this.displayName)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/meetings/rooms");
}