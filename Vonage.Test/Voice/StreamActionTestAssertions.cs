#region
using FluentAssertions;
#endregion

namespace Vonage.Test.Voice;

internal static class StreamActionTestAssertions
{
    internal static void ShouldMatchExpectedStreamUrlJson(this string actual)
    {
        var expected = "{\"action\":\"stream\",\"streamUrl\":[\"https://www.example.com/waiting.mp3\"]}";
        actual.Should().Be(expected);
    }
}