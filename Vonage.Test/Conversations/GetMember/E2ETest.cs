#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Conversations;
using Vonage.Conversations.GetMember;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Conversations.GetMember;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetMember_App() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeApp), SerializationTest.VerifyAppResponse);

    [Fact]
    public async Task GetMember_InitiatorAdmin() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeInitiatorAdmin),
            SerializationTest.VerifyInitiatorAdminResponse);

    [Fact]
    public async Task GetMember_Messenger() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeMessenger),
            SerializationTest.VerifyMessengerResponse);

    [Fact]
    public async Task GetMember_MMS() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeMMS), SerializationTest.VerifyMMSResponse);

    [Fact]
    public async Task GetMember_Phone() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializePhone),
            SerializationTest.VerifyPhoneResponse);

    [Fact]
    public async Task GetMember_SMS() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeSMS), SerializationTest.VerifySMSResponse);

    [Fact]
    public async Task GetMember_Viber() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeViber),
            SerializationTest.VerifyViberResponse);

    [Fact]
    public async Task GetMember_WhatsApp() =>
        await this.VerifyGetMember(nameof(SerializationTest.ShouldDeserializeWhatsApp),
            SerializationTest.VerifyWhatsAppResponse);

    private async Task VerifyGetMember(string response, Action<Member> verification)
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/conversations/CON-123/members/MEM-123")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(response)));
        await this.Helper.VonageClient.ConversationsClient
            .GetMemberAsync(GetMemberRequest.Parse("CON-123", "MEM-123"))
            .Should()
            .BeSuccessAsync(verification);
    }
}