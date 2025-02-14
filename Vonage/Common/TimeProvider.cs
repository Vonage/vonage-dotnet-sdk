using System;

namespace Vonage.Common;

/// <inheritdoc />
public class TimeProvider : ITimeProvider
{
    /// <inheritdoc />
    public long Epoch => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}