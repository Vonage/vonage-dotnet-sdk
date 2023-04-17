namespace Vonage.Server.Video.Broadcast.GetBroadcasts;

/// <summary>
///     Represents a response from GetBroadcasts.
/// </summary>
public struct GetBroadcastsResponse
{
    /// <summary>
    ///     The number of broadcasts in the the response.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///     The list of broadcasts.
    /// </summary>
    public Broadcast[] Items { get; set; }
}