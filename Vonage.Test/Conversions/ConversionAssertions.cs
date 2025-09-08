#region
using FluentAssertions;
#endregion

namespace Vonage.Test.Conversions;

internal static class ConversionAssertions
{
    internal static void ShouldBeSuccessfulConversion(this bool actual) => actual.Should().BeTrue();
}