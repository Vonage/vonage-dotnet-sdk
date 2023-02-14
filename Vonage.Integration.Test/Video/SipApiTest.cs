using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.InitiateCall;

namespace Vonage.Integration.Test.Video;

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
}
