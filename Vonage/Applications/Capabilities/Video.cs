#region
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Video capability.
/// </summary>
public class Video
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public Video() => this.Webhooks = new Dictionary<VideoWebhookType, VideoWebhook>();

    /// <summary>
    ///     Represents the collection of Webhook URLs with their configuration.
    /// </summary>
    [JsonProperty("webhooks")]
    public IDictionary<VideoWebhookType, VideoWebhook> Webhooks { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoStorage Storage { get; private set; }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static Video Build() => new Video();

    /// <summary>
    ///     Enabled cloud storage.
    /// </summary>
    /// <returns></returns>
    public Video EnableCloudStorage()
    {
        this.Storage = (this.Storage ?? VideoStorage.Default) with {CloudStorage = true};
        return this;
    }

    /// <summary>
    ///     Enables end-to-end encryption.
    /// </summary>
    /// <returns></returns>
    public Video EnableEndToEndEncryption()
    {
        this.Storage = (this.Storage ?? VideoStorage.Default) with {EndToEndEncryption = true};
        return this;
    }

    /// <summary>
    ///     Enables server-side encryption.
    /// </summary>
    /// <returns></returns>
    public Video EnableServerSideEncryption()
    {
        this.Storage = (this.Storage ?? VideoStorage.Default) with {ServerSideEncryption = true};
        return this;
    }

    /// <summary>
    ///     Sets the archive status webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithArchiveStatus(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.ArchiveStatus] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the broadcast status webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithBroadcastStatus(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.BroadcastStatus] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the captions status webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithCaptionsStatus(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.CaptionsStatus] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the connection created webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithConnectionCreated(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.ConnectionCreated] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the connection destroyed webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithConnectionDestroyed(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.ConnectionDestroyed] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the render status webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithRenderStatus(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.RenderStatus] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the session created webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSessionCreated(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SessionCreated] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the session destroyed webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSessionDestroyed(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SessionDestroyed] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the session notification webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSessionNotification(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SessionNotification] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the sip call created webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSipCallCreated(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SipCallCreated] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the sip call destroyed webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSipCallDestroyed(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SipCallDestroyed] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the sip call mute forced webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSipCallMuteForced(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SipCallMuteForced] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the sip call updated webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithSipCallUpdated(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.SipCallUpdated] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the stream created webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithStreamCreated(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.StreamCreated] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Sets the stream destroyed webhook for Video capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="active">Whether the webhook is active.</param>
    /// <returns>The Video capability instance for fluent chaining.</returns>
    public Video WithStreamDestroyed(string url, bool active = true)
    {
        this.Webhooks[VideoWebhookType.StreamDestroyed] = new VideoWebhook(
            new Uri(url),
            active
        );
        return this;
    }

    /// <summary>
    ///     Represents a webhook for Video API.
    /// </summary>
    /// <param name="Address">The webhook address.</param>
    /// <param name="Active">Whether the webhook is active.</param>
    public record VideoWebhook(
        [property: JsonProperty("address", Order = 0)]
        Uri Address,
        [property: JsonProperty("active", Order = 1)]
        bool Active);

    /// <summary>
    /// </summary>
    /// <param name="ServerSideEncryption"></param>
    /// <param name="EndToEndEncryption"></param>
    /// <param name="CloudStorage"></param>
    public record VideoStorage(
        [property: JsonProperty("server_side_encryption")]
        bool ServerSideEncryption,
        [property: JsonProperty("end_to_end_encryption")]
        bool EndToEndEncryption,
        [property: JsonProperty("cloud_storage")]
        bool CloudStorage)
    {
        internal static VideoStorage Default => new VideoStorage(false, false, false);
    }
}

/// <summary>
///     Represents various Video Webhook types.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum VideoWebhookType
{
    /// <summary>
    ///     Archive status webhook
    /// </summary>
    [EnumMember(Value = "archive_status")] ArchiveStatus = 0,

    /// <summary>
    ///     Connection created webhook
    /// </summary>
    [EnumMember(Value = "connection_created")]
    ConnectionCreated = 1,

    /// <summary>
    ///     Connection destroyed webhook
    /// </summary>
    [EnumMember(Value = "connection_destroyed")]
    ConnectionDestroyed = 2,

    /// <summary>
    ///     Session created webhook
    /// </summary>
    [EnumMember(Value = "session_created")]
    SessionCreated = 3,

    /// <summary>
    ///     Session destroyed webhook
    /// </summary>
    [EnumMember(Value = "session_destroyed")]
    SessionDestroyed = 4,

    /// <summary>
    ///     Session notification webhook
    /// </summary>
    [EnumMember(Value = "session_notification")]
    SessionNotification = 5,

    /// <summary>
    ///     Stream created webhook
    /// </summary>
    [EnumMember(Value = "stream_created")] StreamCreated = 6,

    /// <summary>
    ///     Stream destroyed webhook
    /// </summary>
    [EnumMember(Value = "stream_destroyed")]
    StreamDestroyed = 7,

    /// <summary>
    ///     Render status webhook
    /// </summary>
    [EnumMember(Value = "render_status")] RenderStatus = 8,

    /// <summary>
    ///     Captions status webhook
    /// </summary>
    [EnumMember(Value = "captions_status")]
    CaptionsStatus = 9,

    /// <summary>
    ///     Broadcast status webhook
    /// </summary>
    [EnumMember(Value = "broadcast_status")]
    BroadcastStatus = 10,

    /// <summary>
    ///     Sip call created status webhook
    /// </summary>
    [EnumMember(Value = "sip_call_created")]
    SipCallCreated = 11,

    /// <summary>
    ///     Sip call updated status webhook
    /// </summary>
    [EnumMember(Value = "sip_call_updated")]
    SipCallUpdated = 12,

    /// <summary>
    ///     Sip call destroyed status webhook
    /// </summary>
    [EnumMember(Value = "sip_call_destroyed")]
    SipCallDestroyed = 13,

    /// <summary>
    ///     Sip call mute forced status webhook
    /// </summary>
    [EnumMember(Value = "sip_call_mute_forced")]
    SipCallMuteForced = 14,
}