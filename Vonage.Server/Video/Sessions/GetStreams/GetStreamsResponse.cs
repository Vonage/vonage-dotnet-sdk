using System.Text.Json.Serialization;

namespace Vonage.Server.Video.Sessions.GetStreams;

/// <summary>
///     Represents a response for the GetStreamsRequest.
/// </summary>
public struct GetStreamsResponse
{
    /// <summary>
    ///     Number of items in the response.
    /// </summary>
    public int Count { get; }

    /// <summary>
    ///     Stream layouts for requested session IDs.
    /// </summary>
    public Stream[] Items { get; }

    /// <summary>
    ///     Creates a response.
    /// </summary>
    /// <param name="count">The number of elements.</param>
    /// <param name="items">The streams.</param>
    [JsonConstructor]
    public GetStreamsResponse(int count, Stream[] items)
    {
        this.Count = count;
        this.Items = items;
    }

    /// <summary>
    ///     Represents a stream.
    /// </summary>
    public struct Stream
    {
        /// <summary>
        ///     The stream Id.
        /// </summary>
        /// <remarks>
        ///     This struct should be read-only. The setter is mandatory for deserialization.
        /// </remarks>
        public string Id { get; }

        /// <summary>
        ///     An array of the layout classes for the stream.
        /// </summary>
        /// <remarks>
        ///     This struct should be read-only. The setter is mandatory for deserialization.
        /// </remarks>
        public string[] LayoutClassList { get; }

        /// <summary>
        ///     The stream name (if one was set when the client published the stream).
        /// </summary>
        /// <remarks>
        ///     This struct should be read-only. The setter is mandatory for deserialization.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        ///     Set to "camera", "screen", or "custom". A "screen" video uses screen sharing on the publisher as the video source;
        ///     a "custom" video is published by a web client using an HTML VideoTrack element as the video source.
        /// </summary>
        /// <remarks>
        ///     This struct should be read-only. The setter is mandatory for deserialization.
        /// </remarks>
        public string VideoType { get; }

        /// <summary>
        ///     Creates a stream.
        /// </summary>
        /// <param name="id">The stream ID.</param>
        /// <param name="videoType">The video type.</param>
        /// <param name="name"></param>
        /// <param name="layoutClassList"></param>
        [JsonConstructor]
        public Stream(string id, string videoType, string name, string[] layoutClassList)
        {
            this.Id = id;
            this.VideoType = videoType;
            this.Name = name;
            this.LayoutClassList = layoutClassList;
        }
    }
}