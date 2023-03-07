using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents Viber-specific information.
/// </summary>
public class ViberRequestData
{
    /// <summary>
    ///     The node for Viber action buttons.
    /// </summary>
    [JsonPropertyOrder(5)]
    public ViberAction Action { get; set; }

    /// <summary>
    ///     The use of different category tags enables the business to send messages for
    ///     different use cases. For Viber Service Messages the first message sent from a
    ///     business to a user must be personal, informative and a targeted message - not promotional.
    ///     By default Vonage sends the transaction category to Viber Service Messages.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ViberMessageCategory>))]
    public ViberMessageCategory? Category { get; set; }

    /// <summary>
    ///     The duration of the video in seconds.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Duration { get; set; }

    /// <summary>
    ///     The file size of the video in MB.
    /// </summary>
    [JsonPropertyOrder(2)]
    public string FileSize { get; set; }

    /// <summary>
    ///     Set the time-to-live of message to be delivered in seconds. i.e. if the
    ///     message is not delivered in 600 seconds then delete the message.
    /// </summary>
    [JsonPropertyOrder(3)]
    public int? TTL { get; set; }

    /// <summary>
    ///     Viber-specific type definition. To use "template", please contact your
    ///     Vonage Account Manager to setup your templates.
    /// </summary>
    [JsonPropertyOrder(4)]
    public string Type { get; set; }
}