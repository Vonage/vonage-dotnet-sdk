using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send an image message on Viber.
/// </summary>
public class ViberImageRequest : IViberMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.ViberService;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(5)]
    [JsonPropertyName("viber_service")]
    public ViberRequestData Data { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <summary>
    ///    The file information of the request.
    /// </summary>
    [JsonPropertyOrder(6)]
    public FileInformation Image { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <summary>
    ///     Represents the file information of the request.
    /// </summary>
    public struct FileInformation
    {
        /// <summary>
        ///     A caption to accompany the image. Required if the message includes an action button.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Caption { get; set; }

        /// <summary>
        ///     The publicly accessible URL of the image attachment. The image file is available for 48 hours after it is created.
        ///     Supported types are .jpg, .jpeg, and .png
        /// </summary>
        [JsonPropertyOrder(0)]
        public string Url { get; set; }
    }
}