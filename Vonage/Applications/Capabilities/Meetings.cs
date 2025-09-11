#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Meetings capability.
/// </summary>
public class Meetings : Capability
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public Meetings()
    {
        this.Type = CapabilityType.Meetings;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    public Meetings(IDictionary<Webhook.Type, Webhook> webhooks) : this()
    {
        this.Webhooks = webhooks;
    }

    /// <summary>
    ///     Sets the recording changed webhook for Meetings capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Meetings capability instance for fluent chaining.</returns>
    public Meetings WithRecordingChanged(string url)
    {
        this.Webhooks[Webhook.Type.RecordingChanged] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }

    /// <summary>
    ///     Sets the room changed webhook for Meetings capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Meetings capability instance for fluent chaining.</returns>
    public Meetings WithRoomChanged(string url)
    {
        this.Webhooks[Webhook.Type.RoomChanged] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }

    /// <summary>
    ///     Sets the session changed webhook for Meetings capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Meetings capability instance for fluent chaining.</returns>
    public Meetings WithSessionChanged(string url)
    {
        this.Webhooks[Webhook.Type.SessionChanged] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }
}