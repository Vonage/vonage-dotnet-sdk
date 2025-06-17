#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.AddStream;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.AddStream;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid archiveId;
    private readonly Guid streamId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.archiveId = fixture.Create<Guid>();
        this.streamId = fixture.Create<Guid>();
    }

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        AddStreamRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithArchiveId(this.archiveId)
            .WithStreamId(this.streamId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/streams");
}