using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations;
using Vonage.Conversations.GetUserConversations;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Conversations.GetUserConversations;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetUserConversations()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/users/USR-123/conversations")
                .WithParam("page_size", "50")
                .WithParam("order", "desc")
                .WithParam("order_by", "custom-value")
                .WithParam("date_start", "2023-12-18T09:56:08Z")
                .WithParam("include_custom_data", "true")
                .WithParam("state", "joined")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetUserConversationsAsync(GetUserConversationsRequest.Build()
                .WithUserId("USR-123")
                .WithPageSize(50)
                .WithOrder(FetchOrder.Descending)
                .WithOrderBy("custom-value")
                .WithStartDate(DateTimeOffset.Parse("2023-12-18T09:56:08.152Z", CultureInfo.InvariantCulture))
                .WithState(State.Joined)
                .IncludeCustomData()
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task GetUserConversationsMinimal()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/users/USR-123/conversations")
                .WithParam("page_size", "10")
                .WithParam("order", "asc")
                .WithParam("order_by", "created")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .GetUserConversationsAsync(GetUserConversationsRequest.Build()
                .WithUserId("USR-123")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}