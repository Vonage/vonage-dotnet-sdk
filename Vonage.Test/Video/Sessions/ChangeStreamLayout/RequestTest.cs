#region
using System;
using System.Collections.Generic;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.ChangeStreamLayout;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly ChangeStreamLayoutRequest.LayoutItem item1;
    private readonly ChangeStreamLayoutRequest.LayoutItem item2;
    private readonly IEnumerable<ChangeStreamLayoutRequest.LayoutItem> items;
    private readonly string sessionId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.item1 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
        this.item2 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
    }

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        ChangeStreamLayoutRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithItems(new[] {this.item1})
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream");
}