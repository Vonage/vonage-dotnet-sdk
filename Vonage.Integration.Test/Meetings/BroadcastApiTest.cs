using Vonage.Server.Authentication;
using Vonage.Server.Video.Sessions.CreateSession;
using Xunit.Abstractions;

namespace Vonage.Integration.Test.Meetings;

public class BroadcastApiTest : BaseIntegrationTest
{
    public BroadcastApiTest(ITestOutputHelper outputHelper)
    {
    }

    [Fact]
    public async Task CreateTheme_ShouldReturnSuccess()
    {
        var sessionCreated = await this.VideoClient.SessionClient.CreateSessionAsync(CreateSessionRequest.Default);
        var sessionId = sessionCreated.GetSuccessUnsafe().SessionId;
        var token = TokenAdditionalClaims.Parse(sessionId).Bind(claims =>
            new VideoTokenGenerator().GenerateToken(this.VideoClient.Credentials, claims));
        var applicationId = this.VideoClient.Credentials.ApplicationId;
    }
}