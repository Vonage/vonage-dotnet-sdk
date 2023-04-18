using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Broadcast.GetBroadcast;

/// <summary>
///     Represents a builder for a GetBroadcastRequest.
/// </summary>
public class GetBroadcastRequestBuilder :
    IVonageRequestBuilder<GetBroadcastRequest>,
    IBuilderForApplicationId,
    IBuilderForBroadcastId
{
    private Guid applicationId;
    private Guid broadcastId;

    /// <inheritdoc />
    public Result<GetBroadcastRequest> Create() =>
        Result<GetBroadcastRequest>.FromSuccess(new GetBroadcastRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifyBroadcastId);

    /// <inheritdoc />
    public IBuilderForBroadcastId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetBroadcastRequest> WithBroadcastId(Guid value)
    {
        this.broadcastId = value;
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
    IVonageRequestBuilder<GetBroadcastRequest> WithBroadcastId(Guid value);
}