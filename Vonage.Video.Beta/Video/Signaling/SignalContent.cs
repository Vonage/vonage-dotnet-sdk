namespace Vonage.Video.Beta.Video.Signaling;

public readonly struct SignalContent
{
    public string Type { get; }
    public string Data { get; }

    public SignalContent(string type, string data)
    {
        this.Type = type;
        this.Data = data;
    }
}