#region
using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.CreateArchive;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.CreateArchive;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly Layout layout;
    private readonly string name;
    private readonly OutputMode outputMode;
    private readonly RenderResolution resolution;
    private readonly string sessionId;
    private readonly StreamMode streamMode;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.name = fixture.Create<string>();
        this.streamMode = fixture.Create<StreamMode>();
        this.resolution = fixture.Create<RenderResolution>();
        this.outputMode = fixture.Create<OutputMode>();
        this.layout = fixture.Create<Layout>();
    }

    [Fact]
    public void Build_ShouldAssignArchiveLayout_GivenWithArchiveLayoutIsUsed() =>
        this.BuildValidRequest()
            .WithLayout(this.layout)
            .Create()
            .Should()
            .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

    [Fact]
    public void Build_ShouldAssignDefault_HasTranscription() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.HasTranscription)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldAssignHasTranscription() =>
        this.BuildValidRequest()
            .EnableTranscription()
            .Create()
            .Map(request => request.HasTranscription)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldAssignName_GivenWithNameIsUsed() =>
        this.BuildValidRequest()
            .WithName(this.name)
            .Create()
            .Should()
            .BeSuccess(request => request.Name.Should().BeSome(this.name));

    [Fact]
    public void Build_ShouldAssignOutputMode_GivenWithOutputModeIsUsed() =>
        this.BuildValidRequest()
            .Create()
            .Should()
            .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

    [Fact]
    public void Build_ShouldAssignResolution_GivenWithRenderResolutionIsUsed() =>
        this.BuildValidRequest()
            .WithResolution(this.resolution)
            .Create()
            .Should()
            .BeSuccess(request => request.Resolution.Should().Be(this.resolution));

    [Fact]
    public void Build_ShouldAssignStreamMode_GivenWithStreamModeIsUsed() =>
        this.BuildValidRequest()
            .WithStreamMode(this.streamMode)
            .Create()
            .Should()
            .BeSuccess(request => request.StreamMode.Should().Be(this.streamMode));

    [Fact]
    public void Build_ShouldHaveEmptyMaxBitrate_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.MaxBitrate)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldHaveEmptyQuantizationParameter_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.QuantizationParameter)
            .Should()
            .BeSuccess(Maybe<int>.None);

    [Fact]
    public void Build_ShouldHaveNoMultiArchiveTag_GivenDefault() =>
        this.BuildValidRequest()
            .Create()
            .Map(request => request.MultiArchiveTag)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldReturnDisabledAudio_WhenUsingDisableAudio() =>
        this.BuildValidRequest()
            .DisableAudio()
            .Create()
            .Should()
            .BeSuccess(request => request.HasAudio.Should().Be(false));

    [Fact]
    public void Build_ShouldReturnDisabledVideo_WhenUsingDisableVideo() =>
        this.BuildValidRequest()
            .DisableVideo()
            .Create()
            .Should()
            .BeSuccess(request => request.HasVideo.Should().Be(false));

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        CreateArchiveRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenMaxBitrateIsHigherThanMaximum() =>
        this.BuildValidRequest()
            .WithMaxBitrate(6000001)
            .Create()
            .Should()
            .BeParsingFailure("MaxBitrate cannot be higher than 6000000.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenMaxBitrateIsLowerThanMinimum() =>
        this.BuildValidRequest()
            .WithMaxBitrate(999999)
            .Create()
            .Should()
            .BeParsingFailure("MaxBitrate cannot be lower than 1000000.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenQuantizationParameterIsHigherThanMaximum() =>
        this.BuildValidRequest()
            .WithQuantizationParameter(41)
            .Create()
            .Should()
            .BeParsingFailure("QuantizationParameter cannot be higher than 40.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenQuantizationParameterIsLowerThanMinimum() =>
        this.BuildValidRequest()
            .WithQuantizationParameter(14)
            .Create()
            .Should()
            .BeParsingFailure("QuantizationParameter cannot be lower than 15.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        CreateArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess_WithDefaultValues() =>
        CreateArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.HasAudio.Should().BeTrue();
                request.HasVideo.Should().BeTrue();
                request.Name.Should().BeNone();
                request.OutputMode.Should().Be(OutputMode.Composed);
                request.Resolution.Should().BeNone();
                request.StreamMode.Should().Be(StreamMode.Auto);
                request.Layout.Should().Be(null);
            });

    [Theory]
    [InlineData(1000000)]
    [InlineData(6000000)]
    public void Build_ShouldSetMaxBitrate(int value) =>
        this.BuildValidRequest()
            .WithMaxBitrate(value)
            .Create()
            .Map(request => request.MaxBitrate)
            .Should()
            .BeSuccess(value);

    [Fact]
    public void Build_ShouldSetMultiArchiveTag() =>
        this.BuildValidRequest()
            .WithMultiArchiveTag("custom-tag")
            .Create()
            .Map(request => request.MultiArchiveTag)
            .Should()
            .BeSuccess("custom-tag");

    [Theory]
    [InlineData(15)]
    [InlineData(40)]
    public void Build_ShouldSetQuantizationParameter(int value) =>
        this.BuildValidRequest()
            .WithQuantizationParameter(value)
            .Create()
            .Map(request => request.QuantizationParameter)
            .Should()
            .BeSuccess(value);

    private IBuilderForOptional BuildValidRequest() =>
        CreateArchiveRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId);
}