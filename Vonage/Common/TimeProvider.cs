#region
using System;
#endregion

namespace Vonage.Common;

/// <summary>
///     Default implementation of <see cref="ITimeProvider" /> that returns the actual system time.
/// </summary>
/// <remarks>
///     For unit testing, consider creating a mock implementation that returns controlled time values.
/// </remarks>
public class TimeProvider : ITimeProvider
{
    /// <inheritdoc />
    public long Epoch => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}