using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sip.PlayToneIntoConnection;
using Xunit;

namespace Vonage.Test.Video.Sip.PlayToneIntoConnection;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly string connectionId;
    private readonly string digits;
    private readonly string sessionId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.connectionId = fixture.Create<string>();
        this.digits = fixture.Create<string>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .WithConnectionId(this.connectionId)
            .WithDigits(this.digits)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConnectionId(value)
            .WithDigits(this.digits)
            .Create()
            .Should()
            .BeParsingFailure("ConnectionId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenDigitsIsNullOrWhitespace(string value) =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConnectionId(this.connectionId)
            .WithDigits(value)
            .Create()
            .Should()
            .BeParsingFailure("Digits cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .WithConnectionId(this.connectionId)
            .WithDigits(this.digits)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
        PlayToneIntoConnectionRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithConnectionId(this.connectionId)
            .WithDigits(this.digits)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.ConnectionId.Should().Be(this.connectionId);
                request.Digits.Should().Be(this.digits);
            });
}