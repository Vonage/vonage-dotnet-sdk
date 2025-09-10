#region
using FluentAssertions;
#endregion

namespace Vonage.Test.Voice;

internal static class EndpointTestAssertions
{
    internal static void ShouldMatchExpectedNccoEndpointJson(this string actual)
    {
        var expected =
            "{\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"},\"type\":\"websocket\"}";
        actual.Should().Be(expected);
    }

    internal static void ShouldMatchExpectedWebhookEndpointJson(this string actual)
    {
        var expected =
            "{\"type\":\"websocket\",\"uri\":\"wss://www.example.com/ws\",\"content-type\":\"audio/l16;rate=16000\",\"headers\":{\"Bar\":\"bar\"}}";
        actual.Should().Be(expected);
    }
}