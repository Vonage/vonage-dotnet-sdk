using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations;
using Vonage.Conversations.GetMembers;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Conversations.GetMembers;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetMembers()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/members")
                .WithParam("page_size", "50")
                .WithParam("order", "desc")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetMembersAsync(GetMembersRequest.Build()
                .WithConversationId("CON-123")
                .WithPageSize(50)
                .WithOrder(FetchOrder.Descending)
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetMembersFromHalLink()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/members")
                .WithParam("page_size", "50")
                .WithParam("order", "desc")
                .WithParam("cursor", "7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetMembersAsync(new GetConversationsHalLink(new Uri(
                    "https://api.nexmo.com/v1/conversations/CON-123/members?order=desc&page_size=50&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"))
                .BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}