using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Sessions.CreateSession;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Sessions.CreateSession
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateDefaultSession()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldDeserialize200));
            await this.Helper.VonageClient.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Default)
                .Should()
                .BeSuccessAsync(session => SerializationTest.VerifySessions(new[] {session}));
        }

        [Fact]
        public async Task CreateSession_ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldDeserialize200_GivenEmptyArray));
            await this.Helper.VonageClient.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Default)
                .Should()
                .BeFailureAsync(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        private void SetUpServer(string requestNamespace) =>
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/session/create")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody("location=&archiveMode=manual&p2p.preference=enabled")
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(requestNamespace)));
    }
}