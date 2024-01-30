using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Test.Video.Moderation.MuteStream;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly string sessionId;
    private readonly string streamId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.streamId = fixture.Create<string>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        MuteStreamRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .WithStreamId(this.streamId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        MuteStreamRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .WithStreamId(this.streamId)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
        MuteStreamRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithStreamId(value)
            .Create()
            .Should()
            .BeParsingFailure("StreamId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
        MuteStreamRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithStreamId(this.streamId)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.StreamId.Should().Be(this.streamId);
            });
}