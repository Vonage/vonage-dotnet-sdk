namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call is cancelled before it is answered, typically when the caller hangs up during ringing.
/// </summary>
public class Cancelled : CallStatusEvent
{
}