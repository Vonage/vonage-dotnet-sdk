#region
using Vonage.Verify;
#endregion

namespace Vonage.Test.Verify;

internal static class VerifyTestData
{
    internal static Psd2Request CreateBasicPsd2Request() =>
        new Psd2Request {Number = "447700900000", Payee = "Acme Inc", Amount = 4.8};

    internal static Psd2Request CreateFullPsd2Request() =>
        new Psd2Request
        {
            Number = "447700900000",
            Payee = "Acme Inc",
            Amount = 4.8,
            Country = "GB",
            CodeLength = 4,
            Lg = "en-us",
            PinExpiry = 240,
            NextEventWait = 60,
            WorkflowId = Psd2Request.Workflow.SMS_TTS_TTS,
        };

    internal static VerifyRequest CreateBasicVerifyRequest() =>
        new VerifyRequest {Number = "447700900000", Brand = "Acme Inc"};

    internal static VerifyRequest CreateFullVerifyRequest() =>
        new VerifyRequest
        {
            Number = "447700900000",
            Brand = "Acme Inc",
            Country = "GB",
            SenderId = "ACME",
            CodeLength = 4,
            Lg = "en-us",
            PinExpiry = 240,
            NextEventWait = 60,
            WorkflowId = VerifyRequest.Workflow.SMS_TTS_TTS,
        };

    internal static VerifyCheckRequest CreateBasicVerifyCheckRequest() =>
        new VerifyCheckRequest {Code = "1234", RequestId = "abcdef0123456789abcdef0123456789"};

    internal static VerifyCheckRequest CreateVerifyCheckRequestWithIpAddress() =>
        new VerifyCheckRequest
        {
            Code = "1234",
            RequestId = "abcdef0123456789abcdef0123456789",
            IpAddress = "123.0.0.255",
        };

    internal static VerifyControlRequest CreateVerifyControlRequest() =>
        new VerifyControlRequest {Cmd = "cancel", RequestId = "abcdef0123456789abcdef0123456789"};

    internal static VerifySearchRequest CreateVerifySearchRequest() =>
        new VerifySearchRequest {RequestId = "abcdef0123456789abcdef0123456789"};
}