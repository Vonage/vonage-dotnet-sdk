#region
using System;
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Messages;
using Vonage.Messages.Sms;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Messages;

public class MessageRequestBaseTest
{
    private readonly SerializationTestHelper helper =
        new SerializationTestHelper(typeof(MessageRequestBaseTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void MessageBase()
    {
        var request = new TestMessageBase
        {
            ClientRef = "abcdefg",
            From = "015417543010",
            To = "441234567890",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
        };
        request.Serialize().Should().Be(this.helper.GetRequestJson());
    }

    [Fact]
    public void MessageBase_WithFailover()
    {
        var request = new TestMessageBase
        {
            ClientRef = "abcdefg",
            From = "015417543010",
            To = "441234567890",
            WebhookUrl = new Uri("https://example.com/status"),
            WebhookVersion = "v1",
            Failover = new List<IMessage>
            {
                new TestMessageBase
                {
                    ClientRef = "abcdefg",
                    From = "015417543010",
                    To = "441234567890",
                    WebhookUrl = new Uri("https://example.com/status"),
                    WebhookVersion = "v1",
                },
                new SmsRequest
                {
                    From = "015417543010",
                    To = "441234567890",
                    Text = "Hello World!",
                },
            },
        };
        request.Serialize().Should().Be(this.helper.GetRequestJson());
    }
}

internal class TestMessageBase : MessageRequestBase
{
    public override MessagesChannel Channel => MessagesChannel.SMS;
    public override MessagesMessageType MessageType => MessagesMessageType.Text;
}