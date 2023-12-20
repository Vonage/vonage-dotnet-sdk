using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Conversations.UpdateConversation;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Conversations.UpdateConversation
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public Task UpdateConversation_WithEmptyRequest() =>
            this.UpdateConversationAsync(
                this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmpty)),
                SerializationTest.BuildEmptyRequest());

        [Fact]
        public Task UpdateConversation_WithRequest() =>
            this.UpdateConversationAsync(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)),
                SerializationTest.BuildRequest());

        private async Task UpdateConversationAsync(string jsonRequest, Result<UpdateConversationRequest> request)
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations/CON-1234")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(jsonRequest)
                    .UsingPut())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ConversationsClient
                .UpdateConversationAsync(request)
                .Should()
                .BeSuccessAsync(ConversationTests.VerifyExpectedResponse);
        }
    }
}