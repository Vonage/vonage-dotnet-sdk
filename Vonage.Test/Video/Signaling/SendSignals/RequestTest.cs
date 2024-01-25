using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Test.Video.Signaling.SendSignals;

public class RequestTest
{
    private readonly Guid applicationId;
    private readonly SignalContent content;
    private readonly Fixture fixture;
    private readonly string sessionId;

    public RequestTest()
    {
        this.fixture = new Fixture();
        this.applicationId = this.fixture.Create<Guid>();
        this.sessionId = this.fixture.Create<string>();
        this.content = this.fixture.Create<SignalContent>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        SendSignalsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithContent(this.content)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/signal");
}