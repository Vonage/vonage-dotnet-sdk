namespace Vonage.Video.AudioConnector.Start;

/// <summary>
///     Represents a response when starting an Audio Connector WebSocket connection.
/// </summary>
/// <param name="Id">A unique ID identifying the Audio Connector WebSocket connection.</param>
/// <param name="CaptionsId">Unique ID of the audio captioning session</param>
public record StartResponse(string Id, string CaptionsId);