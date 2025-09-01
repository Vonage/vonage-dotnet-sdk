#region
using Vonage.Conversations;
using Vonage.Conversations.GetEvents;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetEvents;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, null, null, null, false,
        "/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false")]
    [InlineData(50, null, null, null, null, false,
        "/v1/conversations/CON-123/events?page_size=50&order=asc&exclude_deleted_events=false")]
    [InlineData(null, FetchOrder.Descending, null, null, null, false,
        "/v1/conversations/CON-123/events?page_size=10&order=desc&exclude_deleted_events=false")]
    [InlineData(null, null, "123", null, null, false,
        "/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false&start_id=123")]
    [InlineData(null, null, null, "123", null, false,
        "/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false&end_id=123")]
    [InlineData(null, null, null, null, "submitted", false,
        "/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false&event_type=submitted")]
    [InlineData(null, null, null, null, null, true,
        "/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=true")]
    [InlineData(50, FetchOrder.Descending, "123", "456", "submitted", true,
        "/v1/conversations/CON-123/events?page_size=50&order=desc&exclude_deleted_events=true&start_id=123&end_id=456&event_type=submitted")]
    public void RequestUri_ShouldReturnApiEndpoint(int? pageSize, FetchOrder? order, string startId,
        string endId,
        string eventType,
        bool excludeDeletedEvents,
        string expectedEndpoint)
    {
        var builder = GetEventsRequest.Build().WithConversationId("CON-123");
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }

        if (order.HasValue)
        {
            builder = builder.WithOrder(order.Value);
        }

        if (!string.IsNullOrWhiteSpace(startId))
        {
            builder = builder.WithStartId(startId);
        }

        if (!string.IsNullOrWhiteSpace(endId))
        {
            builder = builder.WithEndId(endId);
        }

        if (!string.IsNullOrWhiteSpace(eventType))
        {
            builder = builder.WithEventType(eventType);
        }

        if (excludeDeletedEvents)
        {
            builder = builder.ExcludeDeletedEvents();
        }

        builder.Create().Map(request => request.BuildRequestMessage().RequestUri!.ToString()).Should()
            .BeSuccess(expectedEndpoint);
    }
}