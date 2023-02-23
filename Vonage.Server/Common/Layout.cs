namespace Vonage.Server.Common;

/// <summary>
///     Represents a layout.
/// </summary>
public struct Layout
{
    /// <summary>
    ///     Set the screenshareType property to the layout type to use when there is a screen-sharing stream in the session.
    ///     (This property is optional.) Note if you set the screenshareType property, you must set the type property to
    ///     "bestFit" and leave the stylesheet property unset.
    /// </summary>
    public LayoutType? ScreenshareType { get; set; }

    /// <summary>
    ///     Used for the custom layout to define the visual layout.
    /// </summary>
    public string Stylesheet { get; set; }

    /// <summary>
    ///     Specify this to assign the initial layout type for the archive. This applies only to composed archives. Valid
    ///     values for the layout
    ///     property are "bestFit" (best fit), "custom" (custom), "horizontalPresentation" (horizontal presentation), "pip"
    ///     (picture-in-picture), and "verticalPresentation" (vertical presentation)). If you specify a "custom" layout type,
    ///     set the stylesheet property of the layout object to the stylesheet. (For other layout types, do not set a
    ///     stylesheet property.).
    /// </summary>
    public LayoutType Type { get; set; }
}