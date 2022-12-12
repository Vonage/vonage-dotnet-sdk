using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;

namespace Vonage.Video.Beta.Video.Sessions;

public interface ISessionClient
{
    Credentials Credentials { get; set; }

    Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request);
}