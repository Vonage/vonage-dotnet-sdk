using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetAvailableRooms;

/// <summary>
/// </summary>
public struct GetAvailableRoomsResponse
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("_embedded")]
    public EmbeddedResponse Embedded { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("_links")]
    public LinksResponse Links { get; set; }

    /// <summary>
    ///     The number of results returned on this page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     The overall number of available rooms.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// </summary>
    public struct EmbeddedResponse
    {
        /// <summary>
        ///     List of all accessible rooms
        /// </summary>
        public List<Room> Rooms { get; set; }
    }

    /// <summary>
    /// </summary>
    public struct LinksResponse
    {
        /// <summary>
        /// </summary>
        public Link First { get; set; }

        /// <summary>
        /// </summary>
        public Link Next { get; set; }

        /// <summary>
        /// </summary>
        [JsonPropertyName("prev")]
        public Link Previous { get; set; }

        /// <summary>
        /// </summary>
        public Link Self { get; set; }
    }
}