using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations.DeleteConversation;
using Vonage.Test.Unit.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Conversations.DeleteConversation
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DeleteConversation()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/conversations/CON-123")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.ConversationsClient
                .DeleteConversationAsync(DeleteConversationRequest.Parse("CON-123"))
                .Should()
                .BeSuccessAsync();
        }
    }
}