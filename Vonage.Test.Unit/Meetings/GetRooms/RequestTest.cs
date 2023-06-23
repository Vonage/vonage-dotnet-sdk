using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRooms
{
    public class RequestTest
    {
        [Theory]
        [InlineData(null, null, null, "/meetings/rooms")]
        [InlineData("StartId", null, null, "/meetings/rooms?start_id=StartId")]
        [InlineData(null, "EndId", null, "/meetings/rooms?end_id=EndId")]
        [InlineData("Start Id", "End Id", null, "/meetings/rooms?start_id=Start%20Id&end_id=End%20Id")]
        [InlineData(null, null, 15, "/meetings/rooms?page_size=15")]
        [InlineData("StartId", null, 15, "/meetings/rooms?start_id=StartId&page_size=15")]
        [InlineData(null, "EndId", 15, "/meetings/rooms?end_id=EndId&page_size=15")]
        [InlineData("Start Id", "End Id", 15, "/meetings/rooms?start_id=Start%20Id&end_id=End%20Id&page_size=15")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(string startId, string endId, int? pageSize,
            string expectedEndpoint)
        {
            var builder = GetRoomsRequest.Build();
            if (startId != null)
            {
                builder = builder.WithStartId(startId);
            }

            if (endId != null)
            {
                builder = builder.WithEndId(endId);
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