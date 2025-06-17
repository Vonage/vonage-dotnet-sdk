#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.GetArchives;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.GetArchives;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
    }

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_WithDefaultOffsetAndCount() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=0&count=50");

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_WithOffsetAndCount() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithCount(100)
            .WithOffset(1000)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=1000&count=100");

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint_WithSessionId() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId("123456")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive?offset=0&count=50&sessionId=123456");
}