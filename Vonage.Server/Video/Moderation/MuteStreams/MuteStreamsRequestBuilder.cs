using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Moderation.MuteStreams;

internal class MuteStreamsRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForConfiguration,
    IBuilderForSessionId,
    IVonageRequestBuilder<MuteStreamsRequest>
{
    private Guid applicationId;
    private MuteStreamsRequest.MuteStreamsConfiguration configuration;
    private string sessionId;

    /// <inheritdoc />
    public Result<MuteStreamsRequest> Create() =>
        Result<MuteStreamsRequest>.FromSuccess(new MuteStreamsRequest
            {
                ApplicationId = this.applicationId,
                Configuration = this.configuration,
                SessionId = this.sessionId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifySessionId)
            .Bind(VerifyExcludedStreams);

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<MuteStreamsRequest> WithConfiguration(
        MuteStreamsRequest.MuteStreamsConfiguration value)
    {
        this.configuration = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForConfiguration WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<MuteStreamsRequest> VerifyExcludedStreams(MuteStreamsRequest request) =>
        InputValidation.VerifyNotNull(request, request.Configuration.ExcludedStreamIds,
            nameof(MuteStreamsRequest.MuteStreamsConfiguration.ExcludedStreamIds));
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
    IBuilderForConfiguration WithSessionId(string value);
}

/// <summary>
///     Represents a builder that allows to set the Configuration.
/// </summary>
public interface IBuilderForConfiguration
{
    /// <summary>
    ///     Sets the Configuration on the builder.
    /// </summary>
    /// <param name="value">The configuration.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<MuteStreamsRequest> WithConfiguration(MuteStreamsRequest.MuteStreamsConfiguration value);
}