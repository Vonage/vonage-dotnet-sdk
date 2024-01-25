using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.GetBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.GetBroadcast;

public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly Guid broadcastId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.broadcastId = fixture.Create<Guid>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        GetBroadcastRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithBroadcastId(this.broadcastId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
        GetBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithBroadcastId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("BroadcastId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
        GetBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithBroadcastId(this.broadcastId)
            .Create()
            .Should()
            .BeSuccess(success =>
            {
                success.ApplicationId.Should().Be(this.applicationId);
                success.BroadcastId.Should().Be(this.broadcastId);
            });
}