using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoomsByTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class GetRoomsByThemeRequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRoomsByThemeVonageRequestBuilder.Build("1234")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/themes/1234/rooms");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithEndId() =>
            GetRoomsByThemeVonageRequestBuilder.Build("1234")
                .WithEndId("1234")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/themes/1234/rooms?end_id=1234");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithStartId() =>
            GetRoomsByThemeVonageRequestBuilder.Build("1234")
                .WithStartId("1234")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/themes/1234/rooms?start_id=1234");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithStartIdAndEndId() =>
            GetRoomsByThemeVonageRequestBuilder.Build("1234")
                .WithStartId("1234")
                .WithEndId("5678")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/beta/meetings/themes/1234/rooms?start_id=1234&end_id=5678");
    }
}