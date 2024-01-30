using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Moderation.DisconnectConnection;
using Xunit;

namespace Vonage.Test.Video.Moderation.DisconnectConnection;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly string connectionId;
    private readonly string sessionId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.connectionId = fixture.Create<string>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        DisconnectConnectionRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConnectionId(this.connectionId)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}");
}