using System.Text.Json;
using Vonage.Conversations.CreateEvent;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.CreateEvent;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateEventRequest
            .Build()
            .WithConversationId("CON-123")
            .WithType("submitted")
            .WithBody(JsonSerializer.SerializeToElement(new { }))
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v1/conversations/CON-123/events");
}