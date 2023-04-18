using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.AddStreamToBroadcast;

/// <summary>
///     Represents a builder for AddStreamToBroadcastRequest.
/// </summary>
public class AddStreamToBroadcastRequestBuilder :
    AddStreamToBroadcastRequestBuilder.IBuilderForApplicationId,
    AddStreamToBroadcastRequestBuilder.IBuilderForBroadcastId,
    AddStreamToBroadcastRequestBuilder.IBuilderForStreamId,
    AddStreamToBroadcastRequestBuilder.IBuilderForOptional
{
    private bool hasVideo = true;
    private bool hasAudio = true;
    private Guid applicationId;
    private Guid streamId;
    private Guid broadcastId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new AddStreamToBroadcastRequestBuilder();

    /// <inheritdoc />
    public Result<AddStreamToBroadcastRequest> Create() =>
        Result<AddStreamToBroadcastRequest>.FromSuccess(new AddStreamToBroadcastRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
                HasAudio = this.hasAudio,
                HasVideo = this.hasVideo,
                StreamId = this.streamId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(VerifyBroadcastId)
            .Bind(BuilderExtensions.VerifyStreamId);

    /// <inheritdoc />
    public IBuilderForBroadcastId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForStreamId WithBroadcastId(Guid value)
    {
        this.broadcastId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithDisabledAudio()
    {
        this.hasAudio = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithDisabledVideo()
    {
        this.hasVideo = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<AddStreamToBroadcastRequest> VerifyBroadcastId(AddStreamToBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));

    /// <summary>
    ///     Represents a GetBroadcastRequestBuilder that allows to set the ApplicationId.
    /// </summary>
    public interface IBuilderForApplicationId
    {
        /// <summary>
        ///     Sets the ApplicationId on the builder.
        /// </summary>
        /// <param name="value">The application id.</param>
        /// <returns>The builder.</returns>
        IBuilderForBroadcastId WithApplicationId(Guid value);
    }

    /// <summary>
    ///     Represents a GetBroadcastRequestBuilder that allows to set the ApplicationId.
    /// </summary>
    public interface IBuilderForBroadcastId
    {
        /// <summary>
        ///     Sets the BroadcastId on the builder.
        /// </summary>
        /// <param name="value">The broadcast id.</param>
        /// <returns>The builder.</returns>
        IBuilderForStreamId WithBroadcastId(Guid value);
    }

    /// <summary>
    ///     Represents a GetBroadcastRequestBuilder that allows to set the StreamId.
    /// </summary>
    public interface IBuilderForStreamId
    {
        /// <summary>
        ///     Sets the StreamId on the builder.
        /// </summary>
        /// <param name="value">The stream id.</param>
        /// <returns>The builder.</returns>
        IBuilderForOptional WithStreamId(Guid value);
    }

    /// <summary>
    ///     Represents a AddStreamToBroadcastRequestBuilder that allows to set optional values.
    /// </summary>
    public interface IBuilderForOptional : IVonageRequestBuilder<AddStreamToBroadcastRequest>
    {
        /// <summary>
        ///     Disables audio on the builder.
        /// </summary>
        /// <returns>The builder.</returns>
        IBuilderForOptional WithDisabledAudio();

        /// <summary>
        ///     Disables video on the builder.
        /// </summary>
        /// <returns>The builder.</returns>
        IBuilderForOptional WithDisabledVideo();
    }
}