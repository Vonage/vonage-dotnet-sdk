#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.StopBroadcast;
using Xunit;
#endregion

namespace Vonage.Test.Video.Broadcast.StopBroadcast;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid broadcastId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.broadcastId = fixture.Create<Guid>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
        StopBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithBroadcastId(this.broadcastId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/stop");
}