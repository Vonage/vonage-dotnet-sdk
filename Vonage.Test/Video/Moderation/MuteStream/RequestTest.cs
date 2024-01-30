using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Test.Video.Moderation.MuteStream;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly string sessionId;
    private readonly string streamId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.streamId = fixture.Create<string>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        MuteStreamRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithStreamId(this.streamId)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream/{this.streamId}/mute");
}