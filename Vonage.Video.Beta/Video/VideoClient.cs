using Vonage.Request;
using Vonage.Video.Beta.Video.Session;

namespace Vonage.Video.Beta.Video;

public class VideoClient : IVideoClient
{
    private Credentials credentials;

    public VideoClient(Credentials credentials)
    {
        this.Credentials = credentials;
        this.InitializeClients();
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

    private void InitializeClients() =>
        this.SessionClient = new SessionClient(this.credentials);
}