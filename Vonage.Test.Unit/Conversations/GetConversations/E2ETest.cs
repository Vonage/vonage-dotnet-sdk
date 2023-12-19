using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Conversations.GetConversations;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Conversations.GetConversations
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetConversations()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations")
                    .WithParam("page_size", "50")
                    .WithParam("order", "desc")
                    .WithParam("date_start", "2023-12-18T09:56:08Z")
                    .WithParam("date_end", "2023-12-18T10:56:08Z")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ConversationsClient
                .GetConversationsAsync(GetConversationsRequest.Build()
                    .WithPageSize(50)
                    .WithOrder(FetchOrder.Descending)
                    .WithStartDate(DateTimeOffset.Parse("2023-12-18T09:56:08.152Z", CultureInfo.InvariantCulture))
                    .WithEndDate(DateTimeOffset.Parse("2023-12-18T10:56:08.152Z", CultureInfo.InvariantCulture))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
        }

        [Fact]
        public async Task GetConversationsWithDefaultRequest()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations")
                    .WithParam("page_size", "10")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ConversationsClient
                .GetConversationsAsync(GetConversationsRequest.Build().Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
        }
    }
}