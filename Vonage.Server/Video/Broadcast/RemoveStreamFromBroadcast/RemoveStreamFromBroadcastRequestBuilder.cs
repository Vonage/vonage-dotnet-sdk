using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;

/// <summary>
///     Represents a builder for RemoveStreamFromBroadcastRequest.
/// </summary>
public class RemoveStreamFromBroadcastRequestBuilder :
    IVonageRequestBuilder<RemoveStreamFromBroadcastRequest>,
    RemoveStreamFromBroadcastRequestBuilder.IBuilderForApplicationId,
    RemoveStreamFromBroadcastRequestBuilder.IBuilderForBroadcastId,
    RemoveStreamFromBroadcastRequestBuilder.IBuilderForStreamId
{
    private Guid applicationId;
    private Guid streamId;
    private string broadcastId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new RemoveStreamFromBroadcastRequestBuilder();

    /// <inheritdoc />
    public Result<RemoveStreamFromBroadcastRequest> Create() =>
        Result<RemoveStreamFromBroadcastRequest>.FromSuccess(new RemoveStreamFromBroadcastRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
                StreamId = this.streamId,
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifyBroadcastId)
            .Bind(VerifyStreamId);

    /// <inheritdoc />
    public IBuilderForBroadcastId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForStreamId WithBroadcastId(string value)
    {
        this.broadcastId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<RemoveStreamFromBroadcastRequest> WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<RemoveStreamFromBroadcastRequest> VerifyApplicationId(
        RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<RemoveStreamFromBroadcastRequest>
        VerifyBroadcastId(RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));

    private static Result<RemoveStreamFromBroadcastRequest> VerifyStreamId(RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));

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
        IBuilderForStreamId WithBroadcastId(string value);
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
        IVonageRequestBuilder<RemoveStreamFromBroadcastRequest> WithStreamId(Guid value);
    }
}