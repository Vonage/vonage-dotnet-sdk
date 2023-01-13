using System.ComponentModel;

namespace Vonage.Server.Video.Archives.Common;

/// <summary>
///     Represents the archive layout type.
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