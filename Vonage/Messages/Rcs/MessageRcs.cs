#region
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public struct MessageRcs
{
    /// <summary>
    ///     A category describing the type of content contained in the RCS message. This is required when sending RCS messages
    ///     in certain countries in order to comply with regional regulations and contractual agreements. If you are unsure
    ///     about the restrictions and required categories for the country you are sending to, please contact your Vonage
    ///     Account Manager.
    ///     Must be one of: authentication, transaction, promotion, service-request, acknowledgement.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    ///     The orientation of the rich card.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RcsCardOrientation>))]
    public RcsCardOrientation? CardOrientation { get; set; }

    /// <summary>
    ///     The width of the rich cards displayed in the carousel.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RcsCardWidth>))]
    public RcsCardWidth? CardWidth { get; set; }

    /// <summary>
    ///     The alignment of the thumbnail image in the rich card. This property only applies when sending rich cards with a
    ///     card_orientation of HORIZONTAL.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RcsImageAlignment>))]
    public RcsImageAlignment? ImageAlignment { get; set; }
}

/// <summary>
///     The width of the rich cards displayed in the carousel.
/// </summary>
public enum RcsCardWidth
{
    /// <summary>
    /// </summary>
    [Description("SMALL")] Small,

    /// <summary>
    /// </summary>
    [Description("MEDIUM")] Medium,
}

/// <summary>
///     The orientation of the rich card.
/// </summary>
public enum RcsCardOrientation
{
    /// <summary>
    /// </summary>
    [Description("VERTICAL")] Vertical,

    /// <summary>
    /// </summary>
    [Description("HORIZONTAL")] Horizontal,
}

/// <summary>
///     The alignment of the thumbnail image in the rich card. This property only applies when sending rich cards with a
///     card_orientation of HORIZONTAL.
/// </summary>
public enum RcsImageAlignment
{
    /// <summary>
    /// </summary>
    [Description("LEFT")] Left,

    /// <summary>
    /// </summary>
    [Description("RIGHT")] Right,
}