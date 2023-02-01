﻿using System.Text.Json.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetAvailableRooms;

/// <summary>
/// </summary>
public struct GetAvailableRoomsResponse
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("_embedded")]
    public RoomEmbedded Embedded { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("_links")]
    public RoomLinks Links { get; set; }

    /// <summary>
    ///     The number of results returned on this page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     The overall number of available rooms.
    /// </summary>
    public int TotalItems { get; set; }
}