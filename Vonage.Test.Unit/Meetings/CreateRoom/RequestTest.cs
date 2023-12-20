using AutoFixture;
using Vonage.Meetings.CreateRoom;
using Vonage.Test.Unit.Common.Extensions;
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
                .Build()
                .WithDisplayName(this.displayName)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/meetings/rooms");
    }
}