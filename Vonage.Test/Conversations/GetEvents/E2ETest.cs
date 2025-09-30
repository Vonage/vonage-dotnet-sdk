#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations;
using Vonage.Conversations.GetEvents;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetEvents;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetEvents()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/events")
                .WithParam("page_size", "50")
                .WithParam("order", "desc")
                .WithParam("exclude_deleted_events", "true")
                .WithParam("event_type", "submitted")
                .WithParam("start_id", "123")
                .WithParam("end_id", "456")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetEventsAsync(GetEventsRequest.Build()
                .WithConversationId("CON-123")
                .WithPageSize(50)
                .WithOrder(FetchOrder.Descending)
                .WithEventType("submitted")
                .WithStartId("123")
                .WithEndId("456")
                .ExcludeDeletedEvents()
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetEventsFromHalLink()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/events")
                .WithParam("page_size", "50")
                .WithParam("order", "desc")
                .WithParam("exclude_deleted_events", "true")
                .WithParam("event_type", "submitted")
                .WithParam("start_id", "123")
                .WithParam("end_id", "456")
                .WithParam("cursor", "7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetEventsAsync(new GetEventsHalLink(new Uri(
                    "https://api.nexmo.com/v1/conversations/CON-123/events?page_size=50&order=desc&exclude_deleted_events=true&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&start_id=123&end_id=456&event_type=submitted"))
                .BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetEventsWithDefaultRequest()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/events")
                .WithParam("page_size", "10")
                .WithParam("order", "asc")
                .WithParam("exclude_deleted_events", "false")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetEventsAsync(GetEventsRequest.Build()
                .WithConversationId("CON-123")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}