﻿using System;
using System.Globalization;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.GetConversations;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetConversations;

[Trait("Category", "Request")]
public class GetEventsHalLinkTest
{
    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess() =>
        new GetConversationsHalLink(new Uri("https://api.nexmo.com/v1/users?order=desc&page_size=10"))
            .BuildRequest()
            .Should()
            .BeSuccess(new GetConversationsRequest
            {
                StartDate = Maybe<DateTimeOffset>.None,
                Cursor = Maybe<string>.None,
                EndDate = Maybe<DateTimeOffset>.None,
                PageSize = 10,
                Order = FetchOrder.Descending,
            });

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithCursor() =>
        new GetConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
            .BuildRequest()
            .Map(request => request.Cursor)
            .Should()
            .BeSuccess("7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=");

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithEndDate() =>
        new GetConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users?order=desc&page_size=10&date_end=2023-12-18T10%3A56%3A08Z"))
            .BuildRequest()
            .Map(request => request.EndDate)
            .Should()
            .BeSuccess(DateTimeOffset.Parse("2023-12-18T10:56:08Z", CultureInfo.InvariantCulture));

    [Fact]
    public void BuildRequestForPrevious_ShouldReturnSuccess_WithStartDate() =>
        new GetConversationsHalLink(new Uri(
                "https://api.nexmo.com/v1/users?order=desc&page_size=10&date_start=2023-12-18T09%3A56%3A08Z"))
            .BuildRequest()
            .Map(request => request.StartDate)
            .Should()
            .BeSuccess(DateTimeOffset.Parse("2023-12-18T09:56:08Z", CultureInfo.InvariantCulture));
}