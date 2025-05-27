#region
using System;
using AutoFixture;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.ChangeBroadcastLayout;
using Xunit;
#endregion

namespace Vonage.Test.Video.Broadcast.ChangeBroadcastLayout;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid broadcastId;
    private readonly Layout layout;

    public RequestTest()
    {
        var fixture = new Fixture();
        fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = fixture.Create<Guid>();
        this.broadcastId = fixture.Create<Guid>();
        this.layout = fixture.Create<Layout>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
        ChangeBroadcastLayoutRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithBroadcastId(this.broadcastId)
            .WithLayout(this.layout)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/broadcast/{this.broadcastId}/layout");
}