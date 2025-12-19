#region
using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
public struct NoneFailure : IResultFailure
{
    /// <summary>
    /// </summary>
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