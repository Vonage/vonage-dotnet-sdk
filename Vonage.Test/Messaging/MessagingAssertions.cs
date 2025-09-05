#region
using FluentAssertions;
using Vonage.Messaging;
#endregion

namespace Vonage.Test.Messaging;

internal static class MessagingAssertions
{
    private static void ShouldHaveExpectedBasicProperties(this SendSmsResponse actual)
    {
        actual.MessageCount.Should().Be("1");
        actual.Messages[0].To.Should().Be("447700900000");
        actual.Messages[0].MessageId.Should().Be("0A0000000123ABCD1");
        actual.Messages[0].Status.Should().Be("0");
        actual.Messages[0].StatusCode.Should().Be(SmsStatusCode.Success);
        actual.Messages[0].RemainingBalance.Should().Be("3.14159265");
        actual.Messages[0].Network.Should().Be("12345");
        actual.Messages[0].AccountRef.Should().Be("customer1234");
    }

    internal static void ShouldMatchExpectedBasicResponse(this SendSmsResponse actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
    }

    internal static void ShouldMatchExpectedResponseWithClientRef(this SendSmsResponse actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.Messages[0].ClientRef.Should().Be("my-personal-reference");
    }
}