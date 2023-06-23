using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRooms
{
    public class RequestTest
    {
        [Theory]
        [InlineData(null, null, null, "/meetings/rooms")]
        [InlineData(123, null, null, "/meetings/rooms?start_id=123")]
        [InlineData(null, 456, null, "/meetings/rooms?end_id=456")]
        [InlineData(123, 456, null, "/meetings/rooms?start_id=123&end_id=456")]
        [InlineData(null, null, 15, "/meetings/rooms?page_size=15")]
        [InlineData(123, null, 15, "/meetings/rooms?start_id=123&page_size=15")]
        [InlineData(null, 456, 15, "/meetings/rooms?end_id=456&page_size=15")]
        [InlineData(123, 456, 15, "/meetings/rooms?start_id=123&end_id=456&page_size=15")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(int? startId, int? endId, int? pageSize,
            string expectedEndpoint)
        {
            var builder = GetRoomsRequest.Build();
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