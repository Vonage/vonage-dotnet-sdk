using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Request;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Broadcast;
using Vonage.Server.Video.Moderation;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Sip;

namespace Vonage.Server.Video;

/// <inheritdoc />
public class VideoClient : IVideoClient
{
    private Credentials credentials;

    /// <inheritdoc />
    public ArchiveClient ArchiveClient { get; private set; }

    /// <inheritdoc />
    public BroadcastClient BroadcastClient { get; private set; }

    /// <inheritdoc />
    public Credentials Credentials
    {
        get => this.credentials;
        set
        {
            if (value is null) return;
            this.credentials = value;
            this.InitializeClients();
        }
    }

    /// <inheritdoc />
    public ModerationClient ModerationClient { get; private set; }

    /// <inheritdoc />
    public SessionClient SessionClient { get; private set; }

    /// <inheritdoc />
    public SignalingClient SignalingClient { get; private set; }

    /// <inheritdoc />
    public SipClient SipClient { get; private set; }

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further clients.</param>
    public VideoClient(Credentials credentials) => this.Credentials = credentials;

    private VonageHttpClientConfiguration BuildClientConfiguration() =>
        new(InitializeHttpClient(), () => new Jwt().GenerateToken(this.Credentials),
            this.Credentials.GetUserAgent());

    private void InitializeClients()
    {
        var configuration = this.BuildClientConfiguration();
        this.SessionClient = new SessionClient(configuration);
        this.SignalingClient = new SignalingClient(configuration);
        this.ModerationClient = new ModerationClient(configuration);
        this.ArchiveClient = new ArchiveClient(configuration);
        this.BroadcastClient = new BroadcastClient(configuration);
        this.SipClient = new SipClient(configuration);
    }

    private static HttpClient InitializeHttpClient()
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = Configuration.Instance.VideoApiUrl,
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }
}