namespace Vonage.Common.Failures;

/// <summary>
///     Represents a Failure with an error message.
/// </summary>
public interface IResultFailure
{
    /// <summary>
    ///     Returns the error message defined in the failure.
    /// </summary>
    /// <returns>The error message.</returns>
    string GetFailureMessage();
}