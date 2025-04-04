﻿using System;
using FluentAssertions;
using Xunit;
using TimeProvider = Vonage.Common.TimeProvider;

namespace Vonage.Test.Common;

[Trait("Category", "Unit")]
public class TimeProviderTest
{
    /// <remarks>
    ///     Given we can predict the exact generated Epoch, we have to assume it is "recent".
    /// </remarks>
    [Fact]
    public void Epoch_ShouldReturnCurrentEpoch()
    {
        var reference = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var now = new TimeProvider().Epoch;
        var delay = now - reference;
        delay.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(100000);
    }

    /// <remarks>
    ///     Given we can predict the exact generated DateTime, we have to assume it is "recent".
    /// </remarks>
    [Fact]
    public void Now_ShouldReturnCurrentDateTime()
    {
        var reference = DateTime.UtcNow;
        var now = new TimeProvider().UtcNow;
        var delay = now - reference;
        delay.TotalSeconds.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(2);
    }
}