using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class UpdateRoomRequestTest
    {
        private readonly string displayName;

        public UpdateRoomRequestTest()
        {
            var fixture = new Fixture();
            this.displayName = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateRoomRequestBuilder
                .Build(this.displayName)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/rooms/{this.displayName}");
    }
}