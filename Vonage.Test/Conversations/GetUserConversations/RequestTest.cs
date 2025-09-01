#region
using System;
using System.Globalization;
using Vonage.Conversations;
using Vonage.Conversations.GetUserConversations;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetUserConversations;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, null, null, false, null,
        "/v1/users/US-123/conversations?page_size=10&order=asc&order_by=created")]
    [InlineData(50, null, null, null, false, null,
        "/v1/users/US-123/conversations?page_size=50&order=asc&order_by=created")]
    [InlineData(null, FetchOrder.Descending, null, null, false, null,
        "/v1/users/US-123/conversations?page_size=10&order=desc&order_by=created")]
    [InlineData(null, null, "other", null, false, null,
        "/v1/users/US-123/conversations?page_size=10&order=asc&order_by=other")]
    [InlineData(null, null, null, "2023-12-18T09:56:08.152Z", false, null,
        "/v1/users/US-123/conversations?page_size=10&order=asc&order_by=created&date_start=2023-12-18T09%3A56%3A08Z")]
    [InlineData(null, null, null, null, true, null,
        "/v1/users/US-123/conversations?page_size=10&order=asc&order_by=created&include_custom_data=true")]
    [InlineData(null, null, null, null, false, State.Invited,
        "/v1/users/US-123/conversations?page_size=10&order=asc&order_by=created&state=invited")]
    [InlineData(50, FetchOrder.Descending, "other", "2023-12-18T09:56:08.152Z", true, State.Joined,
        "/v1/users/US-123/conversations?page_size=50&order=desc&order_by=other&date_start=2023-12-18T09%3A56%3A08Z&include_custom_data=true&state=joined")]
    public void RequestUri_ShouldReturnApiEndpoint(
        int? pageSize,
        FetchOrder? order,
        string orderBy,
        string startDate,
        bool includeCustomData,
        State? state,
        string expectedEndpoint)
    {
        var builder = GetUserConversationsRequest.Build().WithUserId("US-123");
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }

        if (order.HasValue)
        {
            builder = builder.WithOrder(order.Value);
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            builder = builder.WithOrderBy(orderBy);
        }

        if (!string.IsNullOrWhiteSpace(startDate))
        {
            builder = builder.WithStartDate(DateTimeOffset.Parse(startDate, CultureInfo.InvariantCulture));
        }

        if (includeCustomData)
        {
            builder = builder.IncludeCustomData();
        }

        if (state.HasValue)
        {
            builder = builder.WithState(state.Value);
        }

        builder.Create().Map(request => request.BuildRequestMessage().RequestUri!.ToString()).Should()
            .BeSuccess(expectedEndpoint);
    }
}