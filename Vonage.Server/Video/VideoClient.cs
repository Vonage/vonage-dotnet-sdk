using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
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
    private readonly Maybe<Configuration> configuration = Maybe<Configuration>.None;

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

    internal VideoClient(Credentials credentials, Configuration configuration)
    {
        this.configuration = configuration;
        this.Credentials = credentials;
    }

    private VonageHttpClientConfiguration BuildClientConfiguration() =>
        new(
            InitializeHttpClient(this.GetConfiguration().VideoApiUrl),
            this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private void InitializeClients()
    {
        var videoConfiguration = this.BuildClientConfiguration();
        this.SessionClient = new SessionClient(videoConfiguration);
        this.SignalingClient = new SignalingClient(videoConfiguration);
        this.ModerationClient = new ModerationClient(videoConfiguration);
        this.ArchiveClient = new ArchiveClient(videoConfiguration);
        this.BroadcastClient = new BroadcastClient(videoConfiguration);
        this.SipClient = new SipClient(videoConfiguration);
    }

    private static HttpClient InitializeHttpClient(Uri baseUri)
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = baseUri,
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }
}