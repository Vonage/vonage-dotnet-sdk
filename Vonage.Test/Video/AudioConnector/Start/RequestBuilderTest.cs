#region
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.AudioConnector.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.AudioConnector.Start;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidSessionId = "session-id";
    private const string ValidToken = "token";
    private readonly Guid validApplicationId = Guid.NewGuid();
    private readonly Uri validUri = new Uri("https://example.com");

    [Fact]
    public void Build_ShouldHaveDefaultAudioRate() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.WebSocket.AudioRate)
            .Should()
            .BeSuccess(SupportedAudioRates.AUDIO_RATE_8000Hz);

    [Fact]
    public void Build_ShouldHaveEmptyHeaders_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.WebSocket.Headers)
            .Should()
            .BeSuccess(headers => headers.Should().BeEmpty());

    [Fact]
    public void Build_ShouldHaveEmptyStreams_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.WebSocket.Streams)
            .Should()
            .BeSuccess(Enumerable.Empty<string>().ToArray());

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Build_ShouldSetAudioRate() =>
        this.BuildValidRequest()
            .WithAudioRate(SupportedAudioRates.AUDIO_RATE_16000Hz)
            .Create()
            .Map(request => request.WebSocket.AudioRate)
            .Should()
            .BeSuccess(SupportedAudioRates.AUDIO_RATE_16000Hz);

    [Fact]
    public void Build_ShouldSetHeaders() =>
        this.BuildValidRequest()
            .WithHeader(new KeyValuePair<string, string>("key1", "value1"))
            .WithHeader(new KeyValuePair<string, string>("key2", "value2"))
            .Create()
            .Map(request => request.WebSocket.Headers)
            .Should()
            .BeSuccess(headers => headers.Should().BeEquivalentTo(new[]
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2"),
            }));

    [Fact]
    public void Build_ShouldSetHeadersWithoutDuplicates() =>
        this.BuildValidRequest()
            .WithHeader(new KeyValuePair<string, string>("key1", "value1"))
            .WithHeader(new KeyValuePair<string, string>("key2", "value2"))
            .WithHeader(new KeyValuePair<string, string>("key1", "value3"))
            .WithHeader(new KeyValuePair<string, string>("key2", "value4"))
            .Create()
            .Map(request => request.WebSocket.Headers)
            .Should()
            .BeSuccess(headers => headers.Should().BeEquivalentTo(new[]
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2"),
            }));

    [Fact]
    public void Build_ShouldSetSessionId() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.SessionId)
            .Should()
            .BeSuccess(ValidSessionId);

    [Fact]
    public void Build_ShouldSetStreams() =>
        this.BuildValidRequest()
            .WithStream("stream-1")
            .WithStream("stream-2")
            .Create()
            .Map(request => request.WebSocket.Streams)
            .Should()
            .BeSuccess(new[] {"stream-1", "stream-2"});

    [Fact]
    public void Build_ShouldSetStreamsWithoutDuplicates() =>
        this.BuildValidRequest()
            .WithStream("stream-1")
            .WithStream("stream-2")
            .WithStream("stream-1")
            .WithStream("stream-2")
            .Create()
            .Map(request => request.WebSocket.Streams)
            .Should()
            .BeSuccess(new[] {"stream-1", "stream-2"});

    [Fact]
    public void Build_ShouldSetToken() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.Token)
            .Should()
            .BeSuccess(ValidToken);

    [Fact]
    public void Build_ShouldSetUri() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.WebSocket.Uri)
            .Should()
            .BeSuccess(this.validUri);

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StartRequest
            .Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenSessionIdIsEmpty(string invalidId) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(invalidId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenTokenIsEmpty(string invalidToken) =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(invalidToken)
            .WithUrl(this.validUri)
            .Create()
            .Should()
            .BeParsingFailure("Token cannot be null or whitespace.");

    private IBuilderForOptional BuildValidRequest() =>
        StartRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithSessionId(ValidSessionId)
            .WithToken(ValidToken)
            .WithUrl(this.validUri);
}