#region
using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // NoneFailure is used when converting a None Maybe to a Result
/// Maybe<string> none = Maybe<string>.None;
/// Result<string> result = none.ToResult();
/// // result.IsFailure is true, and the failure type is NoneFailure
/// ]]></code>
/// </example>
public struct NoneFailure : IResultFailure
{
    /// <summary>
    ///     The singleton instance of NoneFailure.
    /// </summary>
    /// <example>
    ///     <code><![CDATA[
    /// Result<int> failure = Result<int>.FromFailure(NoneFailure.Value);
    /// ]]></code>
    /// </example>
    public static NoneFailure Value => new NoneFailure();

    /// <inheritdoc />
    public Type Type => typeof(NoneFailure);

    /// <inheritdoc />
    public string GetFailureMessage() => "None";

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.GetFailureMessage());

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}