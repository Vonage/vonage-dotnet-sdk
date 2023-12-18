using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Vonage.Server;

/// <summary>
///     Represents a layout.
/// </summary>
/// <param name="ScreenshareType">
///     Set the screenshareType property to the layout type to use when there is a screen-sharing stream in the session.
///     (This property is optional.) Note if you set the screenshareType property, you must set the type property to
///     "bestFit" and leave the stylesheet property unset.
/// </param>
/// <param name="Stylesheet">
///     Used for the custom layout to define the visual layout.
/// </param>
/// <param name="Type">
///     Specify this to assign the initial layout type for the archive. This applies only to composed archives. Valid
///     values for the layout
///     property are "bestFit" (best fit), "custom" (custom), "horizontalPresentation" (horizontal presentation), "pip"
///     (picture-in-picture), and "verticalPresentation" (vertical presentation)). If you specify a "custom" layout type,
///     set the stylesheet property of the layout object to the stylesheet. (For other layout types, do not set a
///     stylesheet property.).
/// </param>
public record Layout(
    [property: JsonPropertyOrder(2)] LayoutType? ScreenshareType,
    [property: JsonPropertyOrder(1)] string Stylesheet,
    [property: JsonPropertyOrder(0)] LayoutType Type);

/// <summary>
///     Represents the layout type.
/// </summary>
public enum LayoutType
{
    /// <summary>
    ///     Best fit.
    /// </summary>
    [Description("bestFit")] BestFit,

    /// <summary>
    ///     A custom layout.
    /// </summary>
    [Description("custom")] Custom,

    /// <summary>
    ///     Horizontal presentation.
    /// </summary>
    [Description("horizontalPresentation")]
    HorizontalPresentation,

    /// <summary>
    ///     Vertical presentation.
    /// </summary>
    [Description("verticalPresentation")] VerticalPresentation,

    /// <summary>
    ///     Picture-in-picture.
    /// </summary>
    [Description("pip")] Pip,
}