using System;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoomsByTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRoomsByThemeRequest.Build()
                .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithEndId() =>
            GetRoomsByThemeRequest.Build()
                .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                .WithEndId(1234)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?end_id=1234");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithStartId() =>
            GetRoomsByThemeRequest.Build()
                .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                .WithStartId(1234)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=1234");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpointWithStartIdAndEndId() =>
            GetRoomsByThemeRequest.Build()
                .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                .WithStartId(1234)
                .WithEndId(5678)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=1234&end_id=5678");
    }
}