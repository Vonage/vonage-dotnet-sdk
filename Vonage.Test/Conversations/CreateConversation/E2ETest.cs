using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateConversation;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Conversations.CreateConversation
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public Task CreateConversation_WithEmptyRequest() =>
            this.CreateConversationAsync(
                this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmpty)),
                SerializationTest.BuildEmptyRequest());

        [Fact]
        public Task CreateConversation_WithRequest() =>
            this.CreateConversationAsync(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)),
                SerializationTest.BuildRequest());

        private async Task CreateConversationAsync(string jsonRequest, Result<CreateConversationRequest> request)
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(jsonRequest)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ConversationsClient
                .CreateConversationAsync(request)
                .Should()
                .BeSuccessAsync(ConversationTests.VerifyExpectedResponse);
        }
    }
}