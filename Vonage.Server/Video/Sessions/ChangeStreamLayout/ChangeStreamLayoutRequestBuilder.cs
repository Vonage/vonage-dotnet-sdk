using System;
using System.Collections.Generic;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sessions.ChangeStreamLayout;

internal class ChangeStreamLayoutRequestBuilder : IBuilderForApplicationId,
    IBuilderForSessionId,
    IBuilderForItems
{
    private Guid applicationId;

    private readonly ICollection<ChangeStreamLayoutRequest.LayoutItem> items =
        new List<ChangeStreamLayoutRequest.LayoutItem>();

    private string sessionId;

    /// <inheritdoc />
    public Result<ChangeStreamLayoutRequest> Create() =>
        Result<ChangeStreamLayoutRequest>.FromSuccess(new ChangeStreamLayoutRequest
            {
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
                Items = this.items,
            })
            .Map(InputEvaluation<ChangeStreamLayoutRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForItems WithItem(ChangeStreamLayoutRequest.LayoutItem value)
    {
        this.items.Add(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForItems WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<ChangeStreamLayoutRequest> VerifyApplicationId(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<ChangeStreamLayoutRequest> VerifySessionId(ChangeStreamLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
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
    IBuilderForSessionId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder that allows to set the SessionId.
/// </summary>
public interface IBuilderForSessionId
{
    /// <summary>
    ///     Sets the SessionId on the builder.
    /// </summary>
    /// <param name="value">The session id.</param>
    /// <returns>The builder.</returns>
    IBuilderForItems WithSessionId(string value);
}

/// <summary>
///     Represents a builder that allows to add an Item.
/// </summary>
public interface IBuilderForItems : IVonageRequestBuilder<ChangeStreamLayoutRequest>
{
    /// <summary>
    ///     Adds an Item on the builder.
    /// </summary>
    /// <param name="value">The item.</param>
    /// <returns>The builder.</returns>
    IBuilderForItems WithItem(ChangeStreamLayoutRequest.LayoutItem value);
}