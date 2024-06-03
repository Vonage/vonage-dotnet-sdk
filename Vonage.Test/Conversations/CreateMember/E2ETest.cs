using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateMember;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Conversations.CreateMember;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }
    
    [Fact]
    public Task CreateMember_WithApp() =>
        this.CreateConversationAsync(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeApp)),
            SerializationTest.BuildAppRequest());
    
    private async Task CreateConversationAsync(string jsonRequest, Result<CreateMemberRequest> request)
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/members")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(jsonRequest)
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .CreateMemberAsync(request)
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyResponse);
    }
}