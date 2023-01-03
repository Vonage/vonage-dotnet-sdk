using System;
using System.Net.Http;
using Vonage.Request;
using Vonage.Video.Beta.Video.Moderation;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Signaling;

namespace Vonage.Video.Beta.Video;

/// <inheritdoc />
public class VideoClient : IVideoClient
{
    private const string ApiUrl = "https://video.api.vonage.com";
    private Credentials credentials;

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
    public ISessionClient SessionClient { get; private set; }

    /// <inheritdoc />
    public ISignalingClient SignalingClient { get; private set; }

    /// <inheritdoc />
    public ModerationClient ModerationClient { get; private set; }

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further clients.</param>
    public VideoClient(Credentials credentials)
    {
        this.Credentials = credentials;
    }

    private void InitializeClients()
    {
        var client = InitializeHttpClient();
        this.SessionClient = new SessionClient(client, () => new Jwt().GenerateToken(this.Credentials));
        this.SignalingClient = new SignalingClient(client, () => new Jwt().GenerateToken(this.Credentials));
        this.ModerationClient = new ModerationClient(client, () => new Jwt().GenerateToken(this.Credentials));
    }

    private static HttpClient InitializeHttpClient()
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = new Uri(ApiUrl),
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }
}