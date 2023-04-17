using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
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
                .Build(this.displayName)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/rooms");
    }
}