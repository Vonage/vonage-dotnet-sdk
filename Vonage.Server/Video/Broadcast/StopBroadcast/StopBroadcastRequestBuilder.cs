using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.StopBroadcast;

/// <summary>
///     Represents a builder for a StopBroadcastRequest.
/// </summary>
public class StopBroadcastRequestBuilder :
    IVonageRequestBuilder<StopBroadcastRequest>,
    StopBroadcastRequestBuilder.IBuilderForApplicationId,
    StopBroadcastRequestBuilder.IBuilderForBroadcastId
{
    private Guid applicationId;
    private Guid broadcastId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StopBroadcastRequestBuilder();

    /// <inheritdoc />
    public Result<StopBroadcastRequest> Create() =>
        Result<StopBroadcastRequest>.FromSuccess(new StopBroadcastRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifyBroadcastId);

    /// <inheritdoc />
    public IBuilderForBroadcastId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<StopBroadcastRequest> WithBroadcastId(Guid value)
    {
        this.broadcastId = value;
        return this;
    }

    private static Result<StopBroadcastRequest> VerifyApplicationId(StopBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<StopBroadcastRequest> VerifyBroadcastId(StopBroadcastRequest request) =>
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
        IVonageRequestBuilder<StopBroadcastRequest> WithBroadcastId(Guid value);
    }
}