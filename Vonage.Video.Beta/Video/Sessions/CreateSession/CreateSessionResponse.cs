using Newtonsoft.Json;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

public struct CreateSessionResponse
{
    public CreateSessionResponse(string sessionId) => this.SessionId = sessionId;

    [JsonProperty("session_id")] public string SessionId { get; set; }
}