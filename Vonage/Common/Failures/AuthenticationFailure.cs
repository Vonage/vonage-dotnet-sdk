#region
using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // Create an authentication failure
/// AuthenticationFailure failure = new AuthenticationFailure("Missing API key");
/// Console.WriteLine(failure.GetFailureMessage()); // "Missing API key"
///
/// // Convert to Result for chained operations
/// Result<User> result = failure.ToResult<User>();
/// ]]></code>
/// </example>
public readonly struct AuthenticationFailure : IResultFailure
{
    private readonly string failure;

    /// <summary>
    ///     Initializes a new <see cref="AuthenticationFailure"/> with a custom failure message.
    /// </summary>
    /// <param name="failure">The failure message returned by <see cref="GetFailureMessage"/>.</param>
    public AuthenticationFailure(string failure) => this.failure = failure;

    /// <inheritdoc />
    public Type Type => typeof(AuthenticationFailure);

    /// <inheritdoc />
    public string GetFailureMessage() => this.failure;

    /// <inheritdoc />
    public Exception ToException() => VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey();

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}