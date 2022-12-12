using System;
using System.Net.Http;
using Vonage.Request;
using Vonage.Video.Beta.Video.Sessions;

namespace Vonage.Video.Beta.Video;

public class VideoClient : IVideoClient
{
    public const string ApiUrl = "https://video.api.vonage.com";
    private Credentials credentials;

    public VideoClient(Credentials credentials)
    {
        this.Credentials = credentials;
    }

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

    public ISessionClient SessionClient { get; private set; }

    private void InitializeClients()
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = new Uri(ApiUrl),
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        this.SessionClient = new SessionClient(this.credentials, client, new Jwt());
    }
}