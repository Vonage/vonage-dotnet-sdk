using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetRoomsByTheme;

/// <summary>
/// </summary>
public struct GetRoomsByThemeResponse
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("_links")]
    public RoomLinks Links { get; set; }

    /// <summary>
    ///     The number of results returned on this page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("_embedded")]
    public List<Room> Rooms { get; set; }
}