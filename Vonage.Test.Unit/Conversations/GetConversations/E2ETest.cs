using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations.GetConversations;
using Vonage.Test.Unit.Common.Extensions;
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
        public async Task GetConversationsFromHalLink()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations")
                    .WithParam("page_size", "50")
                    .WithParam("order", "desc")
                    .WithParam("date_start", "2023-12-18T09:56:08Z")
                    .WithParam("date_end", "2023-12-18T10:56:08Z")
                    .WithParam("cursor", "7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg=")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ConversationsClient
                .GetConversationsAsync(new GetConversationsHalLink(new Uri(
                        "https://api.nexmo.com/v1/users?order=desc&page_size=50&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D&date_start=2023-12-18T09%3A56%3A08Z&date_end=2023-12-18T10%3A56%3A08Z"))
                    .BuildRequest())
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