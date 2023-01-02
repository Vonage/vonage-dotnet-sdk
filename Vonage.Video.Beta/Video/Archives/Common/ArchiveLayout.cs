namespace Vonage.Video.Beta.Video.Archives.Common;

/// <summary>
///     Represents the archive's layout.
/// </summary>
public struct ArchiveLayout
{
    /// <summary>
    ///     Specify this to assign the initial layout type for the archive. This applies only to composed archives. Valid
    ///     values for the layout
    ///     property are "bestFit" (best fit), "custom" (custom), "horizontalPresentation" (horizontal presentation), "pip"
    ///     (picture-in-picture), and "verticalPresentation" (vertical presentation)). If you specify a "custom" layout type,
    ///     set the stylesheet property of the layout object to the stylesheet. (For other layout types, do not set a
    ///     stylesheet property.).
    /// </summary>
    public LayoutType Type { get; }

    /// <summary>
    ///     Used for the custom layout to define the visual layout.
    /// </summary>
    public string Stylesheet { get; }

    /// <summary>
    ///     Set the screenshareType property to the layout type to use when there is a screen-sharing stream in the session.
    ///     (This property is optional.) Note if you set the screenshareType property, you must set the type property to
    ///     "bestFit" and leave the stylesheet property unset.
    /// </summary>
    public LayoutType? ScreenshareType { get; }

    /// <summary>
    ///     Creates an archive layout.
    /// </summary>
    /// <param name="type"> Specify this to assign the initial layout type for the archive</param>
    /// <param name="stylesheet">  Used for the custom layout to define the visual layout</param>
    /// <param name="screenshareType">
    ///     Set the screenshareType property to the layout type to use when there is a screen-sharing
    ///     stream in the session.
    /// </param>
    public ArchiveLayout(LayoutType type, string stylesheet, LayoutType screenshareType)
    {
        this.Type = type;
        this.Stylesheet = stylesheet;
        this.ScreenshareType = screenshareType;
    }
}