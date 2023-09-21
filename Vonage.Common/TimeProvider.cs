using System;
using Epoch.net;

namespace Vonage.Common;

/// <inheritdoc />
public class TimeProvider : ITimeProvider
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;

    /// <inheritdoc />
    public int Epoch => EpochTime.Now.Epoch;
}