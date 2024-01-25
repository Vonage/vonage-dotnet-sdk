using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Test.Video.Sip.PlayToneIntoCall;

public class RequestTest
{
    private readonly Guid applicationId;
    private readonly string digits;
    private readonly string sessionId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.digits = fixture.Create<string>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        PlayToneIntoCallRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithDigits(this.digits)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/play-dtmf");
}