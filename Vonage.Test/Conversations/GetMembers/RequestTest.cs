using Vonage.Conversations;
using Vonage.Conversations.GetMembers;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetMembers;

public class RequestTest
{
    [Theory]
    [InlineData(null, null, "/v1/conversations/CON-123/members?page_size=10&order=asc")]
    [InlineData(50, null, "/v1/conversations/CON-123/members?page_size=50&order=asc")]
    [InlineData(null, FetchOrder.Descending, "/v1/conversations/CON-123/members?page_size=10&order=desc")]
    [InlineData(50, FetchOrder.Descending, "/v1/conversations/CON-123/members?page_size=50&order=desc")]
    public void GetEndpointPath_ShouldReturnApiEndpoint(int? pageSize, FetchOrder? order, string expectedEndpoint)
    {
        var builder = GetMembersRequest.Build().WithConversationId("CON-123");
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }
        
        if (order.HasValue)
        {
            builder = builder.WithOrder(order.Value);
        }
        
        builder.Create().Map(request => request.GetEndpointPath()).Should().BeSuccess(expectedEndpoint);
    }
}