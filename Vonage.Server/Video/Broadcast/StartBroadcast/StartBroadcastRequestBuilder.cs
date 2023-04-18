using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.StartBroadcast;

/// <summary>
///     Represents a builder for StartBroadcastRequest.
/// </summary>
public class StartBroadcastRequestBuilder : IBuilderForSessionId, IBuilderForOutputs, IBuilderForLayout,
    IBuilderForOptional
{
    private const int MaximumMaxDuration = 36000;
    private const int MinimumMaxDuration = 60;
    private StartBroadcastRequest.BroadcastOutput outputs;
    private readonly Guid applicationId;
    private int maxBitrate = 1000;
    private int maxDuration = 14400;
    private Layout layout;
    private Maybe<string> multiBroadcastTag;
    private RenderResolution resolution = RenderResolution.StandardDefinitionLandscape;
    private StreamMode streamMode = StreamMode.Auto;
    private string sessionId;

    private StartBroadcastRequestBuilder(Guid applicationId) => this.applicationId = applicationId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="applicationId">The Vonage application UUID.</param>
    /// <returns>The builder.</returns>
    public static IBuilderForSessionId Build(Guid applicationId) =>
        new StartBroadcastRequestBuilder(applicationId);

    /// <inheritdoc />
    public Result<StartBroadcastRequest> Create() =>
        Result<StartBroadcastRequest>.FromSuccess(new StartBroadcastRequest
            {
                StreamMode = this.streamMode,
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
                Layout = this.layout,
                Outputs = this.outputs,
                Resolution = this.resolution,
                MaxBitrate = this.maxBitrate,
                MaxDuration = this.maxDuration,
                MultiBroadcastTag = this.multiBroadcastTag,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifySessionId)
            .Bind(VerifyMaxDuration)
            .Bind(VerifyHls)
            .Bind(VerifyLayout);

    /// <inheritdoc />
    public IBuilderForOutputs WithLayout(Layout value)
    {
        this.layout = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithManualStreamMode()
    {
        this.streamMode = StreamMode.Manual;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithMaxBitrate(int value)
    {
        this.maxBitrate = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithMaxDuration(int value)
    {
        this.maxDuration = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithMultiBroadcastTag(string value)
    {
        this.multiBroadcastTag = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithOutputs(StartBroadcastRequest.BroadcastOutput value)
    {
        this.outputs = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithResolution(RenderResolution value)
    {
        this.resolution = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForLayout WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<StartBroadcastRequest> VerifyHls(StartBroadcastRequest request) =>
        request.Outputs.Hls
            .Map(value => value.LowLatency && value.Dvr)
            .IfNone(false)
            ? Result<StartBroadcastRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Dvr and LowLatency cannot be both set to true."))
            : request;

    private static Result<StartBroadcastRequest> VerifyLayout(StartBroadcastRequest request) =>
        new
            {
                IsCustomType = request.Layout.Type == LayoutType.Custom,
                IsBestFitType = request.Layout.Type == LayoutType.BestFit,
                IsStylesheetEmpty = string.IsNullOrWhiteSpace(request.Layout.Stylesheet),
                IsScreenshareTypeSet = request.Layout.ScreenshareType != null,
            }
            switch
            {
                {IsScreenshareTypeSet: true, IsStylesheetEmpty: false} =>
                    ResultFailure.FromErrorMessage("Stylesheet should be null when screenshare type is set.")
                        .ToResult<StartBroadcastRequest>(),
                {IsScreenshareTypeSet: true, IsBestFitType: false} =>
                    ResultFailure.FromErrorMessage("Type should be BestFit when screenshare type is set.")
                        .ToResult<StartBroadcastRequest>(),
                {IsCustomType: true, IsStylesheetEmpty: true} =>
                    ResultFailure.FromErrorMessage("Stylesheet cannot be null or whitespace when type is Custom.")
                        .ToResult<StartBroadcastRequest>(),
                {IsCustomType: false, IsStylesheetEmpty: false} =>
                    ResultFailure.FromErrorMessage("Stylesheet should be null or whitespace when type is not Custom.")
                        .ToResult<StartBroadcastRequest>(),
                _ => request,
            };

    private static Result<StartBroadcastRequest> VerifyMaxDuration(StartBroadcastRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.MaxDuration, MinimumMaxDuration, nameof(request.MaxDuration))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, MaximumMaxDuration,
                nameof(request.MaxDuration)));
}

/// <summary>
///     Represents a StartBroadcastRequestBuilder that allows to set the SessionId.
/// </summary>
public interface IBuilderForSessionId
{
    /// <summary>
    ///     Sets the SessionId on the builder.
    /// </summary>
    /// <param name="value">The session id.</param>
    /// <returns>The builder.</returns>
    IBuilderForLayout WithSessionId(string value);
}

/// <summary>
///     Represents a StartBroadcastRequestBuilder that allows to set the Layout.
/// </summary>
public interface IBuilderForLayout
{
    /// <summary>
    ///     Sets the Layout on the builder.
    /// </summary>
    /// <param name="value">The layout.</param>
    /// <returns>The builder.</returns>
    IBuilderForOutputs WithLayout(Layout value);
}

/// <summary>
///     Represents a StartBroadcastRequestBuilder that allows to set Outputs.
/// </summary>
public interface IBuilderForOutputs
{
    /// <summary>
    ///     Sets the Outputs on the builder.
    /// </summary>
    /// <param name="value">The outputs.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOutputs(StartBroadcastRequest.BroadcastOutput value);
}

/// <summary>
///     Represents a StartBroadcastRequestBuilder that allows to set optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<StartBroadcastRequest>
{
    /// <summary>
    ///     Sets the StreamMode to Manual on the builder.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithManualStreamMode();

    /// <summary>
    ///     Sets the MaxBitrate on the builder.
    /// </summary>
    /// <param name="value">The bitrate.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMaxBitrate(int value);

    /// <summary>
    ///     Sets the MaxDuration on the builder.
    /// </summary>
    /// <param name="value">The duration.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMaxDuration(int value);

    /// <summary>
    ///     Sets the MultiBroadcastTag on the builder.
    /// </summary>
    /// <param name="value">The tag.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMultiBroadcastTag(string value);

    /// <summary>
    ///     Sets the Resolution on the builder.
    /// </summary>
    /// <param name="value">The resolution.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithResolution(RenderResolution value);
}