using System;
using System.Globalization;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetUserConversations;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetUserConversations;

public class GetUserConversationsHalLinkTest
{
    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess() =>
        new GetUserConversationsHalLink(
                new Uri("https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10"))
            .BuildRequest()
            .Should()
            .BeSuccess(new GetUserConversationsRequest
            {
                Order = FetchOrder.Descending, PageSize = 10, UserId = "US-123",
                StartDate = Maybe<DateTimeOffset>.None,
                Cursor = Maybe<string>.None,
                State = Maybe<State>.None,
                OrderBy = "created",
                IncludeCustomData = false,
            });

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithCursor() =>
        new GetUserConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
            .BuildRequest()
            .Map(request => request.Cursor)
            .Should()
            .BeSuccess("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=");

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithStartDate() =>
        new GetUserConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10&date_start=2023-12-18T09%3A56%3A08Z"))
            .BuildRequest()
            .Map(request => request.StartDate)
            .Should()
            .BeSuccess(DateTimeOffset.Parse("2023-12-18T09:56:08Z", CultureInfo.InvariantCulture));

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithOrderBy() =>
        new GetUserConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10&order_by=custom"))
            .BuildRequest()
            .Map(request => request.OrderBy)
            .Should()
            .BeSuccess("custom");

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithIncludeCustomData() =>
        new GetUserConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10&include_custom_data=true"))
            .BuildRequest()
            .Map(request => request.IncludeCustomData)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithState() =>
        new GetUserConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users/US-123/conversations?order=desc&page_size=10&state=joined"))
            .BuildRequest()
            .Map(request => request.State)
            .Should()
            .BeSuccess(State.Joined);
}