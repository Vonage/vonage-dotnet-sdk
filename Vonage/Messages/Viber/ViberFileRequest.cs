using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send a file message on Viber.
/// </summary>
public struct ViberFileRequest : IViberMessage
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

    /// <summary>
    ///     The file information of the request.
    /// </summary>
    [JsonPropertyOrder(6)]
    public FileInformation File { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <summary>
    ///     Represents the file information of the request.
    /// </summary>
    public struct FileInformation
    {
        /// <summary>
        ///     The name and extension of the file.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Name { get; set; }

        /// <summary>
        ///     The URL for the file attachment or the path for the location of the file attachment. If name is included, can just
        ///     be the path. If name is not included, must include the filename and extension.
        /// </summary>
        [JsonPropertyOrder(0)]
        public string Url { get; set; }
    }
}