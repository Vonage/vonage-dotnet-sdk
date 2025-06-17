#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send a video message on Viber.
/// </summary>
public class ViberVideoRequest : ViberMessageBase
{
    /// <summary>
    ///     The video information of the request.
    /// </summary>
    [JsonPropertyOrder(9)]
    public VideoInformation Video { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.ViberService;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Video;

    /// <summary>
    ///     Represents the video information of the request.
    /// </summary>
    public struct VideoInformation
    {
        /// <summary>
        ///     Text caption to accompany message.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Caption { get; set; }

        /// <summary>
        ///     URL to an image file for a thumbnail preview of the video.
        /// </summary>
        [JsonPropertyOrder(2)]
        public string ThumbUrl { get; set; }

        /// <summary>
        ///     Publicly accessible URL of the video attachment. Supports file types .mp4 and .3gpp.
        ///     Only supports video codec H.264 and audio codec AAC.
        /// </summary>
        [JsonPropertyOrder(0)]
        public string Url { get; set; }
    }
}