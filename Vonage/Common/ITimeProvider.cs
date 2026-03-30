#region
using System;
#endregion

namespace Vonage.Common;

/// <summary>
///     Provides access to the current time. Enables testability by allowing time to be mocked.
/// </summary>
/// <remarks>
///     <para>Use dependency injection with this interface to make time-dependent code testable.</para>
///     <para>See <see cref="TimeProvider"/> for the default production implementation.</para>
/// </remarks>
public interface ITimeProvider
{
    /// <summary>
    ///     Gets the current time as a Unix timestamp (seconds since January 1, 1970 UTC).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// long timestamp = timeProvider.Epoch;
    /// // Example: 1704067200 (represents 2024-01-01 00:00:00 UTC)
    /// ]]></code>
    /// </example>
    long Epoch { get; }

    /// <summary>
    ///     Gets the current UTC date and time.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// DateTime now = timeProvider.UtcNow;
    /// ]]></code>
    /// </example>
    DateTime UtcNow { get; }
}