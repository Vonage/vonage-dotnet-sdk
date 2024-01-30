using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Test.Video.Moderation.MuteStreams;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly MuteStreamsRequest.MuteStreamsConfiguration configuration;
    private readonly string sessionId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.configuration = fixture.Create<MuteStreamsRequest.MuteStreamsConfiguration>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        MuteStreamsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConfiguration(this.configuration)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess(
                $"/v2/project/{this.applicationId}/session/{this.sessionId}/mute");
}