#region
using Vonage.Redaction;
#endregion

namespace Vonage.Test.Redact;

internal static class RedactTestData
{
    internal static RedactRequest CreateSmsInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Sms,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateSmsOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Sms,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateMessagesInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Messages,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateMessagesOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Messages,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateNumberInsightInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.NumberInsight,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateNumberInsightOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.NumberInsight,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateVerifyInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Verify,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateVerifyOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Verify,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateVerifySdkInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.VerifySdk,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateVerifySdkOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.VerifySdk,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateVoiceInboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Voice,
            Type = RedactionType.Inbound,
        };

    internal static RedactRequest CreateVoiceOutboundRequest() =>
        new RedactRequest
        {
            Id = "test",
            Product = RedactionProduct.Voice,
            Type = RedactionType.Outbound,
        };

    internal static RedactRequest CreateErrorTestRequest() =>
        new RedactRequest
        {
            Id = "209ab3c7536542b91e8b5aef032f6861",
            Product = RedactionProduct.Sms,
            Type = RedactionType.Inbound,
        };
}