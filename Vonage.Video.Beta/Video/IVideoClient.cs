using Vonage.Request;
using Vonage.Video.Beta.Video.Session;

namespace Vonage.Video.Beta.Video;

public interface IVideoClient
{
    Credentials Credentials { get; set; }

    ISessionClient SessionClient { get; }
}