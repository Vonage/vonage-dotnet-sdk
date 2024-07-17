#region
using Vonage.Common.Client;
using Vonage.Video.Archives;
using Vonage.Video.AudioConnector;
using Vonage.Video.Broadcast;
using Vonage.Video.ExperienceComposer;
using Vonage.Video.Moderation;
using Vonage.Video.Sessions;
using Vonage.Video.Signaling;
using Vonage.Video.Sip;
#endregion

namespace Vonage.Video;

/// <inheritdoc />
public class VideoClient : IVideoClient
{
    private readonly VonageHttpClientConfiguration configuration;

    internal VideoClient(VonageHttpClientConfiguration configuration) => this.configuration = configuration;

    /// <inheritdoc />
    public ArchiveClient ArchiveClient => new ArchiveClient(this.configuration);

    /// <inheritdoc />
    public BroadcastClient BroadcastClient => new BroadcastClient(this.configuration);

    /// <inheritdoc />
    public ModerationClient ModerationClient => new ModerationClient(this.configuration);

    /// <inheritdoc />
    public SessionClient SessionClient => new SessionClient(this.configuration);

    /// <inheritdoc />
    public SignalingClient SignalingClient => new SignalingClient(this.configuration);

    /// <inheritdoc />
    public SipClient SipClient => new SipClient(this.configuration);

    /// <inheritdoc />
    public ExperienceComposerClient ExperienceComposerClient => new ExperienceComposerClient(this.configuration);

    /// <inheritdoc />
    public AudioConnectorClient AudioConnectorClient => new AudioConnectorClient(this.configuration);
}