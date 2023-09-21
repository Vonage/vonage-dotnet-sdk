using Epoch.net;
using FluentAssertions;

namespace Vonage.Common.Test;

public class TimeProviderTest
{
    /// <remarks>
    ///     Given we can predict the exact generated DateTime, we have to assume it is "recent".
    /// </remarks>
    [Fact]
    public void Now_ShouldReturnCurrentDateTime()
    {
        var reference = DateTime.Now;
        var now = new TimeProvider().Now;
        var delay = (now.Ticks - reference.Ticks);
        delay.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(10000);
    }

    /// <remarks>
    ///     Given we can predict the exact generated Epoch, we have to assume it is "recent".
    /// </remarks>
    [Fact]
    public void Epoch_ShouldReturnCurrentEpoch()
    {
        var reference = EpochTime.Now.Epoch;
        var now = new TimeProvider().Epoch;
        var delay = (now - reference);
        delay.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(10000);
    }
}