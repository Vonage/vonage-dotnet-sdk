using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.StopArchive;
using Xunit;

namespace Vonage.Test.Video.Archives.StopArchive;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly Guid archiveId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.archiveId = fixture.Create<Guid>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StopArchiveRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithArchiveId(this.archiveId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
        StopArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithArchiveId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ArchiveId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
        StopArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithArchiveId(this.archiveId)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.ArchiveId.Should().Be(this.archiveId);
            });
}