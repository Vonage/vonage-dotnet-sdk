using Vonage.Meetings.GetRooms;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.GetRooms;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, null, "/v1/meetings/rooms")]
    [InlineData(123, null, null, "/v1/meetings/rooms?start_id=123")]
    [InlineData(null, 456, null, "/v1/meetings/rooms?end_id=456")]
    [InlineData(123, 456, null, "/v1/meetings/rooms?start_id=123&end_id=456")]
    [InlineData(null, null, 15, "/v1/meetings/rooms?page_size=15")]
    [InlineData(123, null, 15, "/v1/meetings/rooms?start_id=123&page_size=15")]
    [InlineData(null, 456, 15, "/v1/meetings/rooms?end_id=456&page_size=15")]
    [InlineData(123, 456, 15, "/v1/meetings/rooms?start_id=123&end_id=456&page_size=15")]
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