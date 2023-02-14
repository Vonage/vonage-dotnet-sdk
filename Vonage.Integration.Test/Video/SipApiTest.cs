using Vonage.Common.Monads;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions.CreateSession;
using Vonage.Server.Video.Sip.InitiateCall;
using Vonage.Server.Video.Sip.PlayToneIntoCall;

namespace Vonage.Integration.Test.Video;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// TODO: Use WebApplicationFactory to connect to a session?
/// </remarks>
public class SipApiTest : BaseIntegrationTest
{
    [Fact]
    public async Task InitiateCall_ShouldReturnSuccess()
    {
        // var request = SipElementBuilder
        //     .Build("sip:user@sip.partner.com;transport=tls")
        //     .Create()
        //     .Bind(sipElement => InitiateCallRequest.Parse(new Guid(this.VideoClient.Credentials.ApplicationId), "aze", "aze", sipElement));
        // var response = await this.VideoClient.SipClient.InitiateCall(request);
        // response.Should().BeSuccess(success =>
        // {
        //
        // });
    }

    [Fact]
    public async Task PlayToneIntoCallAsync_ShouldReturnSuccess()
    {
        // var request = await this.VideoClient.SessionClient
        //     .CreateSessionAsync(CreateSessionRequest.Default)
        //     .Map(session => session.SessionId)
        //     .Bind(sessionId =>
        //         PlayToneIntoCallRequest.Parse(new Guid(this.VideoClient.Credentials.ApplicationId), sessionId, "1713"));
        // var response = await this.VideoClient.SipClient.PlayToneIntoCallAsync(request);
        // response.Should().BeSuccess(_ => { });
    }
}
