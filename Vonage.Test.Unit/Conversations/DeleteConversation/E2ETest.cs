using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Conversations.DeleteConversation;
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
                    .WithPath("/v1/conversations")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.ConversationsClient
                .DeleteConversationAsync(DeleteConversationRequest.Parse(""))
                .Should()
                .BeSuccessAsync();
        }
    }
}