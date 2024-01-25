using System;
using System.Globalization;
using Vonage.Conversations;
using Vonage.Conversations.GetConversations;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetConversations;

public class RequestTest
{
    [Theory]
    [InlineData(null, null, null, null, "/v1/conversations?page_size=10&order=asc")]
    [InlineData(50, null, null, null, "/v1/conversations?page_size=50&order=asc")]
    [InlineData(null, FetchOrder.Descending, null, null, "/v1/conversations?page_size=10&order=desc")]
    [InlineData(null, null, "2023-12-18T09:56:08.152Z", null,
        "/v1/conversations?page_size=10&order=asc&date_start=2023-12-18T09%3A56%3A08Z")]
    [InlineData(50, FetchOrder.Descending, "2023-12-18T09:56:08.152Z", null,
        "/v1/conversations?page_size=50&order=desc&date_start=2023-12-18T09%3A56%3A08Z")]
    [InlineData(null, null, null, "2023-12-18T10:56:08.152Z",
        "/v1/conversations?page_size=10&order=asc&date_end=2023-12-18T10%3A56%3A08Z")]
    [InlineData(50, FetchOrder.Descending, "2023-12-18T09:56:08.152Z", "2023-12-18T10:56:08.152Z",
        "/v1/conversations?page_size=50&order=desc&date_start=2023-12-18T09%3A56%3A08Z&date_end=2023-12-18T10%3A56%3A08Z")]
    public void GetEndpointPath_ShouldReturnApiEndpoint(int? pageSize, FetchOrder? order, string startDate,
        string endDate,
        string expectedEndpoint)
    {
        var builder = GetConversationsRequest.Build();
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }

        if (order.HasValue)
        {
            builder = builder.WithOrder(order.Value);
        }

        if (!string.IsNullOrWhiteSpace(startDate))
        {
            builder = builder.WithStartDate(DateTimeOffset.Parse(startDate, CultureInfo.InvariantCulture));
        }

        if (!string.IsNullOrWhiteSpace(endDate))
        {
            builder = builder.WithEndDate(DateTimeOffset.Parse(endDate, CultureInfo.InvariantCulture));
        }

        builder.Create().Map(request => request.GetEndpointPath()).Should().BeSuccess(expectedEndpoint);
    }
}