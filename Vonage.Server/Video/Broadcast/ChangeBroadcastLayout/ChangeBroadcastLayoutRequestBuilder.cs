using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;

internal class ChangeBroadcastLayoutRequestBuilder :
    IVonageRequestBuilder<ChangeBroadcastLayoutRequest>,
    IBuilderForApplicationId,
    IBuilderForBroadcastId,
    IBuilderForLayout
{
    private Guid applicationId;
    private Guid broadcastId;
    private Layout layout;

    /// <inheritdoc />
    public Result<ChangeBroadcastLayoutRequest> Create() =>
        Result<ChangeBroadcastLayoutRequest>.FromSuccess(new ChangeBroadcastLayoutRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
                Layout = this.layout,
            })
            .Map(InputEvaluation<ChangeBroadcastLayoutRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifyBroadcastId));

    /// <inheritdoc />
    public IBuilderForBroadcastId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForLayout WithBroadcastId(Guid value)
    {
        this.broadcastId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<ChangeBroadcastLayoutRequest> WithLayout(Layout value)
    {
        this.layout = value;
        return this;
    }

    private static Result<ChangeBroadcastLayoutRequest> VerifyApplicationId(ChangeBroadcastLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<ChangeBroadcastLayoutRequest> VerifyBroadcastId(ChangeBroadcastLayoutRequest request) =>
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
    IBuilderForLayout WithBroadcastId(Guid value);
}

/// <summary>
///     Represents a builder that allows to set the Layout.
/// </summary>
public interface IBuilderForLayout
{
    /// <summary>
    ///     Sets the Layout on the builder.
    /// </summary>
    /// <param name="value">The layout.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<ChangeBroadcastLayoutRequest> WithLayout(Layout value);
}