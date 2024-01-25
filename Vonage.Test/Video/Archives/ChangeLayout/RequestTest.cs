using System;
using AutoFixture;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.ChangeLayout;
using Xunit;

namespace Vonage.Test.Video.Archives.ChangeLayout;

public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid archiveId;
    private readonly Layout layout;

    public RequestTest()
    {
        var fixture = new Fixture();
        fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = fixture.Create<Guid>();
        this.archiveId = fixture.Create<Guid>();
        this.layout = fixture.Create<Layout>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        ChangeLayoutRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithArchiveId(this.archiveId)
            .WithLayout(this.layout)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/layout");
}