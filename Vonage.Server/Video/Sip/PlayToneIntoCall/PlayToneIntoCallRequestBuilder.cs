using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sip.PlayToneIntoCall;

/// <summary>
///     Represents a builder for PlayToneIntoCallRequestBuilder.
/// </summary>
internal class PlayToneIntoCallRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForDigits,
    IBuilderForSessionId,
    IVonageRequestBuilder<PlayToneIntoCallRequest>
{
    private Guid applicationId;
    private string digits;
    private string sessionId;

    /// <inheritdoc />
    public Result<PlayToneIntoCallRequest> Create() =>
        Result<PlayToneIntoCallRequest>.FromSuccess(new PlayToneIntoCallRequest
            {
                ApplicationId = this.applicationId,
                Digits = this.digits,
                SessionId = this.sessionId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifySessionId)
            .Bind(VerifyDigits);

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<PlayToneIntoCallRequest> WithDigits(string value)
    {
        this.digits = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForDigits WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<PlayToneIntoCallRequest> VerifyDigits(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));
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
///     Represents a builder that allows to set the Digits.
/// </summary>
public interface IBuilderForDigits
{
    /// <summary>
    ///     Sets the Digits on the builder.
    /// </summary>
    /// <param name="value">The digits.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<PlayToneIntoCallRequest> WithDigits(string value);
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
    IBuilderForDigits WithSessionId(string value);
}