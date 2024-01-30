using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.StartBroadcast;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly Layout layout;
    private readonly StartBroadcastRequest.BroadcastOutput outputs;
    private readonly string sessionId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.layout = new Layout(null, null, LayoutType.HorizontalPresentation);
        this.outputs = new StartBroadcastRequest.BroadcastOutput
        {
            Hls = new Vonage.Video.Broadcast.Broadcast.HlsSettings(false, false),
            Streams = fixture.CreateMany<StartBroadcastRequest.BroadcastOutput.Stream>().ToArray(),
        };
    }

    [Fact]
    public void Build_ShouldAssignMaxBitrate_GivenWithMaxBitrateIsUsed() =>
        this.BuildWithMandatoryValues()
            .WithMaxBitrate(500)
            .Create()
            .Map(request => request.MaxBitrate)
            .Should()
            .BeSuccess(500);

    [Fact]
    public void Build_ShouldAssignMaxDuration_GivenMaxDurationIsMaximumValue() =>
        this.BuildWithMandatoryValues()
            .WithMaxDuration(36000)
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(36000);

    [Fact]
    public void Build_ShouldAssignMaxDuration_GivenMaxDurationIsMinimumValue() =>
        this.BuildWithMandatoryValues()
            .WithMaxDuration(60)
            .Create()
            .Map(request => request.MaxDuration)
            .Should()
            .BeSuccess(60);

    [Fact]
    public void Build_ShouldAssignMultiBroadcastTag_GivenWithMultiBroadcastTagIsUsed() =>
        this.BuildWithMandatoryValues()
            .WithMultiBroadcastTag("Test")
            .Create()
            .Map(request => request.MultiBroadcastTag)
            .Should()
            .BeSuccess(success => success.Should().BeSome("Test"));

    [Fact]
    public void Build_ShouldAssignResolution_GivenWithResolutionIsUsed() =>
        this.BuildWithMandatoryValues()
            .WithResolution(RenderResolution.FullHighDefinitionLandscape)
            .Create()
            .Map(request => request.Resolution)
            .Should()
            .BeSuccess(RenderResolution.FullHighDefinitionLandscape);

    [Fact]
    public void Build_ShouldAssignStreamMode_GivenWithStreamModeUsed() =>
        this.BuildWithMandatoryValues()
            .WithManualStreamMode()
            .Create()
            .Map(request => request.StreamMode)
            .Should()
            .BeSuccess(StreamMode.Manual);

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StartBroadcastRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .WithLayout(this.layout)
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenDvrAndLowLatencyAreBothTrue() =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(this.layout)
            .WithOutputs(new StartBroadcastRequest.BroadcastOutput
            {
                Hls = new Vonage.Video.Broadcast.Broadcast.HlsSettings(true, true),
            })
            .Create()
            .Should()
            .BeParsingFailure("Dvr and LowLatency cannot be both set to true.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenLayoutScreenshareTypeIsSetAndStylesheetIsFilled() =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(new Layout(LayoutType.Custom, "stylesheet", LayoutType.BestFit))
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure("Stylesheet should be null when screenshare type is set.");

    [Theory]
    [InlineData(LayoutType.Pip)]
    [InlineData(LayoutType.HorizontalPresentation)]
    [InlineData(LayoutType.VerticalPresentation)]
    public void Build_ShouldReturnFailure_GivenLayoutScreenshareTypeIsSetAndTypeIsNotBestFit(LayoutType value) =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(new Layout(LayoutType.Custom, null, value))
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure("Type should be BestFit when screenshare type is set.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenLayoutStylesheetIsEmptyWithCustomType(string value) =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(new Layout(null, value, LayoutType.Custom))
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure(
                "Stylesheet cannot be null or whitespace when type is Custom.");

    [Theory]
    [InlineData(LayoutType.BestFit)]
    [InlineData(LayoutType.Pip)]
    [InlineData(LayoutType.HorizontalPresentation)]
    [InlineData(LayoutType.VerticalPresentation)]
    public void Build_ShouldReturnFailure_GivenLayoutStylesheetIsFilledWithNonCustomType(LayoutType value) =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(new Layout(null, "stylesheet example", value))
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure(
                "Stylesheet should be null or whitespace when type is not Custom.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenMaxDurationIsHigherThanMaximumValue() =>
        this.BuildWithMandatoryValues()
            .WithMaxDuration(59)
            .Create()
            .Should()
            .BeParsingFailure("MaxDuration cannot be lower than 60.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenMaxDurationIsLowerThanMinimumValue() =>
        this.BuildWithMandatoryValues()
            .WithMaxDuration(59)
            .Create()
            .Should()
            .BeParsingFailure("MaxDuration cannot be lower than 60.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .WithLayout(this.layout)
            .WithOutputs(this.outputs)
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnSuccess_WithDefaultValues() =>
        this.BuildWithMandatoryValues()
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.Layout.Should().Be(this.layout);
                request.Outputs.Should().Be(this.outputs);
                request.ApplicationId.Should().Be(this.applicationId);
                request.MaxDuration.Should().Be(14400);
                request.Resolution.Should().Be(RenderResolution.StandardDefinitionLandscape);
                request.StreamMode.Should().Be(StreamMode.Auto);
                request.MultiBroadcastTag.Should().BeNone();
                request.MaxBitrate.Should().Be(1000);
            });

    private IBuilderForOptional BuildWithMandatoryValues() =>
        StartBroadcastRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithLayout(this.layout)
            .WithOutputs(this.outputs);
}