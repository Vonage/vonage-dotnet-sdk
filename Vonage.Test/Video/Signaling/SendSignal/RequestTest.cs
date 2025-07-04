﻿#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignal;
using Xunit;
#endregion

namespace Vonage.Test.Video.Signaling.SendSignal;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly string connectionId;
    private readonly SignalContent content;
    private readonly Fixture fixture;
    private readonly string sessionId;

    public RequestTest()
    {
        this.fixture = new Fixture();
        this.applicationId = this.fixture.Create<Guid>();
        this.sessionId = this.fixture.Create<string>();
        this.connectionId = this.fixture.Create<string>();
        this.content = this.fixture.Create<SignalContent>();
    }

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        SendSignalRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConnectionId(this.connectionId)
            .WithContent(this.content)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                $"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}/signal");
}