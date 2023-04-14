using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public struct AuthenticationFailure : IResultFailure
{
    /// <inheritdoc />
    public string GetFailureMessage() => VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey().Message;

    /// <inheritdoc />
    public Exception ToException() => VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey();

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}