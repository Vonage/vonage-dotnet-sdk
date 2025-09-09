#region
using FluentAssertions;
using Vonage.Common.Exceptions;
#endregion

namespace Vonage.Test.Redact;

internal static class RedactAssertions
{
    internal static void ShouldBeSuccessfulRedaction(this bool actual) =>
        actual.Should().BeTrue();

    internal static void ShouldBeHttpRequestException(this VonageHttpRequestException actual, string expectedJson)
    {
        actual.Should().NotBeNull();
        actual.Json.Should().Be(expectedJson);
    }
}