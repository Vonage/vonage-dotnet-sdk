using System.Collections.Generic;

namespace Vonage.Server.Video.Broadcast.GetBroadcasts;

public struct GetBroadcastsResponse
{
    public int Count { get; set; }
    public List<Common.Broadcast> Items { get; set; }
}