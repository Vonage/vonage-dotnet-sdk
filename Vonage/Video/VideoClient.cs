using Vonage.Common.Client;
using Vonage.Video.Archives;
using Vonage.Video.Broadcast;
using Vonage.Video.Moderation;
using Vonage.Video.Sessions;
using Vonage.Video.Signaling;
using Vonage.Video.Sip;

namespace Vonage.Video;

/// <inheritdoc />
public class VideoClient : IVideoClient
{
    private readonly VonageHttpClientConfiguration configuration;

    /// <inheritdoc />
    public ArchiveClient ArchiveClient => new(this.configuration);

    /// <inheritdoc />
    public BroadcastClient BroadcastClient => new(this.configuration);

    /// <inheritdoc />
    public ModerationClient ModerationClient => new(this.configuration);

    /// <inheritdoc />
    public SessionClient SessionClient => new(this.configuration);

    /// <inheritdoc />
    public SignalingClient SignalingClient => new(this.configuration);

    /// <inheritdoc />
    public SipClient SipClient => new(this.configuration);

    internal VideoClient(VonageHttpClientConfiguration configuration) => this.configuration = configuration;
}