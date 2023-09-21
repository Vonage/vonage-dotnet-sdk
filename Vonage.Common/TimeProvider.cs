using System;
using Epoch.net;

namespace Vonage.Common;

/// <inheritdoc />
public class TimeProvider : ITimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;

    /// <inheritdoc />
    public int Epoch => EpochTime.Now.Epoch;
}