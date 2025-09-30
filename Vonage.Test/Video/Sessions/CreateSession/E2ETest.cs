#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions;
using Vonage.Video.Sessions.CreateSession;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.CreateSession;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task CreateDefaultSession()
    {
        this.SetUpServer(nameof(SerializationTest.ShouldDeserialize200),
            "location=&archiveMode=manual&p2p.preference=enabled&e2ee=false");
        await this.Helper.VonageClient.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Default)
            .Should()
            .BeSuccessAsync(session => SerializationTest.VerifySessions(new[] {session}));
    }

    [Fact]
    public async Task CreateSession_ShouldReturnFailure_GivenResponseContainsNoSession()
    {
        this.SetUpServer(nameof(SerializationTest.ShouldDeserialize200_GivenEmptyArray),
            "location=&archiveMode=manual&p2p.preference=enabled&e2ee=false");
        await this.Helper.VonageClient.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Default)
            .Should()
            .BeFailureAsync(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
    }

    [Fact]
    public async Task CreatSession()
    {
        this.SetUpServer(nameof(SerializationTest.ShouldDeserialize200),
            "location=192.168.1.1&archiveMode=always&p2p.preference=disabled&e2ee=true");
        await this.Helper.VonageClient.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Build()
                .WithLocation("192.168.1.1")
                .WithMediaMode(MediaMode.Routed)
                .WithArchiveMode(ArchiveMode.Always)
                .EnableEncryption()
                .Create())
            .Should()
            .BeSuccessAsync(session => SerializationTest.VerifySessions(new[] {session}));
    }

    private void SetUpServer(string requestNamespace, string expectedBody) =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/session/create")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(expectedBody)
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(requestNamespace)));
}