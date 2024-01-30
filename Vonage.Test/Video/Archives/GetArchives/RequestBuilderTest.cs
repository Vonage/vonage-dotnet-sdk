using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Test.Video.Archives.GetArchives;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
    }

    [Fact]
    public void Build_ShouldAssignCount_GivenWithCountIsUsed() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithCount(100)
            .Create()
            .Should()
            .BeSuccess(request => request.Count.Should().Be(100));

    [Fact]
    public void Build_ShouldAssignOffset_GivenWithOffsetIsUsed() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithOffset(500)
            .Create()
            .Should()
            .BeSuccess(request => request.Offset.Should().Be(500));

    [Fact]
    public void Build_ShouldAssignSessionId_GivenWithSessionIdIsUsed() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId("some value")
            .Create()
            .Should()
            .BeSuccess(request => request.SessionId.Should().BeSome("some value"));

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        GetArchivesRequest.Build()
            .WithApplicationId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenCountIsHigherThanThreshold() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithCount(1001)
            .Create()
            .Should()
            .BeParsingFailure("Count cannot be higher than 1000.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenCountIsNegative() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithCount(-1)
            .Create()
            .Should()
            .BeParsingFailure("Count cannot be negative.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenOffsetIsNegative() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithOffset(-1)
            .Create()
            .Should()
            .BeParsingFailure("Offset cannot be negative.");

    [Fact]
    public void Build_ShouldReturnSuccess_WithDefaultValues() =>
        GetArchivesRequest.Build()
            .WithApplicationId(this.applicationId)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.Offset.Should().Be(0);
                request.Count.Should().Be(50);
                request.SessionId.Should().BeNone();
            });
}