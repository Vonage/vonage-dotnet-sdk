#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the product type for a report.
/// </summary>
public enum ReportProduct
{
    /// <summary>SMS messages.</summary>
    [Description("SMS")] Sms,

    /// <summary>SMS Traffic Control.</summary>
    [Description("SMS-TRAFFIC-CONTROL")] SmsTrafficControl,

    /// <summary>Voice calls.</summary>
    [Description("VOICE-CALL")] VoiceCall,

    /// <summary>Failed voice calls.</summary>
    [Description("VOICE-FAILED")] VoiceFailed,

    /// <summary>Voice text-to-speech calls.</summary>
    [Description("VOICE-TTS")] VoiceTts,

    /// <summary>In-app voice calls.</summary>
    [Description("IN-APP-VOICE")] InAppVoice,

    /// <summary>WebSocket calls.</summary>
    [Description("WEBSOCKET-CALL")] WebsocketCall,

    /// <summary>Automatic Speech Recognition.</summary>
    [Description("ASR")] Asr,

    /// <summary>Answering Machine Detection.</summary>
    [Description("AMD")] Amd,

    /// <summary>Verify API.</summary>
    [Description("VERIFY-API")] VerifyApi,

    /// <summary>Verify V2 API.</summary>
    [Description("VERIFY-V2")] VerifyV2,

    /// <summary>Number Insight.</summary>
    [Description("NUMBER-INSIGHT")] NumberInsight,

    /// <summary>Number Insight V2.</summary>
    [Description("NUMBER-INSIGHT-V2")] NumberInsightV2,

    /// <summary>Conversation events.</summary>
    [Description("CONVERSATION-EVENT")] ConversationEvent,

    /// <summary>Conversation messages.</summary>
    [Description("CONVERSATION-MESSAGE")] ConversationMessage,

    /// <summary>Messages API.</summary>
    [Description("MESSAGES")] Messages,

    /// <summary>Video API.</summary>
    [Description("VIDEO-API")] VideoApi,

    /// <summary>Network API events.</summary>
    [Description("NETWORK-API-EVENT")] NetworkApiEvent,

    /// <summary>Reports usage.</summary>
    [Description("REPORTS-USAGE")] ReportsUsage,
}
