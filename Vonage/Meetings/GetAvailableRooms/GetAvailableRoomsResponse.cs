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
    ///     Constructor.
    /// </summary>
    /// <param name="embedded"></param>
    /// <param name="links"></param>
    /// <param name="pageSize">The number of results returned on this page.</param>
    /// <param name="totalItems">  The overall number of available rooms.</param>
    public GetAvailableRoomsResponse(EmbeddedResponse embedded, LinksResponse links, int pageSize, int totalItems)
    {
        this.Embedded = embedded;
        this.Links = links;
        this.PageSize = pageSize;
        this.TotalItems = totalItems;
    }

    /// <summary>
    /// </summary>
    public struct EmbeddedResponse
    {
        /// <summary>
        ///     List of all accessible rooms
        /// </summary>
        public List<Room> Rooms { get; set; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="rooms">  List of all accessible rooms</param>
        public EmbeddedResponse(List<Room> rooms) => this.Rooms = rooms;
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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="next"></param>
        /// <param name="previous"></param>
        /// <param name="self"></param>
        public LinksResponse(Link first, Link next, Link previous, Link self)
        {
            this.First = first;
            this.Next = next;
            this.Previous = previous;
            this.Self = self;
        }
    }
}