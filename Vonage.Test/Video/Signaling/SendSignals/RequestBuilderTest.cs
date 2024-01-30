using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Test.Video.Signaling.SendSignals;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly SignalContent content;
    private readonly Fixture fixture;
    private readonly string sessionId;

    public RequestBuilderTest()
    {
        this.fixture = new Fixture();
        this.applicationId = this.fixture.Create<Guid>();
        this.sessionId = this.fixture.Create<string>();
        this.content = this.fixture.Create<SignalContent>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        SendSignalsRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .WithContent(this.content)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
        SendSignalsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithContent(new SignalContent(this.fixture.Create<string>(), value))
            .Create()
            .Should()
            .BeParsingFailure("Data cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
        SendSignalsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithContent(new SignalContent(value, this.fixture.Create<string>()))
            .Create()
            .Should()
            .BeParsingFailure("Type cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        SendSignalsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .WithContent(this.content)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
        SendSignalsRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithContent(this.content)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.Content.Should().BeEquivalentTo(this.content);
            });
}