using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct AuthenticationFailure : IResultFailure
{
    private readonly string failure;

    /// <inheritdoc />
    public Type Type => typeof(AuthenticationFailure);

    public AuthenticationFailure(string failure) => this.failure = failure;

    /// <inheritdoc />
    public string GetFailureMessage() => this.failure;

    /// <inheritdoc />
    public Exception ToException() => VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey();

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}