using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Common;

namespace Vonage.Server.Video.Archives.CreateArchive;

/// <inheritdoc />
public class CreateArchiveRequestBuilder : IVonageRequestBuilder<CreateArchiveRequest>
{
    private ArchiveLayout layout;
    private bool hasAudio = true;
    private bool hasVideo = true;
    private readonly Guid applicationId;
    private Maybe<string> name = Maybe<string>.None;
    private OutputMode outputMode = OutputMode.Composed;
    private RenderResolution resolution = RenderResolution.StandardDefinitionLandscape;
    private StreamMode streamMode = StreamMode.Auto;
    private readonly string sessionId;

    private CreateArchiveRequestBuilder(Guid applicationId, string sessionId)
    {
        this.applicationId = applicationId;
        this.sessionId = sessionId;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="applicationId">The Vonage Application UUID.</param>
    /// <param name="sessionId"></param>
    /// <returns>The builder.</returns>
    public static CreateArchiveRequestBuilder Build(Guid applicationId, string sessionId) =>
        new(applicationId, sessionId);

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
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId);

    /// <summary>
    ///     Disables the audio on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder DisableAudio()
    {
        this.hasAudio = false;
        return this;
    }

    /// <summary>
    ///     Disables the video on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder DisableVideo()
    {
        this.hasVideo = false;
        return this;
    }

    /// <summary>
    ///     Sets the archive's layout.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder WithArchiveLayout(ArchiveLayout value)
    {
        this.layout = value;
        return this;
    }

    /// <summary>
    ///     Sets the name of the archive.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <summary>
    ///     Sets the output mode.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder WithOutputMode(OutputMode value)
    {
        this.outputMode = value;
        return this;
    }

    /// <summary>
    ///     Sets the resolution.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder WithRenderResolution(RenderResolution value)
    {
        this.resolution = value;
        return this;
    }

    /// <summary>
    ///     Sets the stream mode.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The builder.</returns>
    public CreateArchiveRequestBuilder WithStreamMode(StreamMode value)
    {
        this.streamMode = value;
        return this;
    }

    private static Result<CreateArchiveRequest> VerifyApplicationId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<CreateArchiveRequest> VerifySessionId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}