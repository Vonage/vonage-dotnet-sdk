using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Broadcast.StartBroadcast;

/// <summary>
///     Represents a builder for StartBroadcastRequest.
/// </summary>
public class StartBroadcastRequestBuilder : IBuilderForSessionId, IBuilderForOutputs, IBuilderForLayout,
    IBuilderForOptional
{
    private const int MaximumMaxDuration = 36000;
    private const int MinimumMaxDuration = 60;
    private readonly Guid applicationId;
    private int maxBitrate = 1000;
    private int maxDuration = 14400;
    private Maybe<string> multiBroadcastTag;
    private RenderResolution resolution = RenderResolution.StandardDefinitionLandscape;
    private StreamMode streamMode = StreamMode.Auto;
    private string sessionId;
    private string layout;
    private string outputs;

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
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyMaxDuration);

    /// <inheritdoc />
    public IBuilderForOutputs WithLayout(string value)
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
    public IBuilderForOptional WithOutputs(string value)
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

    private static Result<StartBroadcastRequest> VerifyApplicationId(StartBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<StartBroadcastRequest> VerifyMaxDuration(StartBroadcastRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.MaxDuration, MinimumMaxDuration, nameof(request.MaxDuration))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, MaximumMaxDuration,
                nameof(request.MaxDuration)));

    private static Result<StartBroadcastRequest> VerifySessionId(StartBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
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
    IBuilderForOutputs WithLayout(string value);
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
    IBuilderForOptional WithOutputs(string value);
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