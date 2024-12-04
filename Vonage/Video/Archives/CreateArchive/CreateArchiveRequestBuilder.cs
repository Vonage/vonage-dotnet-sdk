#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server;
#endregion

namespace Vonage.Video.Archives.CreateArchive;

internal class CreateArchiveRequestBuilder : IBuilderForSessionId, IBuilderForApplicationId, IBuilderForOptional
{
    private Guid applicationId;
    private bool hasAudio = true;
    private bool hasVideo = true;
    private Layout layout;
    private Maybe<int> maxBitrate;
    private Maybe<string> multiArchiveTag;
    private Maybe<string> name;
    private OutputMode outputMode = OutputMode.Composed;
    private Maybe<RenderResolution> resolution;
    private string sessionId;
    private StreamMode streamMode = StreamMode.Auto;

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public Result<CreateArchiveRequest> Create() =>
        Result<CreateArchiveRequest>.FromSuccess(new CreateArchiveRequest
            {
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
                OutputMode = this.outputMode,
                StreamMode = this.streamMode,
                HasAudio = this.hasAudio,
                HasVideo = this.hasVideo,
                Layout = this.layout,
                Name = this.name,
                Resolution = this.resolution,
                MultiArchiveTag = this.multiArchiveTag,
                MaxBitrate = this.maxBitrate,
            })
            .Map(InputEvaluation<CreateArchiveRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifySessionId, VerifyApplicationId, VerifyMaximumMaxBitrate,
                VerifyMinimumMaxBitrate));

    /// <inheritdoc />
    public IBuilderForOptional DisableAudio()
    {
        this.hasAudio = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional DisableVideo()
    {
        this.hasVideo = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithArchiveLayout(Layout value)
    {
        this.layout = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithOutputMode(OutputMode value)
    {
        this.outputMode = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithRenderResolution(RenderResolution value)
    {
        this.resolution = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithStreamMode(StreamMode value)
    {
        this.streamMode = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithMultiArchiveTag(string value)
    {
        this.multiArchiveTag = value;
        return this;
    }

    public IBuilderForOptional WithMaxBitrate(int value)
    {
        this.maxBitrate = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<CreateArchiveRequest> VerifyApplicationId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<CreateArchiveRequest> VerifySessionId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<CreateArchiveRequest> VerifyMinimumMaxBitrate(CreateArchiveRequest request) =>
        request.MaxBitrate.Match(
            some => InputValidation.VerifyHigherOrEqualThan(request, some, 1000000, nameof(request.MaxBitrate)),
            () => request);

    private static Result<CreateArchiveRequest> VerifyMaximumMaxBitrate(CreateArchiveRequest request) =>
        request.MaxBitrate.Match(
            some => InputValidation.VerifyLowerOrEqualThan(request, some, 6000000, nameof(request.MaxBitrate)),
            () => request);
}

/// <summary>
///     Represents a builder for ApplicationId.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the ApplicationId.
    /// </summary>
    /// <param name="value">The ApplicationId.</param>
    /// <returns>The builder.</returns>
    IBuilderForSessionId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for SessionId.
/// </summary>
public interface IBuilderForSessionId
{
    /// <summary>
    ///     Sets the SessionId.
    /// </summary>
    /// <param name="value">The SessionId.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSessionId(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateArchiveRequest>
{
    /// <summary>
    ///     Disables the audio on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional DisableAudio();

    /// <summary>
    ///     Disables the video on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional DisableVideo();

    /// <summary>
    ///     Sets the archive's layout.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithArchiveLayout(Layout value);

    /// <summary>
    ///     Sets the name of the archive.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the output mode.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOutputMode(OutputMode value);

    /// <summary>
    ///     Sets the resolution.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithRenderResolution(RenderResolution value);

    /// <summary>
    ///     Sets the stream mode.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStreamMode(StreamMode value);

    /// <summary>
    ///     Sets the multi-archive tag.
    /// </summary>
    /// <param name="value">The tag/</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMultiArchiveTag(string value);

    /// <summary>
    ///     Sets the maximum bitrate.
    /// </summary>
    /// <param name="value">The maximum bitrate</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMaxBitrate(int value);
}