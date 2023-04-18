using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;

/// <summary>
///     Represents a builder for RemoveStreamFromBroadcastRequest.
/// </summary>
internal class RemoveStreamFromBroadcastRequestBuilder :
    IVonageRequestBuilder<RemoveStreamFromBroadcastRequest>,
    IBuilderForApplicationId,
    IBuilderForBroadcastId,
    IBuilderForStreamId
{
    private Guid applicationId;
    private Guid streamId;
    private Guid broadcastId;

    /// <inheritdoc />
    public Result<RemoveStreamFromBroadcastRequest> Create() =>
        Result<RemoveStreamFromBroadcastRequest>.FromSuccess(new RemoveStreamFromBroadcastRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
                StreamId = this.streamId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifyBroadcastId)
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
    public IVonageRequestBuilder<RemoveStreamFromBroadcastRequest> WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
    }
}

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
    IVonageRequestBuilder<RemoveStreamFromBroadcastRequest> WithStreamId(Guid value);
}