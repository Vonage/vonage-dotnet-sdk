namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call times out, typically when the ringing timer expires before the call is answered.
/// </summary>
public class Timeout : CallStatusEvent
{
}