using Vonage.Request;

namespace Vonage.Video.Beta.Video.Session;

public interface ISessionClient
{
    Credentials Credentials { get; set; }
}