#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Conversations.UpdateMember;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.UpdateMember;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public Task UpdateMemberWithFrom() =>
        this.CreateConversationAsync(
            this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeWithFrom)),
            SerializationTest.BuildRequestWithFrom());

    [Fact]
    public Task UpdateMemberWithJoinedState() =>
        this.CreateConversationAsync(
            this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeWithJoinedState)),
            SerializationTest.BuildRequestWithJoinedState());

    [Fact]
    public Task UpdateMemberWithLeftState() =>
        this.CreateConversationAsync(
            this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeWithLeftState)),
            SerializationTest.BuildRequestWithLeftState());

    private async Task CreateConversationAsync(string jsonRequest, Result<UpdateMemberRequest> request)
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/members/MEM-123")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(jsonRequest)
                .UsingPatch())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ConversationsClient
            .UpdateMemberAsync(request)
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyResponse);
    }
}