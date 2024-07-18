namespace Vonage.Video.LiveCaptions.Start;

/// <summary>
///     Represents a response when starting an Audio Connector WebSocket connection.
/// </summary>
/// <param name="CaptionsId">Unique ID of the audio captioning session</param>
public record StartResponse(string CaptionsId);