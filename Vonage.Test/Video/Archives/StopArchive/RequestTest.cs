﻿#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.StopArchive;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.StopArchive;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly Guid archiveId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.archiveId = fixture.Create<Guid>();
    }

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        StopArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithArchiveId(this.archiveId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/stop");
}