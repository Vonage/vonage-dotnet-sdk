using Vonage.Request;

namespace Vonage.Video.Beta.Video.Session;

public class SessionClient : ISessionClient
{
    public SessionClient(Credentials credentials)
    {
        this.Credentials = credentials;
    }

    public Credentials Credentials { get; set; }
}