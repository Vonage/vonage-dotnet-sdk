using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Request;
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
    private Credentials credentials;

    /// <inheritdoc />
    public ArchiveClient ArchiveClient => new(this.BuildClientConfiguration());

    /// <inheritdoc />
    public BroadcastClient BroadcastClient => new(this.BuildClientConfiguration());

    /// <inheritdoc />
    public Credentials Credentials
    {
        get => this.credentials;
        set
        {
            if (value is null) return;
            this.credentials = value;
        }
    }

    /// <inheritdoc />
    public ModerationClient ModerationClient => new(this.BuildClientConfiguration());

    /// <inheritdoc />
    public SessionClient SessionClient => new(this.BuildClientConfiguration());

    /// <inheritdoc />
    public SignalingClient SignalingClient => new(this.BuildClientConfiguration());

    /// <inheritdoc />
    public SipClient SipClient => new(this.BuildClientConfiguration());

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further clients.</param>
    public VideoClient(Credentials credentials) => this.Credentials = credentials;

    private VonageHttpClientConfiguration BuildClientConfiguration() =>
        new(
            InitializeHttpClient(this.GetConfiguration().VideoApiUrl),
            this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.Configuration.IfNone(Vonage.Configuration.Instance);

    private static HttpClient InitializeHttpClient(Uri baseUri)
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = baseUri,
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }

    /// <summary>
    ///     The instance of Configuration used by the client.
    /// </summary>
    protected Maybe<Configuration> Configuration = Maybe<Configuration>.None;
}