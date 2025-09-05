#region
using Vonage.Messaging;
#endregion

namespace Vonage.Test.Messaging;

internal static class MessagingTestData
{
    internal static SendSmsRequest CreateBasicRequest() =>
        new SendSmsRequest
        {
            From = "AcmeInc",
            To = "447700900000",
            Text = "Hello World!",
        };

    internal static SendSmsRequest CreateRequestWithAllProperties() =>
        new SendSmsRequest
        {
            AccountRef = "customer1234",
            Body = "638265253311",
            Callback = "https://example.com/sms-dlr",
            ClientRef = "my-personal-reference",
            From = "AcmeInc",
            To = "447700900000",
            MessageClass = 0,
            ProtocolId = 127,
            StatusReportReq = true,
            Text = "Hello World!",
            Ttl = 900000,
            Type = SmsType.Text,
            Udh = "06050415811581",
            ContentId = "testcontent",
            EntityId = "testEntity",
            TrustedNumber = true,
        };

    internal static SendSmsRequest CreateUnicodeRequest() =>
        new SendSmsRequest
        {
            From = "AcmeInc",
            To = "447700900000",
            Text = "こんにちは世界",
        };
}