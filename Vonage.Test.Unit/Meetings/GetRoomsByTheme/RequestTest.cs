using System;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRoomsByTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRoomsByTheme
{
    public class RequestTest
    {
        private readonly Guid themeId;

        public RequestTest() => this.themeId = new Guid("cf7f7327-c8f3-4575-b113-0598571b499a");

        [Theory]
        [InlineData(null, null, null, "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms")]
        [InlineData(123, null, null, "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=123")]
        [InlineData(null, 456, null, "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?end_id=456")]
        [InlineData(123, 456, null,
            "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=123&end_id=456")]
        [InlineData(null, null, 15, "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?page_size=15")]
        [InlineData(123, null, 15,
            "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=123&page_size=15")]
        [InlineData(null, 456, 15,
            "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?end_id=456&page_size=15")]
        [InlineData(123, 456, 15,
            "/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a/rooms?start_id=123&end_id=456&page_size=15")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(int? startId, int? endId, int? pageSize,
            string expectedEndpoint)
        {
            var builder = GetRoomsByThemeRequest.Build().WithThemeId(this.themeId);
            if (startId != null)
            {
                builder = builder.WithStartId(startId.Value);
            }

            if (endId != null)
            {
                builder = builder.WithEndId(endId.Value);
            }

            if (pageSize != null)
            {
                builder = builder.WithPageSize(pageSize.Value);
            }

            builder.Create()
                .Map(request => request.GetEndpointPath())
                .Should().BeSuccess(expectedEndpoint);
        }
    }
}