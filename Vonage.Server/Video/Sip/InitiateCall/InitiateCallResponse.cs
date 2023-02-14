namespace Vonage.Server.Video.Sip.InitiateCall;

public struct InitiateCallResponse
{
    public string Id { get; set; }
    public string ConnectionId { get; set; }
    public string StreamId { get; set; }
}