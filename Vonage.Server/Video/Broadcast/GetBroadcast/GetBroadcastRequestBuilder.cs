using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.GetBroadcast;

internal class GetBroadcastRequestBuilder :
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
            .Map(InputEvaluation<GetBroadcastRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifyBroadcastId));

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

    private static Result<GetBroadcastRequest> VerifyApplicationId(GetBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetBroadcastRequest> VerifyBroadcastId(GetBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));
}

/// <summary>
///     Represents a builder that allows to set the ApplicationId.
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
///     Represents a builder that allows to set the ApplicationId.
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