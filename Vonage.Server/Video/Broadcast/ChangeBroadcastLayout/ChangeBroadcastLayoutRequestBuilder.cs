using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Common;

namespace Vonage.Server.Video.Broadcast.ChangeBroadcastLayout;

/// <summary>
///     Represents a builder for a ChangeBroadcastLayoutRequest.
/// </summary>
public class ChangeBroadcastLayoutRequestBuilder :
    IVonageRequestBuilder<ChangeBroadcastLayoutRequest>,
    ChangeBroadcastLayoutRequestBuilder.IBuilderForApplicationId,
    ChangeBroadcastLayoutRequestBuilder.IBuilderForBroadcastId,
    ChangeBroadcastLayoutRequestBuilder.IBuilderForLayout
{
    private Guid applicationId;
    private Guid broadcastId;
    private Layout layout;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new ChangeBroadcastLayoutRequestBuilder();

    /// <inheritdoc />
    public Result<ChangeBroadcastLayoutRequest> Create() =>
        Result<ChangeBroadcastLayoutRequest>.FromSuccess(new ChangeBroadcastLayoutRequest
            {
                ApplicationId = this.applicationId,
                BroadcastId = this.broadcastId,
                Layout = this.layout,
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
        IBuilderForLayout WithBroadcastId(Guid value);
    }

    /// <summary>
    ///     Represents a GetBroadcastRequestBuilder that allows to set the Layout.
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
}