using System;
using AutoFixture;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.StartBroadcast;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Fixture fixture;

    public RequestTest()
    {
        this.fixture = new Fixture();
        this.fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = this.fixture.Create<Guid>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.fixture.Create<string>())
            .WithLayout(new Layout(null, null, LayoutType.HorizontalPresentation))
            .WithOutputs(this.fixture.Create<StartBroadcastRequest.BroadcastOutput>())
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/broadcast");
}