#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using Xunit;
#endregion

namespace Vonage.Test.Video.Broadcast.AddStreamToBroadcast;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid broadcastId;
    private readonly Guid streamId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.broadcastId = fixture.Create<Guid>();
        this.streamId = fixture.Create<Guid>();
    }

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
        AddStreamToBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithBroadcastId(this.broadcastId)
            .WithStreamId(this.streamId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/streams");
}